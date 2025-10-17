using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Enums;

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
        Task<IEnumerable<Menu>> GetAccessMenusAsync(IEnumerable<string> roleIds);



        //Role Access
        Task<int> AddAccessAsync(RoleAccess input);
        Task<int> UpdateAccessAsync(RoleAccess input);
        Task<int> DeleteAccessAsync(RoleAccess input);

        Task<RoleAccess> GetAccessAsync(int id);
        Task<IEnumerable<RoleAccess>> GetAccessesAsync();
        Task<IEnumerable<RoleAccess>> GetAccessByRoleAsync(string roleId);



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



        //Settings
        Task<int> AddImageSettingAsync(ImageSettings input);
        Task<int> UpdateImageSettingAsync(ImageSettings input);
        Task<int> DeleteImageSettingAsync(ImageSettings input);

        Task<ImageSettings> GetImageSettingAsync(int id);
        Task<IEnumerable<ImageSettings>> GetImageSettingsAsync();
        Task<IEnumerable<ImageSettings>> GetImageSettingsByMenuIdAsync(int menuId);
        Task<IEnumerable<ImageSettings>> GetImageSettingsByAreaAsync(Area area);
        Task<IEnumerable<ImageSettings>> GetImageSettingsByConfigIdAsync(int configId);


        Task<int> AddVideoSettingAsync(VideoSettings input);
        Task<int> UpdateVideoSettingAsync(VideoSettings input);
        Task<int> DeleteVideoSettingAsync(VideoSettings input);

        Task<VideoSettings> GetVideoSettingAsync(int id);
        Task<IEnumerable<VideoSettings>> GetVideoSettingsAsync();
        Task<IEnumerable<VideoSettings>> GetVideoSettingsByMenuIdAsync(int menuId);
        Task<IEnumerable<VideoSettings>> GetVideoSettingsByAreaAsync(Area area);
        Task<IEnumerable<VideoSettings>> GetVideoSettingsByConfigIdAsync(int configId);


        Task<int> AddAudioSettingAsync(AudioSettings input);
        Task<int> UpdateAudioSettingAsync(AudioSettings input);
        Task<int> DeleteAudioSettingAsync(AudioSettings input);

        Task<AudioSettings> GetAudioSettingAsync(int id);
        Task<IEnumerable<AudioSettings>> GetAudioSettingsAsync();
        Task<IEnumerable<AudioSettings>> GetAudioSettingsByMenuIdAsync(int menuId);
        Task<IEnumerable<AudioSettings>> GetAudioSettingsByAreaAsync(Area area);
        Task<IEnumerable<AudioSettings>> GetAudioSettingsByConfigIdAsync(int configId);


        Task<int> AddDocumentSettingAsync(DocumentSettings input);
        Task<int> UpdateDocumentSettingAsync(DocumentSettings input);
        Task<int> DeleteDocumentSettingAsync(DocumentSettings input);

        Task<DocumentSettings> GetDocumentSettingAsync(int id);
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsAsync();
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByMenuIdAsync(int menuId);
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByAreaAsync(Area area);
        Task<IEnumerable<DocumentSettings>> GetDocumentSettingsByConfigIdAsync(int configId);




        //Roles
        Task<IdentityResult> AddRoleAsync(string roleName);
        Task<IdentityResult> UpdateRoleAsync(IdentityRole role);
        Task<IdentityResult> DeleteRoleAsync(IdentityRole role);
        Task<IdentityRole> GetRoleAsync(string roleId);
        Task<IEnumerable<IdentityRole>> GetRolesAsync(string userId);
        Task<IEnumerable<IdentityRole>> GetRolesAsync();

        // Default : every login has at least "User" role
        // Default : cannot give "User" and "Admin" or "SuperAdmin" role to another user (SuperAdmin can give "admin")
        // Default : cannot take "User" and "Admin" or "SuperAdmin" role from another user (SuperAdmin can demotion "admin")
        // Give role to the user, gives back some identity roles for later : fetch the name of the roles (string) then return
        Task<IEnumerable<IdentityRole>> GiveRolesAsync(string userId, IEnumerable<IdentityRole> roles);
        Task<IEnumerable<IdentityRole>> TakeRolesAsync(string userId, IEnumerable<IdentityRole> roles);

    }
}
