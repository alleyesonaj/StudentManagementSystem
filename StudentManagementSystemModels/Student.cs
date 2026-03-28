namespace StudentManagementSystemModels
{
    public class Student
    {
        
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Status { get; set; } = "Not yet Enrolled!";

        public Student()
        {
            Name = "";
            Status = "Not yet Enrolled!";
        }
    }
}