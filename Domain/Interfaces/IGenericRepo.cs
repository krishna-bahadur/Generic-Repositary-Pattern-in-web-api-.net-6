using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IGenericRepo<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        bool Exists();
        void Add(T model);
        void Edit(T model);
        void Delete(int id);
        Task<int> Save();
        

    }
}
