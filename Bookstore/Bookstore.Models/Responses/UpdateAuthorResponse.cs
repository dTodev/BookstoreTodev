﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models.Models;

namespace Bookstore.Models.Responses
{
    public class UpdateAuthorResponse : BaseResponse
    {
        public Author Name { get; set; }
    }
}
