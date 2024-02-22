namespace SpaceGame.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetUserList();

        Task<ServiceResponse<GetUserDto>> GetSingle(int id);

        Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser);
    }
}
