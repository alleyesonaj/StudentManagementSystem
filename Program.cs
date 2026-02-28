using System;
using System.Collections;

namespace StudentManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrayList names = new ArrayList();
            ArrayList status = new ArrayList();

            while (true)
            {
                Console.WriteLine("\n--- Student Management System ---");
                Console.WriteLine("1. Add a Student");
                Console.WriteLine("2. Search a Student");
                Console.WriteLine("3. Update Student Status");
                Console.WriteLine("4. View Lists of Students");
                Console.WriteLine("5. Exit");
                Console.Write("Choose: ");

                int option = Convert.ToInt32(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        Console.Write("Enter Student Name: ");
                        string newName = Console.ReadLine();

                        names.Add(newName);
                        status.Add("Not yet Enrolled");

                        Console.WriteLine("Successfully Saved!");
                        break;

                    case 2:
                        Console.Write("Search Student: ");
                        string search = Console.ReadLine();

                        int foundIndex = names.IndexOf(search);

                        if (foundIndex != -1)
                        {
                            Console.WriteLine("Name: " + names[foundIndex]);
                            Console.WriteLine("Status: " + status[foundIndex]);
                        }
                        else
                        {
                            Console.WriteLine("Student " + search + " does not exist.");
                        }
                        break;

                    case 3:
                        Console.Write("Enter Student Name to update: ");
                        string target = Console.ReadLine();

                        int i = names.IndexOf(target);

                        if (i == -1)
                        {
                            Console.WriteLine("Student Name does not exist.");
                            break;
                        }

                        Console.WriteLine("\nChoose new status:");
                        Console.WriteLine("1. Enroll");
                        Console.WriteLine("2. UnEnroll");
                        Console.WriteLine("3. Apply");
                        Console.WriteLine("4. Drop");
                        Console.WriteLine("5. Transferee");
                        Console.WriteLine("6. Waitlist");
                        Console.WriteLine("7. Deactivate");
                        Console.Write("Choice: ");

                        int choice = Convert.ToInt32(Console.ReadLine());

                        string newStatus = "Not yet Enrolled!";
                        if (choice == 1) newStatus = "Enrolled";
                        else if (choice == 2)
                            newStatus = "UnEnrolled";
                        else if (choice == 3)
                            newStatus = "Applied";
                        else if (choice == 4)
                            newStatus = "Dropped";
                        else if (choice == 5)
                            newStatus = "Transferee";
                        else if (choice == 6)
                            newStatus = "Waitlisted";
                        else if (choice == 7)
                            newStatus = "Deactivated";
                        else
                        {
                            Console.WriteLine("Invalid choice.");
                            break;
                        }

                        status[i] = newStatus;
                        Console.WriteLine("Status updated successfully!");
                        break;

                    case 4:
                        if (names.Count == 0)
                        {
                            Console.WriteLine("No students yet.");
                            break;
                        }

                        Console.WriteLine("\n--- Student List ---");
                        for (int x = 0; x < names.Count; x++)
                        {
                            Console.WriteLine($"{x + 1}. {names[x]} - {status[x]}");
                        }
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("\nInvalid option.");
                        break;
                }
            }
        }
    }
}