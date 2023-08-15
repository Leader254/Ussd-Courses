using JituCourses.Models;
using System;
using System.IO;
using JituCourses.Plans;
using System.Collections.Generic;
using System.Linq;



namespace JituCourses.Auth
{
    public class AuthenticationOptions
    {

        public Authentication ShowOptions()
        {
            Console.WriteLine("Authentication Options:");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");

            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            if (IsValidOption(input, 2, out int option))
            {
                switch (option)
                {
                    case (int)Authentication.Login:
                        ShowLoginOptions();
                        break;
                    case (int)Authentication.Register:
                        HandleRegister();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        ShowOptions();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
                ShowOptions();
            }

            return Authentication.Login;
        }

        // show login options
        public UserType ShowLoginOptions()
        {
            Console.WriteLine("Login Options:");
            Console.WriteLine("1. User");
            Console.WriteLine("2. Admin");

            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            if (IsValidOption(input, 2, out int option))
            {
                switch (option)
                {
                    case (int)UserType.User:
                        HandleLogin(UserType.User);
                        break;
                    case (int)UserType.Admin:
                        HandleLogin(UserType.Admin);
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        ShowLoginOptions();
                        break;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input.");
                Console.ResetColor();
                ShowLoginOptions();
            }

            return UserType.User;
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


        // Handle login user logic
        private void HandleLogin(UserType userType)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter your username: ");
            Console.ResetColor();
            string username = Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter your password: ");
            Console.ResetColor();
            string password = Console.ReadLine();

            string filePath = "users.txt";
            bool found = false;

            try
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4 && parts[2] == username && parts[3] == password)
                    {
                        found = true;
                        if (userType == UserType.User)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("User logged in!!");
                            Console.ResetColor();
                            CourseEnrollment courseEnrollment = new CourseEnrollment();
                            courseEnrollment.ShowAvailableCourses(UserType.User);
                        }
                        else if (userType == UserType.Admin)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Admin logged in!!");
                            Console.ResetColor();
                            ShowAdminMenu();
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading user data: {ex.Message}");
                return;
            }

            if (!found)
            {
                Console.WriteLine("Invalid credentials. Please try again.");
                ShowLoginOptions();
            }
        }

        private void ShowAdminMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Admin Menu:");
            Console.ResetColor();
            Console.WriteLine("1. Create Course");
            Console.WriteLine("2. Update Course");
            Console.WriteLine("3. Delete Course");
            Console.WriteLine("4. View Courses");
            Console.WriteLine("5. View Analytics");

            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            CourseCRUD allCourses = new CourseCRUD();

            if (IsValidOption(input, 5, out int option))
            {
                switch (option)
                {
                    case 1:
                        allCourses.CreateCourse();
                        break;
                    case 2:
                        allCourses.UpdateCourse();
                        break;
                    case 3:
                        allCourses.DeleteCourse();
                        break;
                    case 4:
                        allCourses.ViewCourses();
                        break;
                    case 5:
                        allCourses.ViewAnalytics();
                        break;
                    default:
                        Console.WriteLine("Invalid option.");
                        ShowAdminMenu();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
                ShowAdminMenu();
            }
        }

        // RegUserDTO newUser = new RegUserDTO();

        // Register user logic
        public void HandleRegister()
        {
            try
            {
                string filePath = "users.txt";
                List<string> lines = File.ReadAllLines(filePath).ToList();

                // Get the last user ID and increment it
                int lastId = 0;
                if (lines.Count > 0)
                {
                    string lastLine = lines.Last();
                    string[] parts = lastLine.Split(',');
                    lastId = int.Parse(parts[0]);
                }
                int newId = lastId + 1;

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter your email: ");
                Console.ResetColor();
                // check if email is valid
                string email = Console.ReadLine();
                // if (IsValidEmail(Console.ReadLine()) == false)
                // {
                //     Console.ForegroundColor = ConsoleColor.Red;2
                //     Console.WriteLine("Invalid email address.");
                //     Console.ResetColor();
                //     HandleRegister();
                // }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter yours username: ");
                Console.ResetColor();
                string username = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enter your password: ");
                Console.ResetColor();
                string password = Console.ReadLine();

                string newUserLine = $"{newId}, {email},{username},{password}";
                lines.Add(newUserLine);

                File.WriteAllLines(filePath, lines);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("User registered successfully!!.");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error storing user: {ex.Message}");
            }
        }

        // check if email is valid
        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

}
