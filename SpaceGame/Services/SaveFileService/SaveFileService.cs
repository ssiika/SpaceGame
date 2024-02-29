using Microsoft.EntityFrameworkCore;
using SpaceGame.Models;

namespace SpaceGame.Services.SaveFileService
{
    public class SaveFileService : ISaveFileService
    {
        private static readonly User testUser = new() { Id = 1, Username = "steven", Password = "minecraft" };

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public SaveFileService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponse<GetSaveFileDto>> GetSaveFile()
        {
            ServiceResponse<GetSaveFileDto> serviceResponse = new();
            try
            {              
                var userSaveFile = await _context.SaveFiles
                .Include("User")
                .FirstOrDefaultAsync(record => record.User != null && record.User.Id == testUser.Id);

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
                // Make sure user exists in database already first
                var dbUser = await _context.Users.FindAsync(testUser.Id) ??
                    throw new Exception("Could not find user");

                var saveFileExists = await _context.SaveFiles
                    .Include("User")
                    .FirstOrDefaultAsync(record => record.User != null && record.User == dbUser);

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
                var userSaveFile = await _context.SaveFiles
                .Include("User")
                .FirstOrDefaultAsync(record => record.User != null && record.User.Id == testUser.Id);

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
                var userSaveFile = await _context.SaveFiles
                .Include("User")
                .FirstOrDefaultAsync(record => record.User != null && record.User.Id == testUser.Id);

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
