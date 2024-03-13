namespace ModelStruct
{
    public class Game
    {
        public int Id { get; set; }
        public string? Name { get; set; }
   
        public virtual Studio? Studio { get; set; }
     
        public string? GameType {  get; set; }
        public int? Copies { get; set; }
        public DateTime Realise { get; set; }

        public virtual ICollection<Style>? Styles { get; set; }

    }
}
