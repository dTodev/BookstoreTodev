using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DL.Interfaces;
using Bookstore.Models.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Dapper;

namespace Bookstore.DL.Repositories.MsSql
{
    public class BookRepository : IBookRepository
    {
        private readonly ILogger<BookRepository> _logger;
        private readonly IConfiguration _configuration;

        public BookRepository(ILogger<BookRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Book?> AddBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Books] (AuthorId, Title, LastUpdated, Quantity, Price) VALUES(@AuthorId, @Title, GETDATE(), @Quantity, @Price)", book);

                    return book;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddBook)}: {e.Message}", e);
            }
            return new Book();
        }

        public async Task<bool> AddMultipleBooks(IEnumerable<Book> bookCollection)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Books] (AuthorId, Title, LastUpdated, Quantity, Price) VALUES(@AuthorId, @Title, GETDATE(), @Quantity, @Price)", bookCollection);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddMultipleBooks)}: {e.Message}", e);
            }
            return false;
        }

        public async Task<Book> DeleteBook(int bookId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var temp = await GetById(bookId);

                    var result = await conn.ExecuteAsync("DELETE FROM [Books] WHERE Id = @Id", temp);

                    return temp;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteBook)}: {e.Message}", e);
            }
            return new Book();
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Book>("SELECT * FROM Books WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllBooks)}: {e.Message}", e);
            }
            return Enumerable.Empty<Book>();
        }

        public async Task<Book> GetBookByName(string title)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Title = @Title", new { Title = title });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetBookByName)}: {e.Message}", e);
            }
            return new Book();
        }

        public async Task<Book?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetById)}: {e.Message}", e);
            }
            return new Book();
        }

        public async Task<Book?> GetByAuthorId(int authorId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Book>("SELECT * FROM Books WITH(NOLOCK) WHERE AuthorId = @AuthorId", new { AuthorId = authorId });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetByAuthorId)}: {e.Message}", e);
            }
            return new Book();
        }

        public async Task<Book> UpdateBook(Book book)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Books] SET AuthorId = @AuthorId, Title = @Title, Quantity = @Quantity, Price = @Price WHERE Id = @Id", book);

                    return book;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateBook)}: {e.Message}", e);
            }
            return new Book();
        }
    }
}
