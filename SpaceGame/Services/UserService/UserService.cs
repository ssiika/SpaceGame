using SpaceGame.Models;

namespace SpaceGame.Services.UserService
{
    public class UserService : IUserService
    {
        private static List<User> mockUsers = new List<User> {
            new User(),
            new User { Id = 1, Username = "steven", Password = "minecraft" }
        };

        private readonly IMapper _mapper;
        public UserService(IMapper mapper)
        {
            _mapper = mapper;     
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUserList()
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new();

            serviceResponse.Data = mockUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingle(int id)
        {
            ServiceResponse<GetUserDto> serviceResponse = new();

            try
            {
                var user = mockUsers.FirstOrDefault(user => user.Id == id);

                if (user is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                serviceResponse.Data = _mapper.Map<GetUserDto>(user);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }   

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new();

            User user = _mapper.Map<User>(newUser);
            user.Id = mockUsers.Max(user => user.Id);

            mockUsers.Add(user);

            serviceResponse.Data = mockUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new();

            try
            {
                var foundUser = mockUsers.FirstOrDefault(user => user.Id == updatedUser.Id);

                if (foundUser is null)
                {
                    throw new Exception($"User with id {updatedUser.Id} not found");
                }

                foundUser.Username = updatedUser.Username;
                foundUser.Password = updatedUser.Password;

                serviceResponse.Data = mockUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            }           
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }
    }
}
