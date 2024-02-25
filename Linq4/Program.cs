namespace Linq4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Good> goods1 = new List<Good>(){
        new Good()
        { Id = 1, Title = "Nokia 1100", Price = 450.99, Category = "Mobile" },
        new Good()
        { Id = 2, Title = "Iphone 4", Price = 5000, Category = "Mobile" },
        new Good()
        { Id = 3, Title = "Refregirator 5000", Price = 2555, Category = "Kitchen" },
        new Good()
        { Id = 4, Title = "Mixer", Price = 150, Category = "Kitchen" },
        new Good()
        { Id = 5, Title = "Magnitola", Price = 1499, Category = "Car" },
        new Good()
         { Id = 6, Title = "Samsung Galaxy", Price = 3100, Category = "Mobile" },
        new Good()
         { Id = 7, Title = "Auto Cleaner", Price = 2300, Category = "Car" },
        new Good()
         { Id = 8, Title = "Owen", Price = 700, Category = "Kitchen" },
        new Good()
         { Id = 9, Title = "Siemens Turbo", Price = 3199, Category = "Mobile" },
        new Good()
         { Id = 10, Title = "Lighter", Price = 150, Category = "Car" }
        };


            //1
            var Task1_1 = goods1.Select(x => x).Where(x => x.Price > 1000).ToList();

            var Task1_2 = from good in goods1
                          where good.Price>1000
                          select good;

            //2
            var Task2_1 = goods1.Select(x => x).Where(x => x.Price > 1000&&x.Category!="Kitchen").ToList();

            var Task2_2 = from good in goods1
                          where good.Price > 1000 && good.Category != "Kitchen"
                          select good;



          /*  foreach (var item in Task2_2)
            {
                Console.WriteLine(item);
            }*/


            //3
            var Task3_1 = goods1.Average(x => x.Price);

            var Task3_2 = (from good in goods1
                          select good.Price).Average();

         
           // Console.WriteLine(Task3_2);


            //4


            var Task4_1 = goods1.Select(x=>x.Category).Distinct();

            var Task4_2 = (from good in goods1
                           select good.Category).Distinct();

/*
            foreach (var item in Task4_2)
            {
                Console.WriteLine(item);
            }*/

            //5



            var Task5_1 = goods1.OrderBy(x => x.Category).ToList();

            var Task5_2 = (from good in goods1
                           orderby good.Category ascending
                           select good).ToList();


            foreach (var item in Task5_1)
            {
                Console.WriteLine(item);
            }

            //6

            var Task6_1 = goods1.Where(x => x.Category == "Car" || x.Category == "Mobile").Count();

            var Task6_2 = (from x in goods1
                           where x.Category == "Car" || x.Category == "Mobile"
                           select x).Count();

            Console.WriteLine(Task6_2);



            //7

            var Task7_1 = goods1
                                .GroupBy(good => good.Category)
                                .Select(group => new { Category = group.Key, Count = group.Count() });


            var Task7_2= from good in goods1
                         group good by good.Category into g
                         select new { Category = g.Key, Count = g.Count() };


            foreach (var categoryCount in Task7_2)
            {
                Console.WriteLine($"{categoryCount.Category} {categoryCount.Count}");
            }


        }
    }
}