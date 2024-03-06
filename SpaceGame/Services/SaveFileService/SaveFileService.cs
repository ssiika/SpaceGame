using Microsoft.EntityFrameworkCore;
using SpaceGame.Models;
using System.Security.Claims;

namespace SpaceGame.Services.SaveFileService
{
    public class SaveFileService : ISaveFileService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SaveFileService(
            IMapper mapper, 
            DataContext context,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ServiceResponse<GetSaveFileDto>> GetSaveFile()
        {
            ServiceResponse<GetSaveFileDto> serviceResponse = new();
            try
            {    
                if (_httpContextAccessor.HttpContext is null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("Could not find user id");
                }

                var userSaveFile = await _context.SaveFiles
                    .Include("User")
                    .FirstOrDefaultAsync(saveFile => saveFile.User != null && saveFile.User.Id == int.Parse(userId));

                if (userSaveFile is null)
                {
                    throw new Exception("Save file not found");
                }

                serviceResponse.Data = _mapper.Map<GetSaveFileDto>(userSaveFile);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSaveFileDto>> AddSaveFile(AddSaveFileDto newSaveFile)
        {
            ServiceResponse<GetSaveFileDto> serviceResponse = new();
            try
            {
                if (_httpContextAccessor.HttpContext is null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("Could not find user id");
                }

                // Make sure user exists in database already first
                var dbUser = await _context.Users.FindAsync(int.Parse(userId)) ??
                    throw new Exception("Could not find user");

                var saveFileExists = await _context.SaveFiles
                    .Include("User")
                    .FirstOrDefaultAsync(saveFile => saveFile.User != null && saveFile.User == dbUser);

                if (saveFileExists is not null)
                {
                    throw new Exception("Save file already exists. Consider PUT instead.");
                }                

                var mappedFile = _mapper.Map<SaveFile>(newSaveFile);

                mappedFile.User = dbUser;

                _context.SaveFiles.Add(mappedFile);
                await _context.SaveChangesAsync();

                var addedFile = await _context.SaveFiles.FindAsync(mappedFile.Id) ??
                    throw new Exception("Save file not added to database successfully");

                serviceResponse.Data = _mapper.Map<GetSaveFileDto>(addedFile);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetSaveFileDto>> UpdateSaveFile(UpdateSaveFileDto newSaveFile)
        {
            ServiceResponse<GetSaveFileDto> serviceResponse = new();
            try
            {
                if (_httpContextAccessor.HttpContext is null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("Could not find user id");
                }

                var userSaveFile = await _context.SaveFiles
                    .Include("User")
                    .FirstOrDefaultAsync(saveFile => saveFile.User != null && saveFile.User.Id == int.Parse(userId));

                if (userSaveFile is null)
                {
                    throw new Exception("Save file not found");
                }

                userSaveFile.Seed = newSaveFile.Seed;
                userSaveFile.Stage = newSaveFile.Stage;
                userSaveFile.Distance = newSaveFile.Distance;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetSaveFileDto>(userSaveFile);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }      

        public async Task<ServiceResponse<bool>> DeleteSaveFile()
        {
            ServiceResponse<bool> serviceResponse = new();
            serviceResponse.Data = false;

            try
            {
                if (_httpContextAccessor.HttpContext is null)
                {
                    throw new Exception("User not found");
                }

                string userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    throw new Exception("Could not find user id");
                }

                var userSaveFile = await _context.SaveFiles
                .Include("User")
                .FirstOrDefaultAsync(saveFile => saveFile.User != null && saveFile.User.Id == int.Parse(userId));

                if (userSaveFile is null)
                {
                    throw new Exception("Save file not found");
                }

                _context.SaveFiles.Remove(userSaveFile);
                await _context.SaveChangesAsync();

                serviceResponse.Data = true;
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
