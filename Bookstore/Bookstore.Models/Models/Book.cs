using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Models
{
    public record Book
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public int AuthorId { get; init; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public decimal Price { get; set; }
    }
}
