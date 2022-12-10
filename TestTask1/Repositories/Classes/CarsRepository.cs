using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TestTask1.Models;
using TestTask1.Repositories.Interfaces.Derived;

namespace TestTask1.Repositories.Classes
{
    public class CarsRepository : ICarsRepository
    {
        private readonly IConfiguration Configuration;
        private readonly string _connectionString;

        public CarsRepository(IConfiguration configuration)
        {
            Configuration = configuration;
            _connectionString = Configuration.GetSection("ConnectionString").Value;
        }

        public async Task AddAsync(Car car)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                if (await db.QueryFirstAsync<int>($"SELECT COUNT(*) FROM Cars WHERE Number='{car.Number}'") == 0)
                {
                    await db.ExecuteAsync($"INSERT INTO Cars(Brand, Model, CategoryId, Number, ReleaseYear) VALUES(@Brand, @Model, @CategoryId, @Number, @ReleaseYear);", car);
                }
            }
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryAsync<Car>($"SELECT * FROM Cars;");
            }
        }

        public async Task<IEnumerable<Car>> GetAllWithCategoriesAsync()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                //return await db.QueryAsync<Car, Category>($"SELECT * FROM Cars INNER JOIN Categories ON Cars.CategoryId=Categories.Id;");
                string sql = @"SELECT * FROM Cars INNER JOIN Categories ON CategoryId=Categories.Id;";
                var cars = await db.QueryAsync<Car, Category, Car>(sql, (car, category) => { car.Category = category; return car; });
                return cars;
            }
        }

        public async Task<Car> GetWithCategoryAsync()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                //return await db.QueryAsync<Car, Category>($"SELECT * FROM Cars INNER JOIN Categories ON Cars.CategoryId=Categories.Id;");
                string sql = @"SELECT * FROM Cars INNER JOIN Categories ON CategoryId=Categories.Id limit 1;";
                var cars = await db.QueryAsync<Car, Category, Car>(sql, (car, category) => { car.Category = category; return car; });
                return cars.FirstOrDefault();
            }
        }

        public async Task<Car> GetAsync(long id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryFirstAsync<Car>($"SELECT * FROM Cars INNER JOIN Categories ON CategoryId=Categories.Id where Cars.id=@id;", new { id });
            }
        }

        public async Task<Car> GetWithCategoryAsync(long id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                string sql = @"SELECT * FROM Cars Inner Join Categories ON Cars.CategoryId=Categories.Id WHERE Cars.Id=@id LIMIT 1;";

                var cars = await db.QueryAsync<Car, Category, Car>(sql, (car, category) => { car.Category = category; return car; });
                return null;
            }
        }

        public async Task RemoveAsync(long id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.QueryFirstAsync<Car>($"DELETE FROM Cars WHERE Id=@id;", new { id });
            }
        }

        public async Task UpdateAsync(Car car)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync($"UPDATE Cars SET Brand = @Brand, Model = @Model, CategoryId = @CategoryId, Number = @Number, ReleaseYear = @ReleaseYear WHERE Id=@Id;", car);
            }
        }
    }
}
