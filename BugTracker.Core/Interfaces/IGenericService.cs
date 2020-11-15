using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Core.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> FindOne(int id);
        Task<int> Delete(int id);
        Task<int> Update(T entity);
        Task<int> Create(T entity);
    }
}
