using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mkr___2
{
    internal class ZavodRepository
    {
        protected SqlConnection _connection;
        public ZavodRepository(SqlConnection connection)
        {
            _connection = connection;
        }
        public List<Zavod> GetAll()
        {
            var zavods = new List<Zavod>();
            string query = "SELECT * FROM Zavod;";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zavod zavod = new Zavod()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Surname = Convert.ToString(reader["Surname"]),
                            CechNumber = Convert.ToInt32(reader["CechNumber"]),
                            Position = Convert.ToString(reader["Position"]),
                            Experience = Convert.ToInt32(reader["Experience"]),
                            Salary = Convert.ToDecimal(reader["Salary"])
                        };
                        zavods.Add(zavod);
                    }
                }
            }
            return zavods;
        }
        public List<Zavod> GetBySurname(string surname)
        {
            var zavods = new List<Zavod>();
            string query = $"SELECT Surname, CechNumber, Position, Experience, Salary FROM Zavod WHERE Surname = '{surname}';";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Zavod zavod = new Zavod()
                        {
                            Surname = Convert.ToString(reader["Surname"]),
                            CechNumber = Convert.ToInt32(reader["CechNumber"]),
                            Position = Convert.ToString(reader["Position"]),
                            Experience = Convert.ToInt32(reader["Experience"]),
                            Salary = Convert.ToDecimal(reader["Salary"])
                        };
                        zavods.Add(zavod);
                    }
                }
            }
            return zavods;
        }
        public int GetAverageSalary(int sech)
        {
            int averageSalary = 0;
            string query = $"SELECT AVG(Salary) as AverageSalary FROM Zavod WHERE CechNumber = {sech};";
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        averageSalary = Convert.ToInt32(reader["AverageSalary"]);
                    }
                }
            }
            return averageSalary;
        }
    }
}
