using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace searchService.DTOs
{
    public class StudentRestrictionDTO
    {
        public string UUID { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string UUIDRestriction { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public DateTime CreationDate { get; set; }
    }
}