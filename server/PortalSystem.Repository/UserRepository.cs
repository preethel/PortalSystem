using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Portal.Database;
using PortalSystem.Models;
using PortalSystem.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Repository
{
    public class UserRepository : MongoRepository<User>, IUserRepository
    {
        //ApplicationDbContext _db;
        //public UserRepository(ApplicationDbContext db) : base(db)
        //{
        //    _db = db;
        //}
        //private DbSet<User> Table
        //{
        //    get
        //    {
        //        return _db.Set<User>();
        //    }
        //}
        //public Task<User> GetByEmail(string email)
        //{
        //    return Table.FirstOrDefaultAsync(x => x.Email == email);
        //}
        //public Task<User> GetById(Guid Id)
        //{
        //    return Table.FirstOrDefaultAsync(x => x.Id == Id);
        //}
        private readonly IMongoCollection<User> _collection;
        public UserRepository(IMongoCollection<User> collection) : base(collection)
        {
            _collection = collection;
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _collection.Find(x=>x.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User> GetById(Guid Id)
        {
            return await _collection.Find(x => x.Id == Id).FirstOrDefaultAsync();
        }
    }
}
