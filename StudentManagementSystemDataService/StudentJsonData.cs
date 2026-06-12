using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using StudentManagementSystemModels;

namespace StudentManagementSystemDataService
{
    public class StudentJsonData : IStudentDataService
    {
        private List<Student> students = new List<Student>();
        private string _jsonFileName;

        public StudentJsonData()
        {
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}students.json";
            PopulateJsonFile();
        }

        private void PopulateJsonFile()
        {
            RetrieveDataFromJsonFile();
            if (students.Count <= 0)
            {
                students.Add(new Student { StudentID = 1, Name = "AJ", Status = "Graduated" });
                students.Add(new Student { StudentID = 2, Name = "Eunice", Status = "Transferee" });
                students.Add(new Student { StudentID = 3, Name = "Francis", Status = "Enrolled" });
                SaveDataToJsonFile();
            }
        }

        private void SaveDataToJsonFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(students, options);
            System.IO.File.WriteAllText(_jsonFileName, json);
        }

        private void RetrieveDataFromJsonFile()
        {
            if (!System.IO.File.Exists(_jsonFileName))
            {
                students = new List<Student>();
                return;
            }

            using (var reader = System.IO.File.OpenText(_jsonFileName))
            {
                var content = reader.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(content))
                {
                    students = JsonSerializer.Deserialize<List<Student>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Student>();
                }
            }
        }

        public List<Student> GetStudents()
        {
            RetrieveDataFromJsonFile();
            return students;
        }

        public Student? GetById(int id)
        {
            RetrieveDataFromJsonFile();
            return students.FirstOrDefault(s => s.StudentID == id);
        }

        public void Add(Student student)
        {
            RetrieveDataFromJsonFile();
            student.StudentID = students.Count > 0 ? students.Max(s => s.StudentID) + 1 : 1;
            students.Add(student);
            SaveDataToJsonFile();
        }

        public void Update(Student student)
        {
            RetrieveDataFromJsonFile();
            var existing = students.FirstOrDefault(s => s.StudentID == student.StudentID);
            if (existing != null)
            {
                existing.Name = student.Name;
                existing.Status = student.Status;
            }
            SaveDataToJsonFile();
        }

        public bool Delete(int id)
        {
            RetrieveDataFromJsonFile();
            var student = students.FirstOrDefault(s => s.StudentID == id);
            if (student != null)
            {
                students.Remove(student);
                SaveDataToJsonFile();
                return true;
            }
            return false;
        }
    }
}