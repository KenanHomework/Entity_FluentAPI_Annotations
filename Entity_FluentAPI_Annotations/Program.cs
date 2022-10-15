using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualBasic;
using System.Text.Json;

namespace Entity_FluentAPI_Annotations
{
    internal class Program
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

        class Group
        {

            public int Id { get; set; }

            public string Name { get; set; }

            public int Rating { get; set; }

            public int Year { get; set; }

        }

        class Department
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public decimal Financing { get; set; }

        }

        class Facultie
        {
            public int Id { get; set; }

            public string Name { get; set; }

        }

        class Teacher
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public DateTime EmploymentDate { get; set; }

            public decimal Premium { get; set; }

            public decimal Salary { get; set; }
        }

        class AppContext : DbContext
        {

            public DbSet<Group> Groups { get; set; }
            public DbSet<Department> Departments { get; set; }
            public DbSet<Facultie> Faculties { get; set; }
            public DbSet<Teacher> Teachers { get; set; }


            public AppContext()
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer(ConnStr);

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

                #region Groups

                // Name
                modelBuilder.Entity<Group>()
                                .Property("Name")
                                .IsUnicode()
                                .HasMaxLength(10);
                modelBuilder.Entity<Group>().HasIndex(u => u.Name).IsUnique();


                // Rating
                modelBuilder.Entity<Group>().HasCheckConstraint("Rating", "Rating BETWEEN 0 AND 5");


                // Year
                modelBuilder.Entity<Group>().HasCheckConstraint("Year", "Year BETWEEN 0 AND 5");

                #endregion

                #region Department

                //Financing
                modelBuilder.Entity<Department>()
                                .Property("Financing")
                                .HasDefaultValue(0);
                modelBuilder.Entity<Department>().HasCheckConstraint("Financing", "Financing >= 0");

                // Name
                modelBuilder.Entity<Department>()
                                .Property("Name")
                                .IsUnicode()
                                .HasMaxLength(100);
                modelBuilder.Entity<Department>().HasIndex(d => d.Name).IsUnique();

                #endregion

                #region Facultie

                // Name
                modelBuilder.Entity<Facultie>()
                                .Property("Name")
                                .IsUnicode()
                                .HasMaxLength(100);
                modelBuilder.Entity<Facultie>().HasIndex(f => f.Name).IsUnique();

                #endregion

                #region Teachers

                // Name
                modelBuilder.Entity<Teacher>()
                                .Property("Name")
                                .IsUnicode();


                // Surname
                modelBuilder.Entity<Teacher>()
                                .Property("Surname")
                                .IsUnicode();


                //EmploymentDate 
                modelBuilder.Entity<Teacher>()
                                .Property("EmploymentDate")
                                .HasColumnType("date");
                modelBuilder.Entity<Teacher>().HasCheckConstraint("EmploymentDate", "EmploymentDate >= '01.01.1990'");


                // Premium
                modelBuilder.Entity<Teacher>()
                                .Property("Premium")
                                .HasDefaultValue(0);
                modelBuilder.Entity<Teacher>().HasCheckConstraint("Premium", "Premium >= 0");


                // Salary
                modelBuilder.Entity<Teacher>().HasCheckConstraint("Salary", "Salary > 0");

                #endregion
            }
        }

    }
}