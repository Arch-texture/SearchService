using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using searchService.DTOs;

namespace searchService.DTOs
{
    public class AddGradeDTO
    {
        public List<GradeDTO> grades { get; set; } = null!;
    }
}