using Microsoft.IdentityModel.Tokens;
using SpaceGame.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly IConfiguration _conf;
        public UserService(IMapper mapper, DataContext context, IConfiguration conf)
        {
            _mapper = mapper;     
            _context = context;
            _conf = conf;
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

        public async Task<ServiceResponse<UserCredsDto>> AddUser(AddUserDto newUser)
        {
            ServiceResponse<UserCredsDto> serviceResponse = new();
            User user = _mapper.Map<User>(newUser);

            try
            {
                // Check if user exists already 
                var userExists = await _context.Users.FirstOrDefaultAsync(user => user.Username == newUser.Username);

                if (userExists is not null)
                {
                    throw new Exception("User already exists");
                }

                string passswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password);

                user.Password = passswordHash;

                // user.Id is updated from 0 to the id the user will have in the database here automatically
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                var addedUser = await _context.Users.FindAsync(user.Id);


                if (addedUser is null)
                {
                    throw new Exception("User not added to database successfully");
                }

                string token = CreateToken(addedUser);

                serviceResponse.Data = new UserCredsDto
                {
                    Username = addedUser.Username,
                    Token = token
                };
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

        public async Task<ServiceResponse<UserCredsDto>> LoginUser(AddUserDto loginRequest)
        {
            ServiceResponse<UserCredsDto> serviceResponse = new();

            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(user => user.Username == loginRequest.Username);

                if (user is null)
                {
                    throw new Exception($"Wrong username or password");
                }

                if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
                {
                    throw new Exception("Wrong username or password");
                }

                string token = CreateToken(user);

                serviceResponse.Data = new UserCredsDto
                {
                    Username = user.Username,
                    Token = token
                };
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;           
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Username == "admin" ? "Admin" : "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _conf.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
