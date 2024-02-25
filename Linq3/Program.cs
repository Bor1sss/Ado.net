namespace Linq3
{
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }

        public override string ToString()
        {
            return $"{Id} {FirstName} {LastName} {Age}";
        }
    }
    class Department
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
    }
    class Program
    {
        static void Main()
        {
                            List<Department> departments = new List<Department>()
                { new Department(){ Id = 1, Country = "Ukraine", City = "Odesa"},
                 new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
                 new Department(){ Id = 3, Country = "France", City = "Paris" },
                 new Department(){ Id = 4, Country = " Ukraine ", City = "Lviv"}
                };
                            List<Employee> employees = new List<Employee>()
                 {
                 new Employee()
                 {
                 Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2
                 },
                 new Employee()
                 {
                 Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1
                 },
                 new Employee()
                 {
                 Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3
                 },
                 new Employee()
                 {
                 Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2
                 },
                 new Employee()
                 {
                 Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4
                 },
                 new Employee()
                 {
                 Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 22, DepId = 2
                 },
                 new Employee()
                 {
                 Id = 7, FirstName = "Nikita", LastName = "Krotov", Age = 27, DepId = 4
                 }
                 };


            var Task1 = employees
                         .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                         .Where(x => x.dep.Country == "Ukraine").OrderBy(x => x.emp.LastName)
                         .Select(x => new { x.emp.FirstName, x.emp.LastName }).ToList();

            var Task1_2 = (from emp in employees
                          join department in departments on emp.DepId equals department.Id
                          where department.Country=="Ukraine"
                          orderby emp.LastName
                          select new { emp.FirstName, emp.LastName }).ToList();




            foreach (var item in Task1_2)
            {
                Console.WriteLine(item);
            }

            foreach (var item in Task1_2)
            {
                Console.WriteLine("\n");
            }
            //task 2

            var Task2_1 = employees
                         .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                         .OrderByDescending(x => x.emp.Age)
                         .Select(x => new { x.emp.Id,x.emp.FirstName, x.emp.LastName,x.emp.Age }).ToList();


            var Task2_2 = (from emp in employees
                          join department in departments on emp.DepId equals department.Id
                          orderby emp.Age descending
                          select emp).ToList();

            foreach (var item in Task2_1)
            {
                Console.WriteLine(item);
            }
            foreach (var item in Task2_1)
            {
                Console.WriteLine("\n");
            }

            //task 3


            var Task3_1 = employees.GroupBy(emp => emp.Age).Select(x => new {Age=x.Key, Count =x.Count()}).ToList();
            var Task3_2 = from emp in employees
                          group emp by emp.Age into x
                          select new { Age = x.Key, Count = x.Count() };




            foreach (var item in Task3_2)
            {
                Console.WriteLine(item);
            }


        }
    }
}