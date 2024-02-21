namespace SpaceGame.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<User>>> GetUserList();

        Task<ServiceResponse<User>> GetSingle(int id);

        Task<ServiceResponse<List<User>>> AddUser(User newUser);
    }
}
