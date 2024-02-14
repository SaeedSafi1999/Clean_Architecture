using Application.Cqrs.Commands;
using Core.Application.Database;
using Core.Domain.DTOs.Shared;
using Entities.Users;
using Entities.UsersEntity;

namespace Core.Application.Requests.User.Command
{
    public class CreateUserCommand:ICommand<ServiceRespnse>
    {
        public string FullName { get; set; }
        public DateTime CreateDate { get; private set; } = DateTime.Now;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public GenderType Gender { get; set; }
        public UserType UserType { get; set; }
        public string? Discord { get; set; }
        public string? FaceBook { get; set; }
        public string? Telegram { get; set; }
        public string? About { get; set; }
        public bool IsReadRules { get; set; }


        public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, ServiceRespnse>
        {
            private readonly IUnitOfWork _ProtectedDb;

            public CreateUserCommandHandler(IUnitOfWork protectedDb)
            {
                _ProtectedDb = protectedDb;
            }

            public async Task<ServiceRespnse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var Repository = _ProtectedDb.GetRepository<Entities.UsersEntity.User>();
                try
                {
                   var insertedData =  await Repository.AddAsync(new Entities.UsersEntity.User
                    {
                        About = request.About,
                        Age = request.Age,
                        Discord = request.Discord,
                        FaceBook = request.FaceBook,
                        FirstName = request.FirstName,
                        FullName = request.FullName,
                        Gender = request.Gender,
                        IsReadRules = request.IsReadRules,
                        LastName = request.LastName,
                        Telegram = request.Telegram,
                        UserType = request.UserType,
                        UserName = request.UserName,
                    });
                    if(insertedData)
                        return new ServiceRespnse() { IsSuccess = true,Message="" };
                }
                catch (Exception ex)
                {

                    throw;
                }
                
            }
        }
    }
}
