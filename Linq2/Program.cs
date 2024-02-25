using System;

namespace Linq2
{
    class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int DepId { get; set; }
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
            {
            new Department(){ Id = 1, Country = "Ukraine", City = "Lviv" },
            new Department(){ Id = 2, Country = "Ukraine", City = "Kyiv" },
            new Department(){ Id = 3, Country = "France", City = "Paris" },
            new Department(){ Id = 4, Country = "Ukraine", City = "Odesa" }
             };



            List<Employee> employees = new List<Employee>()
            {
            new Employee()
            { Id = 1, FirstName = "Tamara", LastName = "Ivanova", Age = 22, DepId = 2 },
            new Employee()
            { Id = 2, FirstName = "Nikita", LastName = "Larin", Age = 33, DepId = 1 },
            new Employee()
            { Id = 3, FirstName = "Alica", LastName = "Ivanova", Age = 43, DepId = 3 },
            new Employee()
            { Id = 4, FirstName = "Lida", LastName = "Marusyk", Age = 22, DepId = 2 },
            new Employee()
            { Id = 5, FirstName = "Lida", LastName = "Voron", Age = 36, DepId = 4 },
            new Employee()
            { Id = 6, FirstName = "Ivan", LastName = "Kalyta", Age = 28, DepId = 2 },
            new Employee()
            { Id = 7, FirstName = "Nikita", LastName = " Krotov ", Age = 27, DepId = 4 }
            };


            //1

            var Names = from emp in employees
                        join department in departments on emp.DepId equals department.Id
                        where department.Country == "Ukraine"
                        where department.City!= "Odesa"
                        select new { emp.FirstName, emp.LastName };



            var result = employees
                .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                .Where(x => x.dep.Country == "Ukraine" && x.dep.City != "Odesa")
                .Select(x => new { x.emp.FirstName, x.emp.LastName });


         /*   foreach (var employee in result)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}");
            }*/
            //2
            var distinctCountries = departments.Select(dep => dep.Country).Distinct().ToList();


            var distinctCountries2 = (from dep in departments
                                     select dep.Country).Distinct().ToList();

            distinctCountries2.ForEach(country => Console.WriteLine(country));




            //3 

            var Tree = employees.Where(emp => emp.Age > 25).Take(3).ToList();

            foreach (var employee in Tree)
            {
                Console.WriteLine($"{employee.FirstName} {employee.LastName}, Age: {employee.Age}");
            }



            var Three2 = (from emp in employees
                                     where emp.Age > 25
                                     select emp).Take(3).ToList();

            

            //4

            var Task4 = employees
                        .Join(departments, emp => emp.DepId, dep => dep.Id, (emp, dep) => new { emp, dep })
                        .Where(x => x.dep.City == "Kyiv" && x.emp.Age>23)
                        .Select(x => new { x.emp.FirstName, x.emp.LastName });

            foreach (var item in Task4)
            {
                Console.WriteLine(item);
            }
        }






    }
}