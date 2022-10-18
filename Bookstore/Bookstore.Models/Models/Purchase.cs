using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Models
{
    public record Purchase
    {
        public Guid Id { get; init; }
        public ICollection<Book> Books { get; set; } = Enumerable.Empty<Book>().ToList();
        public decimal TotalMoney { get ; set; }
        public int UserId { get; set; }
    }
}
