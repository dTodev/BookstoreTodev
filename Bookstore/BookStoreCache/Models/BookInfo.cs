using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Bookstore.Cache.Models
{
    [MessagePackObject]
    public record BookInfo
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int AuthorId { get; set; }
        [Key(2)]
        public string? Title { get; set; }
        [Key(3)]
        public DateTime LastUpdated { get; set; }
        [Key(4)]
        public int Quantity { get; set; }
        [Key(5)]
        public decimal Price { get; set; }
    }
}
