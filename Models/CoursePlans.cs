using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JituCourses.Models
{
    public class CoursePlansDTO
    {
        public int Id { get; set; } = 0;
        public string CourseName { get; set; } = string.Empty;
        public string CourseDescription { get; set; } = string.Empty;
        public int CoursePrice { get; set; } = 0;
    }
}