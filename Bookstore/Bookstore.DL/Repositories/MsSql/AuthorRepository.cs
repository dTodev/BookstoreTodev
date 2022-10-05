using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Models;
using Bookstore.Models.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bookstore.DL.Repositories.MsSql
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ILogger<AuthorRepository> _logger;
        private readonly IConfiguration _configuration;

        public AuthorRepository(ILogger<AuthorRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task <Author> AddAuthor(Author user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("INSERT INTO [Authors] (Name, Age, DateOfBirth, NickName) VALUES(@Name, @Age, @DateOfBirth, @NickName)", user);

                    return user;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddAuthor)}: {e.Message}", e);
            }
            return new Author();
        }

        public async Task <bool>AddMultipleAuthors(IEnumerable<Author> authorCollection)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    
                    var result = await conn.ExecuteAsync("INSERT INTO [Authors] (Name, Age, DateOfBirth, NickName) VALUES(@Name, @Age, @DateOfBirth, @NickName)", authorCollection);

                    return result > 0;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(AddMultipleAuthors)}: {e.Message}", e);
            }
            return false;
        }

        public async Task <Author> DeleteAuthor(int userId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var temp = await GetById(userId);

                    var result = await conn.ExecuteAsync("DELETE FROM [Authors] WHERE Id = @Id", temp);

                    return temp;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(DeleteAuthor)}: {e.Message}", e);
            }
            return new Author();
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryAsync<Author>("SELECT * FROM Authors WITH(NOLOCK)");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllAuthors)}: {e.Message}", e);
            }
            return Enumerable.Empty<Author>();
        }

        public async Task <Author> GetAuthorByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Name = @Name", new { Name = name });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAuthorByName)}: {e.Message}", e);
            }
            return new Author();
        }

        public async Task <Author?> GetById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    return await conn.QueryFirstOrDefaultAsync<Author>("SELECT * FROM Authors WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetById)}: {e.Message}", e);
            }
            return new Author();
        }

        public async Task <Author> UpdateAuthor(Author user)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();

                    var result = await conn.ExecuteAsync("UPDATE [Authors] SET Name = @Name, Age = @Age, DateOfBirth = @DateOfBirth, NickName = @NickName WHERE Id = @Id", user);

                    return user;
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(UpdateAuthor)}: {e.Message}", e);
            }
            return new Author();
        }
    }
}
