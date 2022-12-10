using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestTask1.Models;
using TestTask1.Repositories.Interfaces.Base;

namespace TestTask1.Repositories.Interfaces.Derived
{
    public interface ICarsRepository : IRepository<Car>
    {
        Task<IEnumerable<Car>> GetAllWithCategoriesAsync();
        public Task<Car> GetWithCategoryAsync(long id);
    }
}
