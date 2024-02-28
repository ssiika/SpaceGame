using SpaceGame.Dtos.SaveFile;

namespace SpaceGame.Services.SaveFileService
{
    public interface ISaveFileService
    {
        Task<ServiceResponse<GetSaveFileDto>> GetSaveFile();

        Task<ServiceResponse<GetSaveFileDto>> UpdateSaveFile();

        Task<ServiceResponse<GetSaveFileDto>> DeleteSaveFile();
    }
}
