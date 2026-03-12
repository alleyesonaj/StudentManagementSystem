using System.Collections.Generic;
using StudentManagementSystemModels;

namespace StudentManagementSystemDataService
{
    public class StudentDataService
    {
        private List<Student> students = new List<Student>();

        public List<Student> GetStudents()
        {
            return students;
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }
    }
}