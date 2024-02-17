using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Database;
using Core.Application.Requests.User.DTO;
using Core.Domain.DTOs.Shared;
using Entities.UsersEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests.User.Query
{
    public class GetAllUsersQuery : IQuery<IServiceResponse<IReadOnlyList<GetAllUserDTO>>>
    {
        public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IServiceResponse<IReadOnlyList<GetAllUserDTO>>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
            {

                _unitOfWork = unitOfWork;
            }

            public async Task<IServiceResponse<IReadOnlyList<GetAllUserDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var repository = _unitOfWork.GetRepository<Entities.UsersEntity.User>();
                var data = await repository.GetAsNoTrackingQuery()
                    .Select(z => new GetAllUserDTO
                    {
                        Id = z.Id,
                        UserName = z.UserName,
                    }).ToListAsync();

                return new ServiceRespnse<IReadOnlyList<GetAllUserDTO>>().OK(data);
            }
        }
    }
}
