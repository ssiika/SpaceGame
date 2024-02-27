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
        private readonly DataContext _context;
        public UserService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;     
            _context = context;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetUserList()
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new();

            var userList = await _context.Users.ToListAsync();

            serviceResponse.Data = userList.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> GetSingle(int id)
        {
            ServiceResponse<GetUserDto> serviceResponse = new();

            try
            {
                var user = await _context.Users.FindAsync(id);

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

        public async Task<ServiceResponse<GetUserDto>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<GetUserDto> serviceResponse = new();
            User user = _mapper.Map<User>(newUser);

            try
            {
                // Check if user exists already 
                var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Username == newUser.Username);

                if (userExists is not null)
                {
                    throw new Exception("User already exists");
                }

                // user.Id is updated from 0 to the id the user will have in the database here automatically
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var addedUser = await _context.Users.FindAsync(user.Id);


                if (addedUser is null)
                {
                    throw new Exception("User not added to database successfully");
                }

                serviceResponse.Data = _mapper.Map<GetUserDto>(addedUser);
            }

            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
           
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetUserDto>> UpdateUser(UpdateUserDto updatedUser)
        {
            ServiceResponse<GetUserDto> serviceResponse = new();

            try
            {
                var foundUser = await _context.Users.FindAsync(updatedUser.Id);

                if (foundUser is null)
                {
                    throw new Exception($"User with id {updatedUser.Id} not found");
                }

                foundUser.Username = updatedUser.Username;
                foundUser.Password = updatedUser.Password;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetUserDto>(foundUser);
            }           
            catch (Exception ex) {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }
            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> DeleteUser(int id)
        {
            ServiceResponse<List<GetUserDto>> serviceResponse = new();

            try
            {
                var userToRemove = await _context.Users.FindAsync(id);

                if (userToRemove is null)
                {
                    throw new Exception($"User with id {id} not found");
                }

                _context.Users.Remove(userToRemove);
                await _context.SaveChangesAsync();

                var updatedList = await _context.Users.ToListAsync();
                serviceResponse.Data = updatedList.Select(user => _mapper.Map<GetUserDto>(user)).ToList();
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
