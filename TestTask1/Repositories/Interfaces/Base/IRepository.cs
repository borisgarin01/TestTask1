using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask1.Repositories.Interfaces.Base
{
    public interface IRepository<T>
    {
        public Task<IEnumerable<T>> GetAllAsync();
        public Task AddAsync(T t);
        public Task<T> GetAsync(long id);
        public Task UpdateAsync(T t);
        public Task RemoveAsync(long id);
    }
}
