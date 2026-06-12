using System.Collections.Generic;
using StudentManagementSystemModels;

namespace StudentManagementSystemDataService
{
    public interface IStudentDataService
    {
        List<Student> GetStudents();
        Student? GetById(int id);
        void Add(Student student);
        void Update(Student student);
        bool Delete(int id);
    }
}