using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JituCourses.Models;

namespace JituCourses.Plans
{
    public class CourseCRUD
    {
        public void CreateCourse()
        {
            Console.WriteLine("Creating a course...");
            string filePath = "courses.txt";

            try
            {
                List<string> lines = File.ReadAllLines(filePath).ToList();

                // Get the last course ID and increment it
                int lastId = 0;
                if (lines.Count > 0)
                {
                    string lastLine = lines.Last();
                    string[] parts = lastLine.Split(',');
                    lastId = int.Parse(parts[0]);
                }
                int newId = lastId + 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter course name: ");
                Console.ResetColor();
                string courseName = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter course description: ");
                Console.ResetColor();
                string courseDescription = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter course price: ");
                Console.ResetColor();
                string coursePrice = Console.ReadLine();

                string newCourseLine = $"{newId}, {courseName},{courseDescription},{coursePrice}";
                lines.Add(newCourseLine);

                File.WriteAllLines(filePath, lines);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"{courseName} Course added successfully!!.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error storing courses: {ex.Message}");
            }
        }
        public void ViewCourses()
        {
            CourseService courseService = new CourseService();
            List<CoursePlansDTO> coursePlans = courseService.GetCoursePlans();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("List of Courses:");
            Console.ResetColor();

            foreach (var course in coursePlans)
            {
                Console.WriteLine($"{course.Id}, {course.CourseName}, Description: {course.CourseDescription}, Price: {course.CoursePrice}");
            }
        }

        public void UpdateCourse()
        {
            Console.Write("Enter course ID to update: ");
            string courseId = Console.ReadLine();

            try
            {
                string filePath = "courses.txt";
                List<string> lines = File.ReadAllLines(filePath).ToList();
                bool courseUpdated = false;

                for (int i = 0; i < lines.Count; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 4 && parts[0] == courseId)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter course name: ");
                        Console.ResetColor();
                        string courseName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(courseName))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Course name cannot be empty.");
                            Console.ResetColor();
                            UpdateCourse();
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter course description: ");
                        Console.ResetColor();
                        string courseDescription = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(courseDescription))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Course description cannot be empty.");
                            Console.ResetColor();
                            UpdateCourse();
                        }

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("Enter course price: ");
                        Console.ResetColor();
                        string coursePrice = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(coursePrice))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Course price cannot be empty.");
                            Console.ResetColor();
                            UpdateCourse();
                        }

                        string newCourseLine = $"{courseId}, {courseName},{courseDescription},{coursePrice}";
                        lines[i] = newCourseLine;  // Update the line in the list
                        courseUpdated = true;  // Mark course as updated
                        break;  // Exit the loop
                    }
                }

                if (courseUpdated)
                {
                    File.WriteAllLines(filePath, lines);  // Write the modified lines back to the file
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Course updated successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Course with ID {courseId} not found.\nPlease enter a valid course ID.");
                    Console.ResetColor();
                    // int id = int.Parse(Console.ReadLine());
                    UpdateCourse();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating course: {ex.Message}");
            }
        }

        public void DeleteCourse()
        {
            Console.Write("Enter course ID to delete: ");
            string courseId = Console.ReadLine();

            try
            {
                string filePath = "courses.txt";
                List<string> lines = File.ReadAllLines(filePath).ToList();
                bool courseDeleted = false;

                for (int i = 0; i < lines.Count; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length == 4 && parts[0] == courseId)
                    {
                        lines.RemoveAt(i);  // Remove the line from the list
                        courseDeleted = true;  // Mark course as deleted
                        break;  // Exit the loop
                    }
                }

                if (courseDeleted)
                {
                    File.WriteAllLines(filePath, lines);  // Write the modified lines back to the file
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Course deleted successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Course with ID {courseId} not found.\nPlease enter a valid course ID.");
                    Console.ResetColor();
                    // int id = int.Parse(Console.ReadLine());
                    DeleteCourse();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting course: {ex.Message}");
            }
        }

        public void ViewAnalytics()
        {
            try
            {
                string filePath = "analytics.txt";
                string[] lines = File.ReadAllLines(filePath);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Analytics Data:");
                Console.ResetColor();

                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading analytics data: {ex.Message}");
            }
        }


    }
}