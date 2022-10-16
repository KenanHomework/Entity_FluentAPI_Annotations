namespace Entity_FluentAPI_Annotations
{
    internal partial class Program
    {
        class Teacher
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public DateTime EmploymentDate { get; set; }

            public decimal Premium { get; set; }

            public decimal Salary { get; set; }
        }

    }
}