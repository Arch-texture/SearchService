using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using searchService.Services;

namespace searchService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("searchStudent/{keyboardEnter}")]
        public async Task<IActionResult> SearchStudent(string keyboardEnter)
        {
            var student = await _searchService.SearchStudent(keyboardEnter);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpGet("searchByRestriction/{keyboardEnter}")]
        public async Task<IActionResult> SearchByRestriction(string keyboardEnter)
        {
            var student = await _searchService.SearchByRestriction(keyboardEnter);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

    }
}