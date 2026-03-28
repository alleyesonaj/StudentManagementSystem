using System;
using StudentManagementSystemAppService;

namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentAppService appService = new StudentAppService();

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

                int option = Convert.ToInt32(Console.ReadLine());

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