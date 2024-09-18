using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using searchService.DTOs;
using searchService.Models;

namespace searchService.Services
{
    public class SearchService
    {
        private readonly StudentsService _studentsService;
        private readonly RestrictionService _restrictionsService;

        public SearchService(StudentsService studentsService, RestrictionService restrictionsService)
        {
            _studentsService = studentsService;
            _restrictionsService = restrictionsService;
        }

        //función para buscar un estudiante y entregar toda su información
        public async Task<List<Student>> SearchStudent(string keyboardEnter)
        {
            var studentsByName = await _studentsService.GetStudentByNameAsync(keyboardEnter);
            if(studentsByName.Count > 0)
            {
                return studentsByName;
            }
            var studentByUUID = await _studentsService.GetStudentByUUIDAsync(keyboardEnter);
            if(studentByUUID != null)
            {
                return studentByUUID;
            }
            return null;
            
        }

        public async Task<List<StudentRestrictionDTO>> SearchByRestriction(string keyboardEnter){
            var restrictionByName = await _restrictionsService.GetRestrictionByNameAsync(keyboardEnter);
            if(restrictionByName.Count > 0){
                var allStudents = await _studentsService.GetAsync();
                var response =  new List<StudentRestrictionDTO>();

                foreach (var restriction in restrictionByName)
                {
                    foreach (var student in allStudents)
                    {
                        if(student.Restrictions.Any(r => r.UUID == restriction.UUID))
                        {
                            var studentRestriction = new StudentRestrictionDTO();
                            studentRestriction.UUID = student.UUID;
                            studentRestriction.Name = student.Name;
                            studentRestriction.LastName = student.LastName;
                            studentRestriction.Email = student.Email;
                            studentRestriction.UUIDRestriction = restriction.UUID;
                            studentRestriction.Reason = restriction.Reason;
                            studentRestriction.CreationDate = restriction.CreationDate;
                            response.Add(studentRestriction);
                        }
                    }
                }
                return response;
            }
            var restrictionByUUID = await _restrictionsService.GetRestrictionByUUIDAsync(keyboardEnter);
            if(restrictionByUUID.Count > 0){
                var allStudents = await _studentsService.GetAsync();
                var response =  new List<StudentRestrictionDTO>();

                foreach (var restriction in restrictionByUUID)
                {
                    foreach (var student in allStudents)
                    {
                        if(student.Restrictions.Any(r => r.UUID == restriction.UUID))
                        {
                            var studentRestriction = new StudentRestrictionDTO();
                            studentRestriction.UUID = student.UUID;
                            studentRestriction.Name = student.Name;
                            studentRestriction.LastName = student.LastName;
                            studentRestriction.Email = student.Email;
                            studentRestriction.UUIDRestriction = restriction.UUID;
                            studentRestriction.Reason = restriction.Reason;
                            studentRestriction.CreationDate = restriction.CreationDate;
                            response.Add(studentRestriction);
                        }
                    }
                }
                return response;
            }
            return null;
        }

        public async Task<List<SearchByGradeDTO>> SearchByGradeMinAndMax(float? min, float? max){
            var allStudents = await _studentsService.GetAsync();
            var response = new List<SearchByGradeDTO>();
            if (min > 0 && min <= 7 && max > 0 && max <=7)
            {
                foreach (var student in allStudents)
                {
                    foreach (var grade in student.Grades)
                    {
                        if(grade.grade >= min && grade.grade <= max)
                        {
                            var studentGrade = new SearchByGradeDTO();
                            studentGrade.grades = new List<GradeDTO>();
                            studentGrade.UUIDStudent = student.UUID;
                            studentGrade.Name = student.Name;
                            studentGrade.LastName = student.LastName;
                            studentGrade.Email = student.Email;
                            var gradeDTO = new GradeDTO();
                            gradeDTO.grade = grade.grade;
                            gradeDTO.UUID = grade.UUID;
                            gradeDTO.comment = grade.comment;
                            gradeDTO.subjectName = grade.subject;
                            gradeDTO.gradeName = grade.gradeName;
                            studentGrade.grades.Add(gradeDTO);
                            response.Add(studentGrade);
                        }
                    }
                }
                return response;
            }
            else if(min == 0 && max > 0 && max <=7){
                 foreach (var student in allStudents)
                {
                    foreach (var grade in student.Grades)
                    {
                        if (grade.grade <= max)
                        {
                            var studentGrade = new SearchByGradeDTO();
                            studentGrade.grades = new List<GradeDTO>();
                            studentGrade.UUIDStudent = student.UUID;
                            studentGrade.Name = student.Name;
                            studentGrade.LastName = student.LastName;
                            studentGrade.Email = student.Email;
                            var gradeDTO = new GradeDTO();
                            gradeDTO.grade = grade.grade;
                            gradeDTO.UUID = grade.UUID;
                            gradeDTO.comment = grade.comment;
                            gradeDTO.subjectName = grade.subject;
                            gradeDTO.gradeName = grade.gradeName;
                            studentGrade.grades.Add(gradeDTO);
                            response.Add(studentGrade);
                        }
                    }
                }
                return response;
            }
            else if(min > 0 && min <= 7 && max == 0){
                 foreach (var student in allStudents)
                {
                    foreach (var grade in student.Grades)
                    {
                        if (grade.grade >= min)
                        {
                            var studentGrade = new SearchByGradeDTO();
                            studentGrade.grades = new List<GradeDTO>();
                            studentGrade.UUIDStudent = student.UUID;
                            studentGrade.Name = student.Name;
                            studentGrade.LastName = student.LastName;
                            studentGrade.Email = student.Email;
                            var gradeDTO = new GradeDTO();
                            gradeDTO.grade = grade.grade;
                            gradeDTO.UUID = grade.UUID;
                            gradeDTO.comment = grade.comment;
                            gradeDTO.subjectName = grade.subject;
                            gradeDTO.gradeName = grade.gradeName;
                            studentGrade.grades.Add(gradeDTO);
                            response.Add(studentGrade);
                        }
                    }
                }
                return response;
            }
            return null;
    }
}
}
