namespace SpaceGame.Dtos.SaveFile
{
    public class GetSaveFileDto
    {
        public int Id { get; set; }
        public GetUserDto? User { get; set; }
        public int Seed { get; set; }
        public int Stage { get; set; }
        public int Distance { get; set; }
    }
}
