namespace SpaceGame.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> mockUsers = new List<User> {
            new User(),
            new User { Id = 1, Username = "steven", Password = "minecraft" }
        };

        public async Task<ServiceResponse<List<User>>> GetUserList()
        {
            ServiceResponse<List<User>> serviceResponse = new();

            serviceResponse.Data = mockUsers;
            return serviceResponse;
        }

        public async Task<ServiceResponse<User>> GetSingle(int id)
        {
            ServiceResponse<User> serviceResponse = new();

            var user = mockUsers.FirstOrDefault(user => user.Id == id);

            serviceResponse.Data = user;

            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
            }

            return serviceResponse;
        }   

        public async Task<ServiceResponse<List<User>>> AddUser(User newUser)
        {
            ServiceResponse<List<User>> serviceResponse = new();

            mockUsers.Add(newUser);

            serviceResponse.Data = mockUsers;

            return serviceResponse;
        }
    }
}
