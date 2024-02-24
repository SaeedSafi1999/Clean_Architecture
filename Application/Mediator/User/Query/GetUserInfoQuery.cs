using Application.Cqrs.Queris;
using Core.Application.Database;
using Core.Application.Mediator.User.DTO;
using Core.Application.Requests.User.DTO;
using Core.Domain.DTOs.Shared;
using Entities.UsersEntity;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.User.Query
{
    public class GetUserInfoQuery : IQuery<IServiceResponse<GetUserInfoDTO>>
    {
        public GetUserInfoQuery(long userId)
        {
             UserId = userId;
        }

        public long UserId { get; init; }

        public class GetUserInfoQueryHandler : IQueryHandler<GetUserInfoQuery, IServiceResponse<GetUserInfoDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetUserInfoQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IServiceResponse<GetUserInfoDTO>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
            {
                var Operations = new ServiceRespnse<GetUserInfoDTO>();
                var Errors = new Hashtable();
                var Repository = _unitOfWork.GetRepository<Entities.UsersEntity.User>();
                var user = await Repository.GetAsNoTrackingQuery().FirstOrDefaultAsync(z => z.Id == request.UserId);
                if (user is null)
                {
                    Errors.Add("Id", "user not found");
                    return Operations.Failed(System.Net.HttpStatusCode.NotFound, Errors);
                }
                var result = new GetUserInfoDTO
                {
                    About = user.About,
                    Age = user.Age,
                    Discord = user.Discord,
                    Email = user.Email,
                    FaceBook = user.FaceBook,
                    FirstName = user.FirstName,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    LastName = user.LastName,
                    Mobile = user.Mobile,
                    Telegram = user.Telegram,
                    UserName = user.UserName,
                    UserType = user.UserType,
                };
                return Operations.OK(result); 
            }
        }
    }
}
