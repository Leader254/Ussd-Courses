using System;
using System.Collections.Generic;
using System.IO;
using JituCourses.Models;

namespace JituCourses.Plans
{
    public class CoursePurchase
    {
        public int Amount = 0;

        CourseService courseService = new CourseService();
        List<CoursePlansDTO> coursePlans = new List<CoursePlansDTO>();
        private int Id = 0;

        public CoursePurchase()
        {
            coursePlans = courseService.GetCoursePlans();
        }

        public void PurchaseCourse(int selectedOption)
        {
            Id = selectedOption;

            var option = coursePlans.Find(x => x.Id == selectedOption);
            Console.WriteLine($"Do you want to purchase {option.CourseName} for {option.CoursePrice}, enter yes to proceed");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            var answer = Console.ReadLine();

            if (answer == "1")
            {
                CheckBalanceAndPurchase();
            }
            else if (answer == "2")
            {
                return;
            }
            else
            {
                Console.WriteLine("Enter a valid option");
                PurchaseCourse(Id);
            }
        }

        public void CheckBalanceAndPurchase()
        {
            var option = coursePlans.Find(x => x.Id == Id);
            if (option.CoursePrice > Amount)
            {
                Console.WriteLine("Please TopUp to purchase");
                TopUpBalance();
            }
            else
            {
                BuyCourse();
            }
        }

        public void TopUpBalance(string message = "Enter amount to topup: ")
        {
            Console.Write(message);
            var amountInput = Console.ReadLine();
            var amountInt = Validation(amountInput);

            if (amountInt != 0)
            {
                Amount += amountInt;
                ContinuePurchase();
            }
        }

        public void ContinuePurchase()
        {
            var option = coursePlans.Find(x => x.Id == Id);
            if (option.CoursePrice > Amount)
            {
                TopUpBalance("You have insufficient balance to complete this transaction \nPlease enter an amount to continue: ");
            }
            else
            {
                BuyCourse();
            }
        }

        public void BuyCourse()
        {
            var option = coursePlans.Find(x => x.Id == Id);
            Amount -= option.CoursePrice;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Congratulations! You've enrolled in the {option.CourseName} course.");
            Console.ResetColor();

            // Store course enrollment in analytics
            StoreCourseInAnalytics(option.CourseName);

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private void StoreCourseInAnalytics(string courseName)
        {
            try
            {
                string filePath = "analytics.txt";

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine($"{DateTime.Now} - Course Enrolled: {courseName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error storing course in analytics: {ex.Message}");
            }
        }

        private int Validation(string input)
        {
            int value;
            if (int.TryParse(input, out value) && value >= 0)
            {
                return value;
            }
            else
            {
                Console.WriteLine("Enter a valid integer value.");
                return 0;
            }
        }
    }
}
