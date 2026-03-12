namespace StudentManagementSystemModels
{
    public class Student
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public Student()
        {
            Name = "";
            Status = "Not yet Enrolled!";
        }
    }
}