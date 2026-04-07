using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using StudentManagementSystemModels;

namespace StudentManagementSystemDataService
{
    public class StudentDataService
    {
        
        private string connectionString = "Server=localhost\\SQLEXPRESS;Database=StudentManagementSystemDatabase;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT StudentID, Name, Status FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            Name = reader["Name"].ToString() ?? "",
                            Status = reader["Status"].ToString()?.Trim() ?? ""
                        });
                    }
                }
            }
            return students;
        }

        public void AddStudent(Student student)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (Name, Status) VALUES (@name, @status)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", student.Name);
                cmd.Parameters.AddWithValue("@status", student.Status);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        
        public Student SearchStudentInDb(string name)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT StudentID, Name, Status FROM Students WHERE Name = @name";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", name);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Student
                        {
                            StudentID = (int)reader["StudentID"],
                            Name = reader["Name"].ToString() ?? "",
                            Status = reader["Status"].ToString()?.Trim() ?? ""
                        };
                    }
                }
            }
            return null;
        }

        
        public void UpdateStatusById(int id, string newStatus)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET Status = @status WHERE StudentID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@status", newStatus);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteStudentById(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE StudentID = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}