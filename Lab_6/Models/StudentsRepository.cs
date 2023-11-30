using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab_6.Models
{
    public class StudentsRepository : IRepository<Student>
    {
        private SqlConnection Connection { get; set; }

        public StudentsRepository(SqlConnection _connection)
        {
            this.Connection = _connection;
        }

        // TO DO: CRUD
        public int Count()
        {
            using (SqlCommand command = new SqlCommand(
                "SELECT COUNT(*) FROM Students",
                Connection))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        public Student Create(Student item)
        {
            string sqlQuery = $"INSERT INTO Students (Surname, BirthYear, GroupUni, Faculty, AverageScore, WorkPlace, City) OUTPUT INSERTED.ID VALUES ('{item.Surname}',{item.BirthYear},{item.GroupUni}, {item.Faculty}, {item.AverageScore}, {item.WorkPlace}, {item.City})";
            using (SqlCommand command = new SqlCommand(sqlQuery, Connection))
            {
                item.Id = Convert.ToInt32(command.ExecuteScalar());
            }

            return item;
        }

        public bool Delete(int id)
        {
            using (SqlCommand command = new SqlCommand(
                String.Format("DELETE FROM Students WHERE Id={0}", id),
                Connection))
            {
                return command.ExecuteNonQuery() > 0;
            }
        }

        public Student Read(int id)
        {
            using (SqlCommand command = new SqlCommand(String.Format("SELECT * FROM Students WHERE Id={0}", id), Connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.Read()) return null;

                    return new Student()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Surname = Convert.ToString(reader["Surname"]),
                        BirthYear = Convert.ToInt32(reader["BirthYear"]),
                        GroupUni = Convert.ToString(reader["GroupUni"]),
                        Faculty = Convert.ToString(reader["Faculty"]),
                        AverageScore = (float)reader["AverageScore"],
                        WorkPlace = Convert.ToString(reader["WorkPlace"]),
                        City = Convert.ToString(reader["City"]),
                    };
                }
            }
        }

        public bool Update(Student item)
        {

            string sqlQuery = $"UPDATE Students SET Surname={item.Surname}, BirthYear = {item.BirthYear}, GroupUni={item.GroupUni}, Faculty={item.Faculty}, AverageScore={item.AverageScore}, WorkPlace={item.WorkPlace}, City={item.City} WHERE Id={item.Id})";
            using (SqlCommand command = new SqlCommand(sqlQuery, Connection))
            {
                return command.ExecuteNonQuery() > 0;
            }

        }
    }
}