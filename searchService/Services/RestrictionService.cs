using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using searchService.Data;
using searchService.Models;

namespace searchService.Services
{
    public class RestrictionService
    {
        private readonly IMongoCollection<Restriction> _restrictionCollection;

        public RestrictionService(SearchServiceDatabaseSettings settings)
        {
            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                throw new ArgumentNullException(nameof(settings.ConnectionString));
            }

            if (string.IsNullOrEmpty(settings.DatabaseName))
            {
                throw new ArgumentNullException(nameof(settings.DatabaseName));
            }

            if (string.IsNullOrEmpty(settings.GradesCollectionName))
            {
                throw new ArgumentNullException(nameof(settings.GradesCollectionName));
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _restrictionCollection = database.GetCollection<Restriction>(settings.RestrictionsCollectionName);
        }

        public async Task<List<Restriction>> GetAsync() =>
            await _restrictionCollection.Find(restriction => true).ToListAsync();
        
        public async Task<Restriction> GetAsync(string id) =>
            await _restrictionCollection.Find<Restriction>(restriction => restriction.UUID == id).FirstOrDefaultAsync();
        
        public async Task<Restriction> CreateAsync(Restriction restriction)
        {
            await _restrictionCollection.InsertOneAsync(restriction);
            return restriction;
        }

        public async Task DeleteAsync(string UUID)
        {
            var filter = Builders<Restriction>.Filter.Eq("UUID", UUID);
            await _restrictionCollection.DeleteOneAsync(filter);
        }
    }
}