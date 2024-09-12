using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using searchService.Models;
using searchService.Services;

namespace searchService
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {

        private readonly StudentsService _studentService;

        public StudentController(StudentsService studentService)
        {
            _studentService = studentService;
        }
        
        [HttpGet]
        public ActionResult<List<Student>> Get() =>
            _studentService.Get();
        
        [HttpGet("{UUID}", Name = "GetStudent")]
        public ActionResult<Student> GetStudent(string UUID)
        {
            var student = _studentService.Get(UUID);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            _studentService.Create(student);

            return Ok(new { message = "Student created successfully" });
        }

        [HttpPut("{UUID}")]
        public IActionResult UpdateGrade(string UUID, Grade grade)
        {
            var student = _studentService.Get(UUID);

            if (student == null)
            {
                return NotFound();
            }

            _studentService.UpdateGrade(UUID, grade);

            return Ok(new { message = "Student updated successfully" });
        }

        [HttpDelete("{UUID}")]
        public IActionResult Delete(string UUID)
        {
            var student = _studentService.Get(UUID);

            if (student == null)
            {
                return NotFound();
            }

            _studentService.Remove(student);

            return Ok(new { message = "Student deleted successfully" });
        }
    }


}