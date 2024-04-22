using Portal.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Repository.Abstractions.Base
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(int page, int size);
        Task<bool> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> AddRange(ICollection<T> entities);
        Task<bool> Delete(Guid id);
    }
}
