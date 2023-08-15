using JituCourses.Models;
using JituCourses.Plans;

namespace JituCourses.Auth
{
    public class CourseEnrollment
    {
        public void ShowAvailableCourses(UserType userType)
        {
            CourseService courseService = new CourseService();
            List<CoursePlansDTO> courses = courseService.GetCoursePlans();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Available Courses:");
            Console.ResetColor();
            foreach (CoursePlansDTO course in courses)
            {
                Console.WriteLine($"{course.Id}. {course.CourseName} - {course.CourseDescription} - {course.CoursePrice}");
            }

            int chosenCourseId;
            string input;
            do
            {
                Console.Write("Choose a course to enroll: ");
                input = Console.ReadLine();

                if (IsValidOption(input, courses.Count, out chosenCourseId))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Enter a valid course ID.");
                }
            } while (true);


            PurchaseCourse(userType, chosenCourseId);
        }

        public bool IsValidOption(string input, int limit, out int option)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid Choice, please choose an option");
                option = 0;
                return false;
            }

            if (int.TryParse(input, out option) && option > 0 && option <= limit)
            {
                return true;
            }

            return false;
        }

        private void PurchaseCourse(UserType userType, int courseId)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Enrolling in course with ID {courseId}...");
            Console.ResetColor();

            CoursePurchase coursePurchase = new CoursePurchase();
            coursePurchase.PurchaseCourse(courseId);
        }
    }
}
