using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.DTOs.Product
{
    public class AddProductDTO
    {
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
