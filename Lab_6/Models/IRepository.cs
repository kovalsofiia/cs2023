using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6.Models
{
    internal interface IRepository<T>
    {
        T Create(T item);
        T Read(int id);
        bool Update(T item);
        bool Delete(int id);
        int Count();
    }
}
