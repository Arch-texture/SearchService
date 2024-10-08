using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchService.DTOs
{
    public class SearchByGradeDTO
    {
        public string UUIDStudent { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<GradeDTO> grades { get; set; } = null!;
    }
}