using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MKR_Sofiia_Koval_oct2023
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Patient> listOfPatients = new List<Patient>
            {
                new Patient { SurnameP = "Bowen", BDayP = new DateTime(1955, 08, 23), Diagnos = "Pneumonia", SurnameDoctor = "Walker", DepartmentDoctor = "Intense therapy", LeaveDate = new DateTime(2023, 10, 18)},

                new Patient { SurnameP = "Parks", BDayP = new DateTime(1949, 11, 09), Diagnos = "Heart decease", SurnameDoctor = "Walker", DepartmentDoctor = "Intense therapy", LeaveDate = new DateTime(2023, 07, 29)},

                new Patient { SurnameP = "Williams", BDayP = new DateTime(2003, 08, 14), Diagnos = "High blood preasure", SurnameDoctor = "Sullivan", DepartmentDoctor = "Family therapy", LeaveDate = new DateTime(2023, 09, 13)},             

                new Patient { SurnameP = "Callivan", BDayP = new DateTime(2022, 12, 07), Diagnos = "Sickness", SurnameDoctor = "Cherry", DepartmentDoctor = "Children Therapy", LeaveDate = new DateTime(2023, 09, 27)},

                new Patient { SurnameP = "Lane", BDayP = new DateTime(2018, 09, 23), Diagnos = "Strong allergy", SurnameDoctor = "Cherry", DepartmentDoctor = "Children Therapy", LeaveDate = new DateTime(2023, 08, 30)}



            };

            List<Doctor> listOfDoctors = new List<Doctor>
            {
                new Doctor { Surname = "Walker", Specialization = "Emergency hospitalization" },
                new Doctor { Surname = "Sullivan", Specialization = "Therapist" },
                new Doctor { Surname = "Cherry", Specialization = "Children Therapist" },
            };


            DateTime pensionAgeDate = DateTime.Now.AddYears(-65);
            List<string> targetDepartments = new List<string> { "Family therapy", "Intense therapy" };

            int pensionAgePatientsCount = listOfPatients
                .Count(p => targetDepartments.Contains(p.DepartmentDoctor) && p.BDayP <= pensionAgeDate);
            Console.WriteLine("Task1. Summary amount of pensions patiens in departments Family Therapy and Intense therapy: " + pensionAgePatientsCount);
            


            string targetSpecialization = "Emergency hospitalization"; 

            int distinctDiagnosisCount = listOfPatients
                .Join(listOfDoctors, p => p.SurnameDoctor, d => d.Surname, (p, d) => new { p.Diagnos, d.Specialization })
                .Where(x => x.Specialization == targetSpecialization)
                .Select(x => x.Diagnos)
                .Distinct()
                .Count();

            Console.WriteLine($"Task2. Distinct count of dianosis that doctor with specialization {targetSpecialization} has given is {distinctDiagnosisCount}");


            //ExportToXmlFileFromList(listOfPatients, "C:\\Users\\sofiu\\AppData\\Local\\Temp\\oqu1iqir..xml");


        }



        public static void ExportToXmlFileFromList(List<Patient> list, string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Patient>));
            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, list);
            }
        }

    }
}
