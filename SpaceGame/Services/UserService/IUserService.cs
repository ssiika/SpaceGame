namespace SpaceGame.Services.UserService
{
    public interface IUserService
    {
        Task<ServiceResponse<List<GetUserDto>>> GetUserList();

        Task<ServiceResponse<GetUserDto>> GetSingle(int id);

        Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser);

        Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser);

        Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id);
    }
}
