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

            var user = mockUsers.FirstOrDefault(user => user.Id == id);

            if (user is null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "User not found";
            }

            serviceResponse.Data = _mapper.Map<GetUserDto>(user);          

            return serviceResponse;
        }   

        public async Task<ServiceResponse<List<GetUserDto>>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new();

            mockUsers.Add(_mapper.Map<User>(newUser));

            serviceResponse.Data = mockUsers.Select(user => _mapper.Map<GetUserDto>(user)).ToList();

            return serviceResponse;
        }
    }
}
