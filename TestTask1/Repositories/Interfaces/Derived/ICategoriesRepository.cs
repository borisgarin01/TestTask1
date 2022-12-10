using System;
using TestTask1.Models;
using TestTask1.Repositories.Interfaces.Base;

namespace TestTask1.Repositories.Interfaces.Derived
{
    public interface ICategoriesRepository : IRepository<Category>
    {
    }
}
