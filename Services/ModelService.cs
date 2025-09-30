using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Shop;
using OHaraj.Core.Domain.Models.Product;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using OHaraj.Repositories;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Exceptions;

namespace OHaraj.Services
{
    public class ModelService : IModelService
    {
        private readonly IModelRepository _modelRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string FileContainer;
        private readonly IFileStorageService _uploaderService;
        public ModelService(
            IModelRepository modelRepository,
            ICategoryRepository categoryRepository,
            IAuthenticationRepository authenticationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService uploaderService
            )
        {
            _modelRepository = modelRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            FileContainer = "Model";
            _uploaderService = uploaderService;
        }

        public async Task<ModelDTO> AddModel(UpsertModel input)
        {
            if (await _categoryRepository.GetCategoryAsync(input.CategoryId) == null)
            {
                throw new NotFoundException("کتگوری یافت نشد");
            }
            int? fileId = null;
            if (input.Image != null)
            {
                fileId = await _modelRepository.AddFileToTableAsync(new FileManagement
                {
                    Type = Core.Enums.FileType.Image,
                    path = await _uploaderService.SaveFile(FileContainer, input.Image, wattermark: true)
                });
            }

            Model model = new Model
            {
                Name = input.Name,
                Description = input.Description,
                CategoryId = input.CategoryId,
                FileManagementId = fileId
            };

            await _modelRepository.AddModelAsync(model);

            return _mapper.Map<ModelDTO>(model);
        }

        public async Task<ModelDTO> UpdateModel(UpsertModel input)
        {
            var model = await _modelRepository.GetModelAsync(input.Id);
            if (model == null)
            {
                throw new NotFoundException("مدل یافت نشد");
            }

            if (_categoryRepository.GetCategoryAsync(input.CategoryId) == null)
            {
                throw new NotFoundException("کتگوری یافت نشد");
            }

            // update model main image from "FileManagement" table and static folder
            var mainFile = await _modelRepository.GetFileToTableAsync(model.FileManagementId);

            int? fileId = null;
            if (input.Image != null)
            {
                if (mainFile == null)
                    mainFile = new FileManagement();
                mainFile.path = await _uploaderService.EditFile(FileContainer, input.Image, mainFile.path);
                fileId = await _modelRepository.UpdateFileToTableAsync(mainFile);
            }
            else if (mainFile != null)
            {
                model.FileManagementId = null;
                await _modelRepository.UpdateModelAsync(model); // جدا کردن وابستگی
                await _uploaderService.DeleteFile(FileContainer, mainFile.path);
                await _modelRepository.DeleteFileToTableAsync(mainFile);
            }

            model.Name = input.Name;
            model.Description = input.Description;
            model.CategoryId = input.CategoryId;
            model.FileManagementId = fileId;

            await _modelRepository.UpdateModelAsync(model);

            return _mapper.Map<ModelDTO>(model);
        }

        public async Task<int> DeleteModel(int modelId)
        {
            var model = await _modelRepository.GetModelAsync(modelId);
            if (model == null)
            {
                throw new NotFoundException("مدل یافت نشد");
            }

            await _modelRepository.DeleteModelAsync(model);

            var mainFile = await _modelRepository.GetFileToTableAsync(model.FileManagementId);
            await _modelRepository.DeleteFileToTableAsync(mainFile);
            await _uploaderService.DeleteFile(FileContainer, mainFile.path);

            return model.Id;
        }

        public async Task<ModelDTO> GetModel(int modelId)
        {
            var model = await _modelRepository.GetModelAsync(modelId);
            if (model == null)
            {
                throw new NotFoundException("مدل یافت نشد!");
            }

            // extracting main image path
            var file = await _modelRepository.GetFileToTableAsync(model.FileManagementId);

            var map = _mapper.Map<ModelDTO>(model);
            if (file != null)
                map.ImagePath = file.path;

            return map;
        }

        public async Task<IEnumerable<ModelDTO>> GetAllModels()
        {
            IEnumerable<Model> models = await _modelRepository.GetModelsAsync();

            return _mapper.Map<IEnumerable<ModelDTO>>(models);
        }
    }
}
