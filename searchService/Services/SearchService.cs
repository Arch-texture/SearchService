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

        
    }
}