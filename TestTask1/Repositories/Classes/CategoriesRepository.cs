using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TestTask1.Models;
using TestTask1.Repositories.Interfaces.Base;
using TestTask1.Repositories.Interfaces.Derived;

namespace TestTask1.Repositories.Classes
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IConfiguration Configuration;
        private readonly string _connectionString;

        public CategoriesRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetSection("ConnectionString").Value;
        }

        public async Task AddAsync(Category category)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync($"INSERT INTO Categories(Name) VALUES(@Name);", category);
            }
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryAsync<Category>($"SELECT * FROM Categories;");
            }
        }

        public async Task<Category> GetAsync(long id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<Category>($"SELECT * FROM Categories WHERE Id=@id LIMIT 1;", new { id });
            }
        }

        public async Task RemoveAsync(long id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync($"DELETE FROM Categories WHERE Id = @id", new { id });
            }
        }

        public async Task UpdateAsync(Category category)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync($"UPDATE Categories SET Name = @Name WHERE Id = @Id", category);
            }
        }
    }
}
