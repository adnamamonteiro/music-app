using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Backend.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid Id);
        Task Save(T obj);
        Task Remove(T obj);
        Task Update(T obj);

    }
}
