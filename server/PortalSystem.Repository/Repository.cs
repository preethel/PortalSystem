using Microsoft.EntityFrameworkCore;
using Portal.Database;
using PortalSystem.Repository.Abstractions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        ApplicationDbContext _db;
        public Repository(ApplicationDbContext db) 
        {
            _db = db;
        }
        private DbSet<T> Table
        {
            get
            {
                return _db.Set<T>();
            }
        }
        public Task<List<T>> GetAllAsync(int page, int size)
        {
            return Table.Skip(page).Take(size).ToListAsync();
        }
        public async Task<bool> Add(T entity)
        {
            Table.Add(entity);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<bool> Update(T entity)
        {
            Table.Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateRange(ICollection<T> entity)
        {
            Table.UpdateRange(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public async Task<bool> AddRange(ICollection<T> entities)
        {
            Table.AddRange(entities);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
