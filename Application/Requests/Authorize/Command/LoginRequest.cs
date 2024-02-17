using Application.Cqrs.Commands;
using Core.Application.Database;
using Core.Application.Extensions;
using Core.Application.Requests.Authorize.DTO;
using Core.Application.Services.AuthorizeServices;
using Core.Application.Services.AuthorizeServices.DTO;
using Core.Domain.DTOs.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests.Authorize.Command
{
    public class LoginRequest : ICommand<IServiceResponse<TokenDTO>>
    {
        public LoginDTO LoginDTO { get; set; }

        public class LoginRequestHandler : ICommandHandler<LoginRequest, IServiceResponse<TokenDTO>>
        {
            private readonly IToken _token;
            private readonly IUnitOfWork _unitOfWork;

            public LoginRequestHandler(IToken token, IUnitOfWork unitOfWork)
            {
                _token = token;
                _unitOfWork = unitOfWork;
            }
            public async Task<IServiceResponse<TokenDTO>> Handle(LoginRequest request, CancellationToken cancellationToken)
            {
                //set validation
                var validaion = new Hashtable();
                //set operator
                var Operator = new ServiceRespnse<TokenDTO>();
                //get user repository 
                var dto = request.LoginDTO;
                var Repository = _unitOfWork.GetRepository<Entities.UsersEntity.User>();
                var UserExist = await Repository.GetAsNoTrackingQuery().FirstOrDefaultAsync(x => x.Mobile == dto.Mobile);
                if (UserExist == null)//check user exist
                {
                    Operator.Message = "There Is No User By This Mobile";
                    Operator.IsSuccess = false;
                }
                else
                {
                    var verify = HashExtension.VerifyHashPassword(dto.Password, UserExist.PasswordSalt, UserExist.PasswordHash);
                    if (verify && UserExist.IsActive == true)
                    {
                        //generate token
                        var token = await _token.Get(UserExist);
                        token.FullName = UserExist.FullName;
                        Operator.Message = "Operation success";
                        Operator.StatusCode =System.Net.HttpStatusCode.OK;
                        Operator.IsSuccess = true;
                        Operator.Data = token;
                    }
                    else if (UserExist.IsActive == false)
                    {
                        Operator.Message =  "User Is Not Active";
                        Operator.IsSuccess = false;
                        Operator.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    }
                    else
                    {
                        Operator.Message = "Invalid Pass Or Mobile";
                        Operator.IsSuccess = false;
                        Operator.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    }
                }

                return Operator;
            }
        }
    }
}
