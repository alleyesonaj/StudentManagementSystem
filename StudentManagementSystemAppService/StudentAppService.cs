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
            string newName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Name cannot be empty.\n");
                return;
            }

            Student student = new Student();
            student.Name = newName;
            student.Status = "Not yet Enrolled!";

            dataService.AddStudent(student);
            Console.WriteLine("Successfully Saved!!\n");
        }

        public void SearchStudent()
        {
            Console.Write("Search Student: ");
            string search = Console.ReadLine();

            List<Student> students = dataService.GetStudents();

            foreach (Student student in students)
            {
                if (student.Name == search)
                {
                    Console.WriteLine("Name: " + student.Name);
                    Console.WriteLine("Status: " + student.Status);
                    return;
                }
            }

            Console.WriteLine("Student " + search + " does not exist.\n");
        }

        public void UpdateStudentStatus()
        {
            Console.Write("Enter Student Name to update: ");
            string target = Console.ReadLine();

            List<Student> students = dataService.GetStudents();
            Student foundStudent = null;

            foreach (Student student in students)
            {
                if (student.Name == target)
                {
                    foundStudent = student;
                    break;
                }
            }

            if (foundStudent == null)
            {
                Console.WriteLine("Student Name does not exist.\n");
                return;
            }

            Console.WriteLine("\nChoose a new status for student:");
            Console.WriteLine("1. Enroll");
            Console.WriteLine("2. UnEnroll");
            Console.WriteLine("3. Apply");
            Console.WriteLine("4. Drop");
            Console.WriteLine("5. Transferee");
            Console.WriteLine("6. Waitlist");
            Console.WriteLine("7. Deactivate");
            Console.Write("Choice: ");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1) foundStudent.Status = "Enrolled";
            else if (choice == 2) foundStudent.Status = "UnEnrolled";
            else if (choice == 3) foundStudent.Status = "Applied";
            else if (choice == 4) foundStudent.Status = "Dropped";
            else if (choice == 5) foundStudent.Status = "Transferee";
            else if (choice == 6) foundStudent.Status = "Waitlisted";
            else if (choice == 7) foundStudent.Status = "Deactivated";
            else
            {
                Console.WriteLine("Invalid choice.\n");
                return;
            }

            Console.WriteLine("Status updated successfully!");
        }

        public void ViewStudents()
        {
            List<Student> students = dataService.GetStudents();

            if (students.Count == 0)
            {
                Console.WriteLine("No students yet.");
                return;
            }

            Console.WriteLine("\n--- Student List ---");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + students[i].Name + " - " + students[i].Status);
            }
        }
    }
}