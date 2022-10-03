using AutoMapper;
using Bookstore.Models.Models;
using Bookstore.Models.Requests;
using Bookstore.Models.Responses;

namespace Bookstore.AutoMapper
{
    internal class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AddAuthorRequest, Author>();
            CreateMap<AddBookRequest, Book>();
            CreateMap<UpdateAuthorRequest, Author>();
            CreateMap<UpdateBookRequest, Book>();
        }
    }
}
