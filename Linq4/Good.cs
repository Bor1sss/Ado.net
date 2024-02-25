namespace Linq4
{
    internal class Good
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public Good() { }

        public override string ToString()
        {
            return $"Id:{Id} Title:{Title} Price:{Price} Category:{Category}";
        }
    }
}