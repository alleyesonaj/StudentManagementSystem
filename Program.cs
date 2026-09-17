using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using StudentManagementSystemAppService;
using StudentManagementSystemEmailService;

namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // --- Load appsettings.json ---
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string connectionString =
                configuration.GetConnectionString("DefaultConnection") ?? "";
            string notifyEmail =
                configuration["EmailSettings:NotifyEmail"] ?? "";

            // --- Wire up dependencies ---
            EmailService emailService = new EmailService(configuration);
            StudentAppService appService =
                new StudentAppService(connectionString, emailService, notifyEmail);

            // --- Menu loop ---
            while (true)
            {
                Console.WriteLine("\n--- Student Management System ---");
                Console.WriteLine("1. Add a Student");
                Console.WriteLine("2. Search a Student");
                Console.WriteLine("3. Update Student Status");
                Console.WriteLine("4. View Lists of Students");
                Console.WriteLine("5. Remove a Student");
                Console.WriteLine("6. Exit");
                Console.Write("Choose: ");

                // TryParse instead of Convert.ToInt32 so letters don't crash the app
                if (!int.TryParse(Console.ReadLine(), out int option))
                {
                    Console.WriteLine("\nInvalid option.\n");
                    continue;
                }

                switch (option)
                {
                    case 1:
                        appService.AddStudent();
                        break;

                    case 2:
                        appService.SearchStudent();
                        break;

                    case 3:
                        appService.UpdateStudentStatus();
                        break;

                    case 4:
                        appService.ViewStudents();
                        break;

                    case 5:
                        appService.RemoveStudent();
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("\nInvalid option.\n");
                        break;
                }
            }
        }
    }
}