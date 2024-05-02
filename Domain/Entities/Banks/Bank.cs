using Core.Domain.BaseEntity;
using Core.Domain.Entities.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Entities.Banks
{
    public class Bank:BaseEntity<long>
    {
        public string Name { get; set; }
        public string SVG { get; set; }
        public string Branch { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}
