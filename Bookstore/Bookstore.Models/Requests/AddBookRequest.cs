using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Models.Requests
{
    public class AddBookRequest
    {
        public string Title { get; init; }
        public int AuthorId { get; init; }
    }
}
