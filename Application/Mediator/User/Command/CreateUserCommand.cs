using Application.Cqrs.Commands;
using Core.Application.Database;
using Core.Application.Requests.User.DTO;
using Core.Domain.DTOs.Shared;
using Entities.Users;
using Entities.UsersEntity;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Core.Application.Extensions;
using System.Net;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Requests.User.Command
{
    public class CreateUserCommand : ICommand<IServiceResponse>
    {
        public CreateUserDTO CreateUserDTO { get; set; }


        public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, IServiceResponse>
        {
            private readonly IUnitOfWork _ProtectedDb;

            public CreateUserCommandHandler(IUnitOfWork protectedDb)
            {
                _ProtectedDb = protectedDb;
            }

            public async Task<IServiceResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                // set validation here if you need with hash table
                var Errors = new Hashtable();

                //generate Hash for password
                HashExtension.MakeHmacHashCode(request.CreateUserDTO.Password, out byte[] hash, out byte[] salt);
                //get repository
                var Repository = _ProtectedDb.GetRepository<Entities.UsersEntity.User>();
                var UserExist = await Repository.GetAsNoTrackingQuery().AnyAsync(z => z.Mobile == request.CreateUserDTO.Mobile);
                if (UserExist)
                {
                    Errors.Add("Mobile", "Mobile Must Be Unique");
                    return new ServiceRespnse().Failed(HttpStatusCode.NotFound, Errors);
                }
                else
                {
                    var insertedData = await Repository.AddAsync(new Entities.UsersEntity.User
                    {
                        About = request.CreateUserDTO.About,
                        PasswordHash = hash,
                        PasswordSalt = salt,
                        Age = request.CreateUserDTO.Age,
                        Mobile = request.CreateUserDTO.Mobile,
                        Email = request.CreateUserDTO.Email,
                        Discord = request.CreateUserDTO.Discord,
                        FaceBook = request.CreateUserDTO.FaceBook,
                        FirstName = request.CreateUserDTO.FirstName,
                        FullName = request.CreateUserDTO.FullName,
                        Gender = request.CreateUserDTO.Gender,
                        IsReadRules = request.CreateUserDTO.IsReadRules,
                        LastName = request.CreateUserDTO.LastName,
                        Telegram = request.CreateUserDTO.Telegram,
                        UserType = request.CreateUserDTO.UserType,
                        UserName = request.CreateUserDTO.UserName,
                        RoleId = 3
                    });

                    await _ProtectedDb.CommitAsync();

                    return new ServiceRespnse().OK();
                }
            }

        }

    }
}
