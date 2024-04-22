using MongoDB.Driver;
using PortalSystem.Models;
using PortalSystem.Repository.Abstractions.Base;
using SharpCompress.Common;


namespace PortalSystem.Repository
{
    public abstract class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        protected MongoRepository(IMongoCollection<T> collection)
        {
            _collection = collection;
        }

        public async Task<bool> Add(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return true;
        }

        public async Task<bool> AddRange(ICollection<T> entities)
        {
            await _collection.InsertManyAsync(entities);
            return true;
        }

        public async Task<bool> Delete(Guid id)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity does not have an Id property.");

            var filter = Builders<T>.Filter.Eq("_id", id);

            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount == 1;
        }

        public async Task<List<T>> GetAllAsync(int page, int size)
        {
            var skip = (page - 1) * size;
            return await _collection.Find(_ => true).Skip(skip).Limit(size).ToListAsync();
        }

        public async Task<bool> Update(T entity)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty == null)
                throw new InvalidOperationException("Entity does not have an Id property.");

            var id = idProperty.GetValue(entity, null);
            var filter = Builders<T>.Filter.Eq("_id", id);

            var result = await _collection.ReplaceOneAsync(filter, entity);
            return result.ModifiedCount >= 1;
        }
    }
}
