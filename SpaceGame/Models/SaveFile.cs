namespace SpaceGame.Models
{
    public class SaveFile
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int Seed { get; set; }
        public int Stage { get; set; }
        public int Distance { get; set; }
    }
}
