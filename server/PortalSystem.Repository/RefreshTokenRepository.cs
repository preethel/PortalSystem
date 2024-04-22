using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Portal.Database;
using PortalSystem.Models;
using PortalSystem.Repository.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalSystem.Repository
{
    public class RefreshTokenRepository : MongoRepository<RefreshToken>, IRefreshTokenRepository
    {
        //ApplicationDbContext _db;
        //public RefreshTokenRepository(ApplicationDbContext db) : base(db)
        //{
        //    _db = db;
        //}
        //private DbSet<RefreshToken> Table
        //{
        //    get
        //    {
        //        return _db.Set<RefreshToken>();
        //    }
        //}
        //public async Task<RefreshToken> GetRefreshTokenByIdAsync(string id)
        //{
        //    return await Table.FirstOrDefaultAsync(x => x.Token.ToString() == id);
        //}
        private readonly IMongoCollection<RefreshToken> _collection;
        public RefreshTokenRepository(IMongoCollection<RefreshToken> collection) : base(collection)
        {
            _collection = collection;
        }

        public Task<bool> DeleteRefreshTokenByToken(Guid token)
        {
            throw new NotImplementedException();
        }

        public Task<RefreshToken> GetRefreshTokenByIdAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
