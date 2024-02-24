namespace Linq1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> person = new List<Person>()
            {
                new Person(){ Name = "Andrey", Age = 24, City = "Kyiv"},
                new Person(){ Name = "Liza", Age = 18, City = "Odesa" },
                new Person(){ Name = "Oleg", Age = 15, City = "London" },
                new Person(){ Name = "Sergey", Age = 55, City = "Kyiv" },
                new Person(){ Name = "Sergey", Age = 32, City = "Lviv" }
            };

            //1
            var AGE = from p in person 
                      where p.Age>25
                      select p;
            var AGE2 = person.Where(item => item.Age > 25);
       
            //2

            var city = from p in person
                      where p.City != "London"
                      select p;
            var city2 = person.Where(item => item.City != "London");


            //3

            var Names = from p in person
                       where p.City == "Kyiv" 
                       select p;
            var Names2 = person.Where(item => item.City == "Kyiv");

            //4

            var Ser = from p in person
                     where p.City == "Kyiv"
                     where p.Name == "Sergey"
                     select p;
            var Ser2 = person.Where(item => item.City == "Kyiv"&&item.Name=="Sergey");


            foreach (var item in Ser)
            {
                Console.WriteLine(item);
            }
            foreach (var item in Ser2)
            {
                Console.WriteLine(item);
            }


            //5

            var Odes = from p in person
                      where p.City == "Odesa"
                     
                      select p;
            var Odes2 = person.Where(item => item.City == "Odesa");



        }
    }


    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return $"Name: {Name} Age:{Age} City {City}";
        }
    }
   
}