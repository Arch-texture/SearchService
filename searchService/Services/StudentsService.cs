using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using searchService.Data;
using searchService.Models;

namespace searchService.Services
{
    public class StudentsService
    {
        private readonly IMongoCollection<Student> _students;

        public StudentsService(SearchServiceDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _students = database.GetCollection<Student>(settings.StudentsCollectionName);
        }

        public List<Student> Get() =>
            _students.Find(student => true).ToList();
        
        public Student Get(string id) =>
            _students.Find<Student>(student => student.UUID == id).FirstOrDefault();
        
        public Student Create(Student student) 
        {
            student.UUID = Guid.NewGuid().ToString();
            student.Grades = new List<Grade>();
            student.Restrictions = new List<Restriction>();
            _students.InsertOne(student);
            return student;
        }

        public void UpdateGrade(string uuid, Grade grade ){
            var stu = _students.Find<Student>(student => student.UUID == uuid).FirstOrDefault();
            stu.Grades.Add(grade);
            _students.ReplaceOne(student => student.UUID == uuid, stu);
        }

        public void UpdateRestriction(string uuid, Restriction restriction ){
            var stu = _students.Find<Student>(student => student.UUID == uuid).FirstOrDefault();
            stu.Restrictions.Add(restriction);
        }
        
        public void Remove(Student studentIn) =>
            _students.DeleteOne(student => student.UUID == studentIn.UUID);
        
    }
}