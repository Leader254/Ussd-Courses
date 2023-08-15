using System;
using System.Collections.Generic;
using System.IO;
using JituCourses.Models;

namespace JituCourses.Plans
{
    public class CourseService
    {
        public List<CoursePlansDTO> GetCoursePlans()
        {
            List<CoursePlansDTO> coursePlans = new List<CoursePlansDTO>();

            // Read the lines from the text file
            string filePath = "courses.txt";
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length == 4)
                {
                    if (int.TryParse(parts[0], out int id) &&
                        int.TryParse(parts[3], out int coursePrice))
                    {
                        string courseName = parts[1].Trim();
                        string courseDescription = parts[2].Trim();

                        CoursePlansDTO course = new CoursePlansDTO
                        {
                            Id = id,
                            CourseName = courseName,
                            CourseDescription = courseDescription,
                            CoursePrice = coursePrice
                        };

                        coursePlans.Add(course);
                    }
                    else
                    {
                        Console.WriteLine($"Invalid data format: {line}");
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid line format: {line}");
                }
            }

            return coursePlans;
        }
    }
}
