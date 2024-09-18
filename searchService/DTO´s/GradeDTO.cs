using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchService.DTOs
{
    public class GradeDTO
    {
        public string UUID { get; set; } = null!;
        public float grade { get; set; } = 0!;
        public string gradeName { get; set; } = null!;
        public string subjectName { get; set; } = null!;
        public string comment { get; set; } = null!;
    }
}