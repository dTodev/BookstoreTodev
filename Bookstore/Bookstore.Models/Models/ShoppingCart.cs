using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
