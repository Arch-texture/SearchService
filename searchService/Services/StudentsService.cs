using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using searchService.Data;
using searchService.DTOs;
using searchService.Models;
using System.Text.RegularExpressions;
using MongoDB.Bson;

namespace searchService.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Student> _students;

        private readonly GradesService _gradesService;
        private readonly RestrictionService _restrictionsService;

        public StudentsService(SearchServiceDatabaseSettings settings, GradesService gradesService, RestrictionService restrictionService)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
            _gradesService = gradesService;
            _restrictionsService = restrictionService;
        }

        public async Task<List<Student>> GetAsync() =>
            await _students.Find(student => true).ToListAsync();

        public async Task<Student> GetAsync(string id) =>
            await _students.Find<Student>(student => student.UUID == id).FirstOrDefaultAsync();
        public async Task<List<Student>> GetStudentByNameAsync(string name){
            var filter = Builders<Student>.Filter.Regex("Name", new BsonRegularExpression(new Regex(name, RegexOptions.IgnoreCase)));
            return await _students.Find(filter).ToListAsync();
        }

        public async Task<List<Student>> GetStudentByUUIDAsync(string uuid){
            var filter = Builders<Student>.Filter.Regex("UUID", new BsonRegularExpression(uuid, "i"));
            return await _students.Find(filter).ToListAsync();
        }
            

        public async Task<Student> CreateAsync(Student student)
        {
            student.Grades = new List<Grade>();
            student.Restrictions = new List<Restriction>();
            await _students.InsertOneAsync(student);
            return student;
        }

        public async Task AddGradeAsync(string uuid, AddGradeDTO grades)
        {
            var stu = await _students.Find<Student>(student => student.UUID == uuid).FirstOrDefaultAsync();
            Console.WriteLine("Estudiante:" + stu);
            Console.WriteLine("Grades:" + grades.grades);
            foreach (var grade in grades.grades)
            {
                var g = new Grade();
                g.UUID = grade.UUID;
                g.grade = grade.grade;
                g.gradeName = grade.gradeName;
                g.comment = grade.comment;
                stu.Grades.Add(g);
                await _gradesService.CreateAsync(g);
            }
            await _students.ReplaceOneAsync(student => student.UUID == uuid, stu);
        }

        public async Task AddRestrictionAsync(AddRestrictionDTO restriction)
        {
            foreach (var studentUUID in restriction.studentsUUID)
            {
                var stu = await _students.Find<Student>(student => student.UUID == studentUUID).FirstOrDefaultAsync();
                var res = new Restriction();
                res.UUID = restriction.restrictionUUID;
                res.Reason = restriction.reason;
                res.CreationDate = DateTime.Now;
                stu.Restrictions.Add(res);
                await _students.ReplaceOneAsync(student => student.UUID == studentUUID, stu);
            }
            var resBD = new Restriction();
            resBD.UUID = restriction.restrictionUUID;
            resBD.Reason = restriction.reason;
            await _restrictionsService.CreateAsync(resBD);
        }

        public async Task DeleteRestrictionToStudentAsync(string uuid)
        {
            // Elimina la restricción de la colección de restricciones
            await _restrictionsService.DeleteAsync(uuid);

            // Filtrar estudiantes que tengan esa restricción
            var filter = Builders<Student>.Filter.ElemMatch(student => student.Restrictions, restriction => restriction.UUID == uuid);
            var studentsWithRestriction = await _students.Find(filter).ToListAsync();

            foreach (var student in studentsWithRestriction)
            {
                var restrictionToRemove = student.Restrictions.Find(restriction => restriction.UUID == uuid);
                if (restrictionToRemove != null)
                {
                    // Elimina la restricción de la lista de restricciones del estudiante
                    student.Restrictions.Remove(restrictionToRemove);

                    // Actualiza solo el campo de restricciones en lugar de reemplazar todo el documento
                    var update = Builders<Student>.Update.Set(s => s.Restrictions, student.Restrictions);
                    _students.UpdateOne(s => s.UUID == student.UUID, update);
                }
            }
        }
        public void Remove(Student studentIn) =>
            _students.DeleteOne(student => student.UUID == studentIn.UUID);

    }
}