using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessagePack;

namespace Bookstore.Models.Models.Users
{
    [MessagePackObject]
    public class Person2
    {
        [Key(0)]
        public string UserName { get; set; }
        [Key(1)]
        public int Age { get; set; }
        [Key(2)]
        public DateTime Time { get; set; }
    }
}
