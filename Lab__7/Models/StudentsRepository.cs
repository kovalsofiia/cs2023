using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Lab__7.Models
{
    internal class StudentsRepository : IRepository<Student>
    {

        private readonly SchoolDbContext context;

        public StudentsRepository(SchoolDbContext _context)
        {
            this.context = _context;
        }

        public void ExecuteQuery(string columnName, string columnValue)
        {
            Console.WriteLine($"Query Result for {columnName} = {columnValue}:");

            var students = context.Students
                .Where(s => EF.Property<string>(s, columnName) != null && EF.Property<string>(s, columnName) == columnValue)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }


        public void ExecuteSpecialQuery(string columnName, string columnValue)
        {
            Console.WriteLine($"Query Result for {columnName} LIKE {columnValue}%:");
            var students = context.Students
                .Where(s => EF.Functions.Like(EF.Property<string>(s, columnName), $"{columnValue}%"))
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public void ExecuteIsNullQuery(string columnName)
        {
            Console.WriteLine($"Query Result for {columnName} IS NULL:");
            var students = context.Students
                .Where(s => EF.Property<string>(s, columnName) == null)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public void ExecuteInQuery(List<string> workPlaces)
        {
            Console.WriteLine("Query Result for WorkPlace IN ('Global Innovations', 'Research Labs'):");
            var students = context.Students
                .Where(s => workPlaces.Contains(s.WorkPlace))
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public void ExecuteBetweenQuery(int startYear, int endYear)
        {
            Console.WriteLine($"Query Result for BirthYear BETWEEN {startYear} AND {endYear}:");
            var students = context.Students
                .Where(s => s.BirthYear >= startYear && s.BirthYear <= endYear)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public void ExecuteComplexCriterionQuery()
        {
            Console.WriteLine("Query Result for GroupUni = 'Group2' AND AverageScore < 90 AND City = 'New York':");
            var students = context.Students
                .Where(s => s.GroupUni == "Group2" && s.AverageScore < 90 && s.City == "New York")
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public void ExecuteDistinctQuery(string columnName)
        {
            Console.WriteLine($"Query Result for DISTINCT {columnName}:");
            var distinctValues = context.Students
                .Select(s => EF.Property<string>(s, columnName))
                .Distinct()
                .ToList();

            foreach (var value in distinctValues)
            {
                Console.WriteLine($"{columnName}: {value}");
            }
        }

        public void ExecuteCalculatedFieldQuery()
        {
            Console.WriteLine("Query Result for Calculated Field (AverageScore / 10):");
            var students = context.Students
                .Select(s => new { s.Surname, Total10Score = s.AverageScore / 10 })
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Surname: {student.Surname}, Total10Score: {student.Total10Score}");
            }
        }

        public void ExecuteGroupingQuery()
        {
            Console.WriteLine("Query Result for Grouping (GroupUni, AVG(AverageScore) > 80):");
            var groupedResults = context.Students
                .GroupBy(s => s.GroupUni)
                .Where(g => g.Average(s => s.AverageScore) > 80)
                .Select(g => new { GroupUni = g.Key, AverageScore = g.Average(s => s.AverageScore) })
                .ToList();

            foreach (var result in groupedResults)
            {
                Console.WriteLine($"GroupUni: {result.GroupUni}, AverageScore: {result.AverageScore}");
            }
        }

        public void ExecuteSortingQuery(string columnName, bool ascending)
        {
            Console.WriteLine($"Query Result for Sorting by {columnName} {(ascending ? "ASC" : "DESC")}:");

            IQueryable<Student> query = context.Students;

            if (ascending)
            {
                query = query.OrderBy(s => EF.Property<double>(s, columnName));
            }
            else
            {
                query = query.OrderByDescending(s => EF.Property<double>(s, columnName));
            }

            var students = query.ToList();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }



        public void ExecuteUpdateQuery()
        {
            Console.WriteLine("Updating AverageScore to AverageScore * 10 for all students...");
            context.Students
                .ToList()
                .ForEach(s => s.AverageScore *= 10);

            context.SaveChanges();

            Console.WriteLine("Update complete.");
        }


        private SqlConnection Connection { get; set; }

        // TO DO: CRUD
        public int Count()
        {
            using SqlCommand command = new SqlCommand(
                "SELECT COUNT(*) FROM Students",
                Connection);
            return Convert.ToInt32(command.ExecuteScalar());
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
            var existingStudent = context.Students.Find(item.Id);

            if (existingStudent != null)
            {
                existingStudent.Surname = item.Surname;
                existingStudent.BirthYear = item.BirthYear;
                existingStudent.GroupUni = item.GroupUni;
                existingStudent.Faculty = item.Faculty;
                existingStudent.AverageScore = item.AverageScore;
                existingStudent.WorkPlace = item.WorkPlace;
                existingStudent.City = item.City;

                context.SaveChanges();
                return true;
            }

            return false;
        }


    }
}
