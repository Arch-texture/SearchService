using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchService.DTOs
{
    public class AddRestrictionDTO
    {
        public string restrictionUUID { get; set; } = null!;
        public string reason { get; set; } = null!;
        public List<string> studentsUUID { get; set; } = null!;
    }
}