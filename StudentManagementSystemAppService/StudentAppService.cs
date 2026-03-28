using System;
using System.Collections.Generic;
using StudentManagementSystemModels;
using StudentManagementSystemDataService;

namespace StudentManagementSystemAppService
{
    public class StudentAppService
    {
        private StudentDataService dataService = new StudentDataService();

        
        public void AddStudent()
        {
            Console.Write("Enter Student Name: ");
            string newName = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Name cannot be empty.\n");
                return;
            }

            Student student = new Student();
            student.Name = newName;
            student.Status = "Not yet Enrolled!";

            
            dataService.AddStudent(student);
            Console.WriteLine("Successfully Saved to Database!!\n");
        }

        
        public void SearchStudent()
        {
            Console.Write("Search Student Name: ");
            string search = Console.ReadLine() ?? "";

            
            Student foundStudent = dataService.SearchStudentInDb(search);

            if (foundStudent != null)
            {
                Console.WriteLine("\n--- Student Found ---");
                Console.WriteLine($"ID: {foundStudent.StudentID}");
                Console.WriteLine($"Name: {foundStudent.Name}");
                Console.WriteLine($"Status: {foundStudent.Status}\n");
            }
            else
            {
                Console.WriteLine($"Student '{search}' does not exist.\n");
            }
        }

        
        public void UpdateStudentStatus()
        {
            
            ViewStudents();

            Console.Write("\nEnter Student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int targetId))
            {
                Console.WriteLine("Invalid ID. Please enter a numeric ID.\n");
                return;
            }

            Console.WriteLine("\nChoose a new status:");
            Console.WriteLine("1. Enroll");
            Console.WriteLine("2. UnEnroll");
            Console.WriteLine("3. Apply");
            Console.WriteLine("4. Drop");
            Console.WriteLine("5. Transferee");
            Console.WriteLine("6. Waitlist");
            Console.WriteLine("7. Deactivate");
            Console.Write("Choice (1-7): ");

            string choice = Console.ReadLine() ?? "";
            string newStatus = choice switch
            {
                "1" => "Enrolled",
                "2" => "UnEnrolled",
                "3" => "Applied",
                "4" => "Dropped",
                "5" => "Transferee",
                "6" => "Waitlisted",
                "7" => "Deactivated",
                _ => ""
            };

            if (string.IsNullOrEmpty(newStatus))
            {
                Console.WriteLine("Invalid choice. Update cancelled.\n");
                return;
            }

            
            dataService.UpdateStatusById(targetId, newStatus);
            Console.WriteLine($"Successfully updated Student ID {targetId} to '{newStatus}'.\n");
        }

        
        public void ViewStudents()
        {
            List<Student> students = dataService.GetStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No students found in the database.");
                return;
            }

            Console.WriteLine("\n--- Student List (from MS SQL) ---");
            foreach (var s in students)
            {
                
                Console.WriteLine($"ID: {s.StudentID} | Name: {s.Name} | Status: {s.Status}");
            }
        }
        public void RemoveStudent()
        {
            ViewStudents(); 

            Console.Write("\nEnter Student ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.\n");
                return;
            }

            
            Console.Write("Are you sure you want to remove this student from this history log? (Y/N): ");
            string confirmation = Console.ReadLine()?.ToUpper() ?? "";

            if (confirmation == "Y")
            {
                dataService.DeleteStudentById(id);
                Console.WriteLine("Student removed successfully.\n");
            }
            else
            {
                Console.WriteLine("Removal cancelled.\n");
            }
        }
    }
}