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
    public class ClassRepository : MongoRepository<Class>, IClassRepository
    {
        private readonly IMongoCollection<Class> _collection; 
        public ClassRepository(IMongoCollection<Class> collection) : base(collection)
        {
            _collection = collection;
        }

        public async Task<Class> GetById(Guid id)
        {
            return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
