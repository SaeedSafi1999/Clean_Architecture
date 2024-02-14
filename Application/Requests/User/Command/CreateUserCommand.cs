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
                // set validation
                Hashtable validationErrors = new();
                string[] requiredProperties = { "FirstName", "LastName", "UserName", "Mobile","Email" };
                foreach (var property in requiredProperties)
                {
                    if (string.IsNullOrEmpty(request.CreateUserDTO.GetType().GetProperty(property).GetValue(request.CreateUserDTO) as string))
                    {
                        validationErrors.Add(property, "MustHaveValue");
                    }
                }
                if (!request.CreateUserDTO.IsReadRules)
                {
                    validationErrors.Add("IsReadRule", "MustHaveValueAndTrue");
                }
                if (validationErrors.Count > 0)
                {
                    return new ServiceRespnse().Failed(HttpStatusCode.BadRequest, validationErrors);
                }

                try
                {
                    var Repository = _ProtectedDb.GetRepository<Entities.UsersEntity.User>();
                    var insertedData = await Repository.AddAsync(new Entities.UsersEntity.User
                    {
                        About = request.CreateUserDTO.About,
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
                catch (Exception ex)
                {
                    validationErrors.Add("HasException", ex.Message);
                    return new ServiceRespnse().Failed(HttpStatusCode.InternalServerError, validationErrors);
                }

            }

        }

    }
}
