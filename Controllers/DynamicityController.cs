using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Configs;
using OHaraj.Core.Domain.Entities.Management;
using OHaraj.Core.Domain.Entities.Settings;
using OHaraj.Core.Domain.Models.Dynamicity;
using OHaraj.Core.Domain.Models.Dynamicity.Configs;
using OHaraj.Core.Enums;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Responses;
using System.Net;

namespace OHaraj.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<string>), (int)HttpStatusCode.BadRequest)]
    public class DynamicityController : ControllerBase
    {
        private readonly IDynamicityService _dynamicityService;
        public DynamicityController(IDynamicityService dynamicityService)
        {
            _dynamicityService = dynamicityService;
        }

        /// <summary>
        /// Invalid menuId ~ new menu - Valid menuId ~ update menu - Invalid parentId ~ becomes main menu
        /// </summary>
        /// <returns></returns>
        [HttpPost("Upsert-Menu")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpsertMenu(UpsertMenu input) 
        {
            return new Response<int>(await _dynamicityService.UpsertMenu(input)).ToJsonResult();
        }

        //[HttpPost("Update-Menu")]
        //[Produces(typeof(Response<int>))]
        //public async Task<IActionResult> UpdateMenu(UpsertMenu input)
        //{
        //    return new Response<int>(await _dynamicityService.UpsertMenu(input)).ToJsonResult();
        //}

        [HttpPost("Delete-Menu")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteMenu(int menuId)
        {
            return new Response<int>(await _dynamicityService.DeleteMenu(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Menu")]
        [Produces(typeof(Response<MenuDTO>))]
        public async Task<IActionResult> GetMenu(int menuId)
        {
            return new Response<MenuDTO>(await _dynamicityService.GetMenu(menuId)).ToJsonResult();
        }

        /// <summary>
        /// Get all menus
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Menus")]
        [Produces(typeof(Response<IEnumerable<MenuDTO>>))]
        public async Task<IActionResult> GetMenus()
        {
            return new Response<IEnumerable<MenuDTO>>(await _dynamicityService.GetMenus()).ToJsonResult();
        }

        /// <summary>
        /// Send a menu id and see if the current user has access? (true/execption)
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Has-Current-User-Access")]
        [Produces(typeof(Response<bool>))]
        public async Task<IActionResult> HasCurrentUserAccess(int menuId)
        {
            return new Response<bool>(await _dynamicityService.HasCurrentUserAccess(menuId)).ToJsonResult();
        }

        /// <summary>
        /// By default, everyone can access all menus, except admins set specific roles for that menu
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Anonymous-User-Access-Menus")]
        [Produces(typeof(Response<IEnumerable<MenuDTO>>))]
        public async Task<IActionResult> GetAnonymousUserAccessMenus()
        {
            return new Response<IEnumerable<MenuDTO>>(await _dynamicityService.GetAnonymousUserAccessMenus()).ToJsonResult();
        }

        /// <summary>
        /// Must login first to use this
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Logined-User-Access-Menus")]
        [Produces(typeof(Response<IEnumerable<MenuDTO>>))]
        public async Task<IActionResult> GetLoginedUserAccessMenus()
        {
            return new Response<IEnumerable<MenuDTO>>(await _dynamicityService.GetLoginedUserAccessMenus()).ToJsonResult();
        }

        /// <summary>
        /// Must login first to use this
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Logined-User-Menus")]
        [Produces(typeof(Response<IEnumerable<MenuDTO>>))]
        public async Task<IActionResult> GetLoginedUserMenus()
        {
            return new Response<IEnumerable<MenuDTO>>(await _dynamicityService.GetLoginedUserMenus()).ToJsonResult();
        }

        /// <summary>
        /// Just for admins 
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get-Other-User-Access-Menus")]
        [Produces(typeof(Response<IEnumerable<MenuDTO>>))]
        public async Task<IActionResult> GetOtherUserAccessMenus(string userId)
        {
            return new Response<IEnumerable<MenuDTO>>(await _dynamicityService.GetOtherUserAccessMenus(userId)).ToJsonResult();
        }




        /// <summary>
        /// Which role can access which menu - Invalid id ~ new access - Valid id ~ update access
        /// </summary>
        /// <returns></returns>
        [HttpPost("Upsert-Access")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpsertAccess(UpsertRoleAccess input)
        {
            return new Response<int>(await _dynamicityService.UpsertAccess(input)).ToJsonResult();
        }

        [HttpPost("Delete-Access")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteAccess(int accessId)
        {
            return new Response<int>(await _dynamicityService.DeleteAccess(accessId)).ToJsonResult();
        }

        [HttpGet("Get-Access-By-Id")]
        [Produces(typeof(Response<RoleAccessDTO>))]
        public async Task<IActionResult> GetAccessById(int id)
        {
            return new Response<RoleAccessDTO>(await _dynamicityService.GetAccess(id)).ToJsonResult();
        }

        /// <summary>
        /// What menus can the role access (beside anonymous menus)
        /// SuperAdmin/Admin and the user who has this role can use this
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Role-Accesses")]
        [Produces(typeof(Response<IEnumerable<RoleAccessDTO>>))]
        public async Task<IActionResult> GetRoleAccesses(string roleId)
        {
            return new Response<IEnumerable<RoleAccessDTO>>(await _dynamicityService.GetAccess(roleId)).ToJsonResult();
        }

        [HttpGet("Get-All-Accesses")]
        [Produces(typeof(Response<IEnumerable<RoleAccessDTO>>))]
        public async Task<IActionResult> GetAllAccesses()
        {
            return new Response<IEnumerable<RoleAccessDTO>>(await _dynamicityService.GetAllAccesss()).ToJsonResult();
        }




        [HttpPost("Add-Image-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddImageConfig(UpsertImageConfig input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertImageConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Image-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateImageConfig(UpsertImageConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertImageConfig(input)).ToJsonResult();
        }

        /// <summary>
        /// Delet a config will delete all settings that were matching to it
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete-Image-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteImageConfig(int imageConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteImageConfig(imageConfigId)).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Image-Config")]
        [Produces(typeof(Response<ImageConfigs>))]
        public async Task<IActionResult> GetImageConfig(int imageConfigId)
        {
            return new Response<ImageConfigs>(await _dynamicityService.GetImageConfig(imageConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Image-Configs")]
        [Produces(typeof(Response<IEnumerable<ImageConfigs>>))]
        public async Task<IActionResult> GetImageConfigs()
        {
            return new Response<IEnumerable<ImageConfigs>>(await _dynamicityService.GetImageConfigs()).ToJsonResult();
        }



        [HttpPost("Add-Video-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddVideoConfig(UpsertVideoConfig input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertVideoConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Video-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateVideoConfig(UpsertVideoConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertVideoConfig(input)).ToJsonResult();
        }

        /// <summary>
        /// Delet a config will delete all settings that were matching to it
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete-Video-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteVideoConfig(int videoConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteVideoConfig(videoConfigId)).ToJsonResult();
        }


        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Video-Config")]
        [Produces(typeof(Response<VideoConfigs>))]
        public async Task<IActionResult> GetVideoConfig(int videoConfigId)
        {
            return new Response<VideoConfigs>(await _dynamicityService.GetVideoConfig(videoConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Video-Configs")]
        [Produces(typeof(Response<IEnumerable<VideoConfigs>>))]
        public async Task<IActionResult> GetVideoConfigs()
        {
            return new Response<IEnumerable<VideoConfigs>>(await _dynamicityService.GetVideoConfigs()).ToJsonResult();
        }



        [HttpPost("Add-Audio-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddAudioConfig(UpsertAudioConfig input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertAudioConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Audio-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateAudioConfig(UpsertAudioConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertAudioConfig(input)).ToJsonResult();
        }

        /// <summary>
        /// Delet a config will delete all settings that were matching to it
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete-Audio-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteAudioConfig(int audioConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteAudioConfig(audioConfigId)).ToJsonResult();
        }


        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Audio-Config")]
        [Produces(typeof(Response<AudioConfigs>))]
        public async Task<IActionResult> GetAudioConfig(int audioConfigId)
        {
            return new Response<AudioConfigs>(await _dynamicityService.GetAudioConfig(audioConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Audio-Configs")]
        [Produces(typeof(Response<IEnumerable<AudioConfigs>>))]
        public async Task<IActionResult> GetAudioConfigs()
        {
            return new Response<IEnumerable<AudioConfigs>>(await _dynamicityService.GetAudioConfigs()).ToJsonResult();
        }



        [HttpPost("Add-Document-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddDocumentConfig(UpsertDocumentConfig input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertDocumentConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Document-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateDocumentConfig(UpsertDocumentConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertDocumentConfig(input)).ToJsonResult();
        }

        /// <summary>
        /// Delet a config will delete all settings that were matching to it
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete-Document-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteDocumentConfig(int documentConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteDocumentConfig(documentConfigId)).ToJsonResult();
        }


        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Document-Config")]
        [Produces(typeof(Response<DocumentConfigs>))]
        public async Task<IActionResult> GetDocumentConfig(int documentConfigId)
        {
            return new Response<DocumentConfigs>(await _dynamicityService.GetDocumentConfig(documentConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Document-Configs")]
        [Produces(typeof(Response<IEnumerable<DocumentConfigs>>))]
        public async Task<IActionResult> GetDocumentConfigs()
        {
            return new Response<IEnumerable<DocumentConfigs>>(await _dynamicityService.GetDocumentConfigs()).ToJsonResult();
        }



        [HttpPost("Add-Text-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddTextConfig(UpsertTextConfig input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertTextConfig(input)).ToJsonResult();
        }

        [HttpPost("Update-Text-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateTextConfig(UpsertTextConfig input)
        {
            return new Response<int>(await _dynamicityService.UpsertTextConfig(input)).ToJsonResult();
        }

        /// <summary>
        /// Delet a config will delete all settings that were matching to it
        /// </summary>
        /// <returns></returns>
        [HttpPost("Delete-Text-Config")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteTextConfig(int textConfigId)
        {
            return new Response<int>(await _dynamicityService.DeleteTextConfig(textConfigId)).ToJsonResult();
        }


        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Text-Config")]
        [Produces(typeof(Response<TextConfigs>))]
        public async Task<IActionResult> GetTextConfig(int textConfigId)
        {
            return new Response<TextConfigs>(await _dynamicityService.GetTextConfig(textConfigId)).ToJsonResult();
        }

        [HttpGet("Get-Text-Configs")]
        [Produces(typeof(Response<IEnumerable<TextConfigs>>))]
        public async Task<IActionResult> GetTextConfigs()
        {
            return new Response<IEnumerable<TextConfigs>>(await _dynamicityService.GetTextConfigs()).ToJsonResult();
        }



        [HttpPost("Add-Image-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddImageSetting(UpsertSetting input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertImageSetting(input)).ToJsonResult();
        }

        [HttpPost("Update-Image-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateImageSetting(UpsertSetting input)
        {
            return new Response<int>(await _dynamicityService.UpsertImageSetting(input)).ToJsonResult();
        }

        [HttpPost("Delete-Image-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteImageSetting(int imageSettingId)
        {
            return new Response<int>(await _dynamicityService.DeleteImageSetting(imageSettingId)).ToJsonResult();
        }


        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Image-Setting")]
        [Produces(typeof(Response<ImageSettings>))]
        public async Task<IActionResult> GetImageSetting(int imageSettingId)
        {
            return new Response<ImageSettings>(await _dynamicityService.GetImageSetting(imageSettingId)).ToJsonResult();
        }

        [HttpGet("Get-Image-Settings")]
        [Produces(typeof(Response<IEnumerable<ImageSettings>>))]
        public async Task<IActionResult> GetImageSettings()
        {
            return new Response<IEnumerable<ImageSettings>>(await _dynamicityService.GetImageSettings()).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Image-Settings-By-MenuId")]
        [Produces(typeof(Response<IEnumerable<ImageSettings>>))]
        public async Task<IActionResult> GetImageSettingsByMenuId(int menuId)
        {
            return new Response<IEnumerable<ImageSettings>>(await _dynamicityService.GetImageSettingsByMenuId(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Image-Settings-By-Area")]
        [Produces(typeof(Response<IEnumerable<ImageSettings>>))]
        public async Task<IActionResult> GetImageSettingsByArea(Area area)
        {
            return new Response<IEnumerable<ImageSettings>>(await _dynamicityService.GetImageSettingsByArea(area)).ToJsonResult();
        }

        [HttpGet("Get-Image-Settings-By-ConfigId")]
        [Produces(typeof(Response<IEnumerable<ImageSettings>>))]
        public async Task<IActionResult> GetImageSettingsByConfigId(int imageConfigId)
        {
            return new Response<IEnumerable<ImageSettings>>(await _dynamicityService.GetImageSettingsByConfigId(imageConfigId)).ToJsonResult();
        }



        [HttpPost("Add-Video-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddVideoSetting(UpsertSetting input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertVideoSetting(input)).ToJsonResult();
        }

        [HttpPost("Update-Video-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateVideoSetting(UpsertSetting input)
        {
            return new Response<int>(await _dynamicityService.UpsertVideoSetting(input)).ToJsonResult();
        }

        [HttpPost("Delete-Video-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteVideoSetting(int videoSettingId)
        {
            return new Response<int>(await _dynamicityService.DeleteVideoSetting(videoSettingId)).ToJsonResult();
        }


        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Video-Setting")]
        [Produces(typeof(Response<VideoSettings>))]
        public async Task<IActionResult> GetVideoSetting(int videoSettingId)
        {
            return new Response<VideoSettings>(await _dynamicityService.GetVideoSetting(videoSettingId)).ToJsonResult();
        }

        [HttpGet("Get-Video-Settings")]
        [Produces(typeof(Response<IEnumerable<VideoSettings>>))]
        public async Task<IActionResult> GetVideoSettings()
        {
            return new Response<IEnumerable<VideoSettings>>(await _dynamicityService.GetVideoSettings()).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Video-Settings-By-MenuId")]
        [Produces(typeof(Response<IEnumerable<VideoSettings>>))]
        public async Task<IActionResult> GetVideoSettingsByMenuId(int menuId)
        {
            return new Response<IEnumerable<VideoSettings>>(await _dynamicityService.GetVideoSettingsByMenuId(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Video-Settings-By-Area")]
        [Produces(typeof(Response<IEnumerable<VideoSettings>>))]
        public async Task<IActionResult> GetVideoSettingsByArea(Area area)
        {
            return new Response<IEnumerable<VideoSettings>>(await _dynamicityService.GetVideoSettingsByArea(area)).ToJsonResult();
        }

        [HttpGet("Get-Video-Settings-By-ConfigId")]
        [Produces(typeof(Response<IEnumerable<VideoSettings>>))]
        public async Task<IActionResult> GetVideoSettingsByConfigId(int videoConfigId)
        {
            return new Response<IEnumerable<VideoSettings>>(await _dynamicityService.GetVideoSettingsByConfigId(videoConfigId)).ToJsonResult();
        }



        [HttpPost("Add-Audio-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddAudioSetting(UpsertSetting input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertAudioSetting(input)).ToJsonResult();
        }

        [HttpPost("Update-Audio-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateAudioSetting(UpsertSetting input)
        {
            return new Response<int>(await _dynamicityService.UpsertAudioSetting(input)).ToJsonResult();
        }

        [HttpPost("Delete-Audio-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteAudioSetting(int audioSettingId)
        {
            return new Response<int>(await _dynamicityService.DeleteAudioSetting(audioSettingId)).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Audio-Setting")]
        [Produces(typeof(Response<AudioSettings>))]
        public async Task<IActionResult> GetAudioSetting(int audioSettingId)
        {
            return new Response<AudioSettings>(await _dynamicityService.GetAudioSetting(audioSettingId)).ToJsonResult();
        }

        [HttpGet("Get-Audio-Settings")]
        [Produces(typeof(Response<IEnumerable<AudioSettings>>))]
        public async Task<IActionResult> GetAudioSettings()
        {
            return new Response<IEnumerable<AudioSettings>>(await _dynamicityService.GetAudioSettings()).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Audio-Settings-By-MenuId")]
        [Produces(typeof(Response<IEnumerable<AudioSettings>>))]
        public async Task<IActionResult> GetAudioSettingsByMenuId(int menuId)
        {
            return new Response<IEnumerable<AudioSettings>>(await _dynamicityService.GetAudioSettingsByMenuId(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Audio-Settings-By-Area")]
        [Produces(typeof(Response<IEnumerable<AudioSettings>>))]
        public async Task<IActionResult> GetAudioSettingsByArea(Area area)
        {
            return new Response<IEnumerable<AudioSettings>>(await _dynamicityService.GetAudioSettingsByArea(area)).ToJsonResult();
        }

        [HttpGet("Get-Audio-Settings-By-ConfigId")]
        [Produces(typeof(Response<IEnumerable<AudioSettings>>))]
        public async Task<IActionResult> GetAudioSettingsByConfigId(int audioConfigId)
        {
            return new Response<IEnumerable<AudioSettings>>(await _dynamicityService.GetAudioSettingsByConfigId(audioConfigId)).ToJsonResult();
        }



        [HttpPost("Add-Document-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddDocumentSetting(UpsertSetting input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertDocumentSetting(input)).ToJsonResult();
        }

        [HttpPost("Update-Document-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateDocumentSetting(UpsertSetting input)
        {
            return new Response<int>(await _dynamicityService.UpsertDocumentSetting(input)).ToJsonResult();
        }

        [HttpPost("Delete-Document-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteDocumentSetting(int documentSettingId)
        {
            return new Response<int>(await _dynamicityService.DeleteDocumentSetting(documentSettingId)).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Document-Setting")]
        [Produces(typeof(Response<DocumentSettings>))]
        public async Task<IActionResult> GetDocumentSetting(int documentSettingId)
        {
            return new Response<DocumentSettings>(await _dynamicityService.GetDocumentSetting(documentSettingId)).ToJsonResult();
        }

        [HttpGet("Get-Document-Settings")]
        [Produces(typeof(Response<IEnumerable<DocumentSettings>>))]
        public async Task<IActionResult> GetDocumentSettings()
        {
            return new Response<IEnumerable<DocumentSettings>>(await _dynamicityService.GetDocumentSettings()).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Document-Settings-By-MenuId")]
        [Produces(typeof(Response<IEnumerable<DocumentSettings>>))]
        public async Task<IActionResult> GetDocumentSettingsByMenuId(int menuId)
        {
            return new Response<IEnumerable<DocumentSettings>>(await _dynamicityService.GetDocumentSettingsByMenuId(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Document-Settings-By-Area")]
        [Produces(typeof(Response<IEnumerable<DocumentSettings>>))]
        public async Task<IActionResult> GetDocumentSettingsByArea(Area area)
        {
            return new Response<IEnumerable<DocumentSettings>>(await _dynamicityService.GetDocumentSettingsByArea(area)).ToJsonResult();
        }

        [HttpGet("Get-Document-Settings-By-ConfigId")]
        [Produces(typeof(Response<IEnumerable<DocumentSettings>>))]
        public async Task<IActionResult> GetDocumentSettingsByConfigId(int documentConfigId)
        {
            return new Response<IEnumerable<DocumentSettings>>(await _dynamicityService.GetDocumentSettingsByConfigId(documentConfigId)).ToJsonResult();
        }



        [HttpPost("Add-Text-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> AddTextSetting(UpsertSetting input)
        {
            input.Id = null;
            return new Response<int>(await _dynamicityService.UpsertTextSetting(input)).ToJsonResult();
        }

        [HttpPost("Update-Text-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> UpdateTextSetting(UpsertSetting input)
        {
            return new Response<int>(await _dynamicityService.UpsertTextSetting(input)).ToJsonResult();
        }

        [HttpPost("Delete-Text-Setting")]
        [Produces(typeof(Response<int>))]
        public async Task<IActionResult> DeleteTextSetting(int textSettingId)
        {
            return new Response<int>(await _dynamicityService.DeleteTextSetting(textSettingId)).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Text-Setting")]
        [Produces(typeof(Response<TextSettings>))]
        public async Task<IActionResult> GetTextSetting(int textSettingId)
        {
            return new Response<TextSettings>(await _dynamicityService.GetTextSetting(textSettingId)).ToJsonResult();
        }

        [HttpGet("Get-Text-Settings")]
        [Produces(typeof(Response<IEnumerable<TextSettings>>))]
        public async Task<IActionResult> GetTextSettings()
        {
            return new Response<IEnumerable<TextSettings>>(await _dynamicityService.GetTextSettings()).ToJsonResult();
        }

        /// <summary>
        /// Everyone can access
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("Get-Text-Settings-By-MenuId")]
        [Produces(typeof(Response<IEnumerable<TextSettings>>))]
        public async Task<IActionResult> GetTextSettingsByMenuId(int menuId)
        {
            return new Response<IEnumerable<TextSettings>>(await _dynamicityService.GetTextSettingsByMenuId(menuId)).ToJsonResult();
        }

        [HttpGet("Get-Text-Settings-By-Area")]
        [Produces(typeof(Response<IEnumerable<TextSettings>>))]
        public async Task<IActionResult> GetTextSettingsByArea(Area area)
        {
            return new Response<IEnumerable<TextSettings>>(await _dynamicityService.GetTextSettingsByArea(area)).ToJsonResult();
        }

        [HttpGet("Get-Text-Settings-By-ConfigId")]
        [Produces(typeof(Response<IEnumerable<TextSettings>>))]
        public async Task<IActionResult> GetTextSettingsByConfigId(int textConfigId)
        {
            return new Response<IEnumerable<TextSettings>>(await _dynamicityService.GetTextSettingsByConfigId(textConfigId)).ToJsonResult();
        }



        [HttpPost("Upsert-Role")]
        [Produces(typeof(Response<string>))]
        public async Task<IActionResult> UpsertRole(UpsertRole input)
        {
            return new Response<string>(await _dynamicityService.UpsertRole(input)).ToJsonResult();
        }

        [HttpPost("Delete-Role")]
        [Produces(typeof(Response<string>))]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            return new Response<string>(await _dynamicityService.DeleteRole(roleId)).ToJsonResult();
        }

        [HttpGet("Get-Role")]
        [Produces(typeof(Response<RoleDTO>))]
        public async Task<IActionResult> GetRole(string roleId)
        {
            return new Response<RoleDTO>(await _dynamicityService.GetRole(roleId)).ToJsonResult();
        }

        [HttpGet("Get-Roles")]
        [Produces(typeof(Response<IEnumerable<RoleDTO>>))]
        public async Task<IActionResult> GetRoles()
        {
            return new Response<IEnumerable<RoleDTO>>(await _dynamicityService.GetRoles()).ToJsonResult();
        }

        /// <summary>
        /// Give defiened roles to members (of course except SuperAdmin/Admin/User)
        /// </summary>
        /// <returns></returns>
        [HttpPost("Give-Roles")]
        [Produces(typeof(Response<IEnumerable<RoleDTO>>))]
        public async Task<IActionResult> GiveRoles([FromForm] string userId, [FromForm] IEnumerable<string> roleNames)
        {
            return new Response<IEnumerable<RoleDTO>>(await _dynamicityService.GiveRoles(userId, roleNames)).ToJsonResult();
        }

        /// <summary>
        /// Take roles from members (of course except SuperAdmin/Admin/User)
        /// </summary>
        /// <returns></returns>
        [HttpPost("Take-Roles")]
        [Produces(typeof(Response<IEnumerable<RoleDTO>>))]
        public async Task<IActionResult> TakeRoles([FromForm] string userId, [FromForm] IEnumerable<string> roleNames)
        {
            return new Response<IEnumerable<RoleDTO>>(await _dynamicityService.TakeRoles(userId, roleNames)).ToJsonResult();
        }

        //[HttpPost("test")]
        //[Produces(typeof(Response<int>))]
        //public async Task<IActionResult> test()
        //{
        //    return new Response<int>(2).ToJsonResult();
        //}
    }
}
