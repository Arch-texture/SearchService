using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using searchService.Models;

namespace searchService.Services
{
    public class SearchService
    {
        private readonly StudentsService _studentsService;

        public SearchService(StudentsService studentsService)
        {
            _studentsService = studentsService;
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
    }
}