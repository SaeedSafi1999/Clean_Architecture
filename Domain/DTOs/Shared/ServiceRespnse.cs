using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DTOs.Shared
{
    public interface IServiceResponse
    {
        ServiceRespnse OK(HttpStatusCode status = HttpStatusCode.OK);
        ServiceRespnse Failed(HttpStatusCode status,string ErrorMessage);
    }

    public interface IServiceResponse<T>
    {
        ServiceRespnse<T> OK(HttpStatusCode status = HttpStatusCode.OK);
        ServiceRespnse Failed(HttpStatusCode status, string ErrorMessage);
    }

    public class ServiceRespnse<T>:IServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }

    public class ServiceRespnse:IServiceResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
