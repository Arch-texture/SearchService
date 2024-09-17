using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using searchService.Data;
using searchService.Models;

namespace searchService.Services
{
    public class GradesService
    {
        private readonly IMongoCollection<Grade> _gradesCollection;
        private readonly ILogger<GradesService> _logger;

        public GradesService(SearchServiceDatabaseSettings settings, ILogger<GradesService> logger)
        {
            _logger = logger;

            if (string.IsNullOrEmpty(settings.ConnectionString))
            {
                _logger.LogError("ConnectionString is null or empty.");
                throw new ArgumentNullException(nameof(settings.ConnectionString));
            }

            if (string.IsNullOrEmpty(settings.DatabaseName))
            {
                _logger.LogError("DatabaseName is null or empty.");
                throw new ArgumentNullException(nameof(settings.DatabaseName));
            }

            if (string.IsNullOrEmpty(settings.GradesCollectionName))
            {
                _logger.LogError($"GradesCollectionName is null or empty.{settings.GradesCollectionName}");
                _logger.LogError($"TeachersCollectionName is null or empty.{settings.TeachersCollectionName}");
                _logger.LogError($"StudentsCollectionName is null or empty.{settings.StudentsCollectionName}");
                _logger.LogError($"CoursesCollectionName is null or empty.{settings.SubjectsCollectionName}");
                
                throw new ArgumentNullException(nameof(settings.GradesCollectionName));
            }

            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _gradesCollection = database.GetCollection<Grade>(settings.GradesCollectionName);

            _logger.LogInformation("GradesService initialized successfully.");
        }

        public async Task<List<Grade>> GetAsync() =>
            await _gradesCollection.Find(grade => true).ToListAsync();
        
        public async Task<Grade> GetAsync(string id) =>
            await _gradesCollection.Find<Grade>(grade => grade.UUID == id).FirstOrDefaultAsync();
        
        public async Task<Grade> CreateAsync(Grade grade)
        {
            await _gradesCollection.InsertOneAsync(grade);
            return grade;
        }

        public async Task UpdateAsync(string id, Grade gradeIn) =>
            await _gradesCollection.ReplaceOneAsync(grade => grade.UUID == id, gradeIn);

        public async Task RemoveAsync(Grade gradeIn) =>
            await _gradesCollection.DeleteOneAsync(grade => grade.UUID == gradeIn.UUID);
    }
}
