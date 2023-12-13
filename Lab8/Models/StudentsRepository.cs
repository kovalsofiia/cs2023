using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Lab8.Models
{
    internal class StudentsRepository : IRepository<Student>
    {
        private readonly SchoolDbContext context;

        public StudentsRepository(SchoolDbContext _context)
        {
            this.context = _context;
        }

        public async Task ExecuteQueryAsync(string columnName, string columnValue)
        {
            Console.WriteLine($"Query Result for {columnName} = {columnValue}:");

            var students = await context.Students
                .Where(s => EF.Property<string>(s, columnName) != null && EF.Property<string>(s, columnName) == columnValue)
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public async Task ExecuteSpecialQueryAsync(string columnName, string columnValue)
        {
            Console.WriteLine($"Query Result for {columnName} LIKE {columnValue}%:");

            var students = await context.Students
                .Where(s => EF.Functions.Like(EF.Property<string>(s, columnName), $"{columnValue}%"))
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public async Task ExecuteIsNullQueryAsync(string columnName)
        {
            Console.WriteLine($"Query Result for {columnName} IS NULL:");

            var students = await context.Students
                .Where(s => EF.Property<string>(s, columnName) == null)
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }


        public async Task ExecuteInQueryAsync(List<string> workPlaces)
        {
            Console.WriteLine("Query Result for WorkPlace IN ('Global Innovations', 'Research Labs'):");

            var students = await context.Students
                .Where(s => workPlaces.Contains(s.WorkPlace))
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public async Task ExecuteBetweenQueryAsync(int startYear, int endYear)
        {
            Console.WriteLine($"Query Result for BirthYear BETWEEN {startYear} AND {endYear}:");

            var students = await context.Students
                .Where(s => s.BirthYear >= startYear && s.BirthYear <= endYear)
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public async Task ExecuteComplexCriterionQueryAsync()
        {
            Console.WriteLine("Query Result for GroupUni = 'Group2' AND AverageScore < 90 AND City = 'New York':");

            var students = await context.Students
                .Where(s => s.GroupUni == "Group2" && s.AverageScore < 90 && s.City == "New York")
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public async Task ExecuteDistinctQueryAsync(string columnName)
        {
            Console.WriteLine($"Query Result for DISTINCT {columnName}:");

            var distinctValues = await context.Students
                .Select(s => EF.Property<string>(s, columnName))
                .Distinct()
                .ToListAsync();

            foreach (var value in distinctValues)
            {
                Console.WriteLine($"{columnName}: {value}");
            }
        }

        public async Task ExecuteCalculatedFieldQueryAsync()
        {
            Console.WriteLine("Query Result for Calculated Field (AverageScore / 10):");

            var students = await context.Students
                .Select(s => new { s.Surname, Total10Score = s.AverageScore / 10 })
                .ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Surname: {student.Surname}, Total10Score: {student.Total10Score}");
            }
        }

        public async Task ExecuteGroupingQueryAsync()
        {
            Console.WriteLine("Query Result for Grouping (GroupUni, AVG(AverageScore) > 80):");

            var groupedResults = await context.Students
                .GroupBy(s => s.GroupUni)
                .Where(g => g.Average(s => s.AverageScore) > 80)
                .Select(g => new { GroupUni = g.Key, AverageScore = g.Average(s => s.AverageScore) })
                .ToListAsync();

            foreach (var result in groupedResults)
            {
                Console.WriteLine($"GroupUni: {result.GroupUni}, AverageScore: {result.AverageScore}");
            }
        }

        public async Task ExecuteSortingQueryAsync(string columnName, bool ascending)
        {
            Console.WriteLine($"Query Result for Sorting by {columnName} {(ascending ? "ASC" : "DESC")}:");

            var query = context.Students.AsQueryable();

            if (ascending)
            {
                query = query.OrderBy(s => EF.Property<double>(s, columnName));
            }
            else
            {
                query = query.OrderByDescending(s => EF.Property<double>(s, columnName));
            }

            var students = await query.ToListAsync();

            foreach (var student in students)
            {
                Console.WriteLine($"Id: {student.Id}, Surname: {student.Surname}, BirthYear: {student.BirthYear}, ...");
            }
        }

        public async Task ExecuteUpdateQueryAsync()
        {
            Console.WriteLine("Updating AverageScore to AverageScore * 10 for all students...");

            await context.Database.ExecuteSqlRawAsync("UPDATE Students SET AverageScore = AverageScore * 10");

            Console.WriteLine("Update complete.");
        }


        public async Task<int> CountAsync()
        {
            return await context.Students.CountAsync();
        }

        public async Task<Student> CreateAsync(Student item)
        {
            string sqlQuery = $"INSERT INTO Students (Surname, BirthYear, GroupUni, Faculty, AverageScore, WorkPlace, City) OUTPUT INSERTED.ID VALUES ('{item.Surname}',{item.BirthYear},{item.GroupUni}, {item.Faculty}, {item.AverageScore}, {item.WorkPlace}, {item.City})";

            using (SqlCommand command = new SqlCommand(sqlQuery, (SqlConnection)context.Database.GetDbConnection()))
            {
                item.Id = Convert.ToInt32(await command.ExecuteScalarAsync());
            }

            return item;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student != null)
            {
                context.Students.Remove(student);
                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<Student> ReadAsync(int id)
        {
            return await context.Students.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Student item)
        {
            var existingStudent = await context.Students.FindAsync(item.Id);

            if (existingStudent != null)
            {
                existingStudent.Surname = item.Surname;
                existingStudent.BirthYear = item.BirthYear;
                existingStudent.GroupUni = item.GroupUni;
                existingStudent.Faculty = item.Faculty;
                existingStudent.AverageScore = item.AverageScore;
                existingStudent.WorkPlace = item.WorkPlace;
                existingStudent.City = item.City;

                await context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
