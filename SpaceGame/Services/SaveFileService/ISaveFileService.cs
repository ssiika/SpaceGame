using SpaceGame.Dtos.SaveFile;

namespace SpaceGame.Services.SaveFileService
{
    public interface ISaveFileService
    {
        Task<ServiceResponse<GetSaveFileDto>> GetSaveFile();
        Task<ServiceResponse<GetSaveFileDto>> AddSaveFile(AddSaveFileDto newSaveFile);
        Task<ServiceResponse<GetSaveFileDto>> UpdateSaveFile(UpdateSaveFileDto newSaveFile);
        Task<ServiceResponse<bool>> DeleteSaveFile();
    }
}
