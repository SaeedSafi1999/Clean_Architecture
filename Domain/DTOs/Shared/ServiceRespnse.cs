using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DTOs.Shared
{
    public class ServiceRespnse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }

    public class ServiceRespnse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
