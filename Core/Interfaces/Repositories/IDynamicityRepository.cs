using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Handling;

namespace OHaraj.Core.Interfaces.Repositories
{
    public interface IDynamicityRepository
    {
        //Menu
        Task<int> AddMenuAsync(Menu input);
        Task<int> UpdateMenuAsync(Menu input);
        Task<int> DeleteMenuAsync(Menu input);

        Task<Menu> GetMenuAsync(int id);
        Task<IEnumerable<Menu>> GetMenusAsync();
        // first get all menus then remove these access denied menus and send
        Task<IEnumerable<Menu>> GetAccessDeniedMenusAsync(IEnumerable<string> roleIds);



        //Role Access Banned
        Task<int> AddBanAccessAsync(RoleAccessBanned input);
        Task<int> UpdateBanAccessAsync(RoleAccessBanned input);
        Task<int> DeleteBanAccessAsync(RoleAccessBanned input);

        Task<RoleAccessBanned> GetBanAccessAsync(int id);
        Task<IEnumerable<RoleAccessBanned>> GetBanAccessesAsync();
        Task<IEnumerable<RoleAccessBanned>> GetBanAccessByRoleAsync(int roleId);



        //Configs
        Task<int> AddImageConfigAsync(ImageConfigs input);
        Task<int> UpdateImageConfigAsync(ImageConfigs input);
        Task<int> DeleteImageConfigAsync(ImageConfigs input);

        Task<ImageConfigs> GetImageConfigAsync(int id);
        Task<IEnumerable<ImageConfigs>> GetImageConfigsAsync();


        Task<int> AddVideoConfigAsync(VideoConfigs input);
        Task<int> UpdateVideoConfigAsync(VideoConfigs input);
        Task<int> DeleteVideoConfigAsync(VideoConfigs input);

        Task<VideoConfigs> GetVideoConfigAsync(int id);
        Task<IEnumerable<VideoConfigs>> GetVideoConfigsAsync();


        Task<int> AddAudioConfigAsync(AudioConfigs input);
        Task<int> UpdateAudioConfigAsync(AudioConfigs input);
        Task<int> DeleteAudioConfigAsync(AudioConfigs input);

        Task<AudioConfigs> GetAudioConfigAsync(int id);
        Task<IEnumerable<AudioConfigs>> GetAudioConfigsAsync();


        Task<int> AddDocumentConfigAsync(DocumentConfigs input);
        Task<int> UpdateDocumentConfigAsync(DocumentConfigs input);
        Task<int> DeleteDocumentConfigAsync(DocumentConfigs input);

        Task<DocumentConfigs> GetDocumentConfigAsync(int id);
        Task<IEnumerable<DocumentConfigs>> GetDocumentConfigsAsync();
    }
}
