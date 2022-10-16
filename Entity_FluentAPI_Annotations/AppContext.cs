using Microsoft.EntityFrameworkCore;

namespace Entity_FluentAPI_Annotations
{
    internal partial class Program
    {
        class AppContext : DbContext
        {

            public DbSet<Group> Groups { get; set; }
            public DbSet<Department> Departments { get; set; }
            public DbSet<Facultie> Faculties { get; set; }
            public DbSet<Teacher> Teachers { get; set; }


            public AppContext()
            {
                //Database.EnsureDeleted(); 
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