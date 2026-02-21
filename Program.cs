namespace StudentManagementSystem

{
    internal class Program
    {

        static void Main(string[] args)

        {
            //not yet finished!!
            //string[] names = new string[50];
            //string[] status = new string[50];
            string names = "";
            string status = "Not yet Enrolled";
            int choice;
            //int lists;

            int option;
            Console.WriteLine("--- Student Management System --- \n");
            Console.WriteLine("1. Add Student ");
            Console.WriteLine("2. Search Student");
            Console.WriteLine("3. Update Student Status");
            Console.WriteLine("4. View Lists of Students");
            Console.WriteLine("5. Exit\n");
            option = Convert.ToInt32(Console.ReadLine());

            switch (option)
            {

                case 1:
                    Console.WriteLine("Enter Student Name: ");
                    names = (Console.ReadLine());
                    status = "Not yet Enrolled";
                    Console.WriteLine("Successfully Saved!");
                    break;

                case 2:
                    Console.WriteLine("Search Student:");
                    string search = Console.ReadLine();

                    if (search == names)
                    {
                        Console.WriteLine("Name" + names);
                        Console.WriteLine("Status" + status);
                    }
                    else
                    {
                        Console.WriteLine("Student " +search +" is not yet enrolled.");
                    }
                    break;

                case 3:
                    Console.WriteLine("Enter Student Name: ");
                    status = Console.ReadLine();
                    
                    if (status == names)
                    {
                    Console.WriteLine("1. Enroll");
                    Console.WriteLine("2. UnEnroll");
                    Console.WriteLine("3. Applied");
                    Console.WriteLine("4. Drop");
                    Console.WriteLine("5. Transferee");
                    Console.WriteLine("6. Waitlist");
                    Console.WriteLine("7. Deactivate");
                    }
                    else {
                        Console.WriteLine("Student does not exist.");
                    }








            }
        }
    }
}
     
