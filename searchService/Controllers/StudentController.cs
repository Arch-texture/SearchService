using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using searchService.DTOs;
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

        [HttpPost]
        public async Task<ActionResult<Student>> Create(Student student)
        {
            await _studentService.CreateAsync(student);
            return Ok(new { message = "Student created successfully" });
        }

        [HttpPut("AddGrade/{UUID}")]
        public async Task<IActionResult> AddGrade(string UUID, AddGradeDTO grades)
        {
            var student = await _studentService.GetAsync(UUID);

            if (student == null)
            {
                return NotFound();
            }

            await _studentService.AddGradeAsync(UUID, grades);

            return Ok(new { message = "Student updated successfully" });
        }

        [HttpPut("AddRestriction")]
        public async Task<IActionResult> addRestriction(AddRestrictionDTO restriction)
        {
            await _studentService.AddRestrictionAsync(restriction);

            return Ok(new { message = "Students updated successfully" });
        }
        [HttpDelete("deleteStudent/{UUID}")]
        public async Task<IActionResult> Delete(string UUID)
        {
            var student = await _studentService.GetAsync(UUID);

            if (student == null)
            {
                return NotFound();
            }

            _studentService.Remove(student);

            return Ok(new { message = "Student deleted successfully" });
        }

        [HttpDelete("deleteRestriction/{UUID}")]
        public async Task<IActionResult> DeleteRestriction(string UUID)
        {
            try{
                await _studentService.DeleteRestrictionToStudentAsync(UUID);
                return Ok(new { message = "Tamos ok mi fan" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
            
        }
    }


}