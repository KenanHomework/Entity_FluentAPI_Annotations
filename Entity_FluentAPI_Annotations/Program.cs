using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace Entity_FluentAPI_Annotations
{
    internal partial class Program
    {

        public static string ConnStr { get; set; }

        static void Main(string[] args)
        {
            ReadConnectionString();


            using (AppContext db = new())
            {

                db.Groups.Add(new() { Name = "TestGroup", Rating = 5, Year = 2 });
                db.Departments.Add(new() { Name = "TestDepartmen", Financing = 200 });
                db.Faculties.Add(new() { Name = "TestFacultie" });
                db.Teachers.Add(new() { Name = "TestTeacherName", Surname = "TestTeacherSurname", EmploymentDate = new DateTime(2000, 1, 1), Salary = 200, Premium = 100 });


                db.SaveChanges();
            }


        }

        public static void ReadConnectionString()
        {
            using (var jsonDoc = JsonDocument.Parse(File.ReadAllText("appconfig.json")))
            {
                JsonElement key = jsonDoc.RootElement.GetProperty("SqlServerConnection");
                ConnStr = key.ToString();
            }
        }
    }
}