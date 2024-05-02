using Application.Cqrs.Commands;
using Core.Application.Database;
using Core.Application.Requests.User.DTO;
using Core.Domain.DTOs.Shared;
using System.Collections;
using Core.Application.Extensions;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Requests.User.Command
{
    public class CreateUserCommand : ICommand<ServiceRespnse>
    {
        public CreateUserDTO CreateUserDTO { get; set; }


        public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, ServiceRespnse>
        {
            private readonly IUnitOfWork _ProtectedDb;

            public CreateUserCommandHandler(IUnitOfWork protectedDb)
            {
                _ProtectedDb = protectedDb;
            }

            public async Task<ServiceRespnse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                // set validation here if you need with hash table
                var Errors = new Hashtable();

                byte[]? saltSet = null;
                byte[]? hashSet = null;
                //generate Hash for password
                if (request.CreateUserDTO.Password != null)
                {
                    HashExtension.MakeHmacHashCode(request.CreateUserDTO.Password, out byte[] hash, out byte[] salt);
                    saltSet = salt;
                    hashSet = hash;
                }

                bool UserExist = false;
                var Repository = _ProtectedDb.GetRepository<Entities.UsersEntity.User>();
                //get repository
                if (!string.IsNullOrEmpty(request.CreateUserDTO.Mobile))
                {
                    UserExist = await Repository.GetAsNoTrackingQuery()
                       .AnyAsync(z => z.Mobile == request.CreateUserDTO.Mobile);
                }

                if (UserExist)
                {
                    Errors.Add("Mobile", "Mobile Must Be Unique");
                    return new ServiceRespnse().Failed(HttpStatusCode.NotFound, Errors);
                }
                else
                {
                    var insertedData = await Repository.AddAsync(new Entities.UsersEntity.User
                    {
                        TelegramId = request.CreateUserDTO.TelegramId,
                        PasswordHash = hashSet.Length > 0 ? hashSet : null,
                        PasswordSalt = saltSet.Length > 0 ? saltSet : null,
                        Mobile = request.CreateUserDTO.Mobile,
                        Email = request.CreateUserDTO.Email,
                        FirstName = request.CreateUserDTO.FirstName,
                        FullName = request.CreateUserDTO.FullName,
                        LastName = request.CreateUserDTO.LastName,
                        UserType = Entities.Users.UserType.DefaultUser,
                        UserName = request.CreateUserDTO.UserName,
                        RoleId = 3
                    }); ;

                    await _ProtectedDb.CommitAsync();

                    return new ServiceRespnse().OK();
                }
            }
        }
    }
}