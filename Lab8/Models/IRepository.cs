using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8.Models
{
    internal interface IRepository<T>
    {
        Task<T> CreateAsync(T item);
        Task<T> ReadAsync(int id);
        Task<bool> UpdateAsync(T item);
        Task<bool> DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
