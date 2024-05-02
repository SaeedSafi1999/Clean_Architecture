using Core.Domain.BaseEntity;
using Core.Domain.Entities.Transactions;

namespace Core.Domain.Entities.Categories
{
    public class Category:BaseEntity<long>
    {
        public string  Name { get; set; }
        public long parentId { get; set; }
        public Category Parent { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
