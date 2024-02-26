namespace SpaceGame.Dtos.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = "username";
        public string Password { get; set; } = "password";
    }
}
