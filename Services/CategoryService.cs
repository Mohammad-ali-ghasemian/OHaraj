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
using System.Xml.Linq;

namespace OHaraj.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string FileContainer;
        private readonly IFileStorageService _uploaderService;
        public CategoryService(
            ICategoryRepository categoryRepository,
            IAuthenticationRepository authenticationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService uploaderService
            )
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            FileContainer = "Category";
            _uploaderService = uploaderService;
        }

        public async Task<CategoryDTO> AddCategory(UpsertCategory input)
        {
            int? fileId = null;
            if (input.Image != null)
            {
                fileId = await _categoryRepository.AddFileToTableAsync(new FileManagement
                {
                    Type = Core.Enums.FileType.Image,
                    path = await _uploaderService.SaveFile(FileContainer, input.Image, wattermark: true)
                });
            }


            Category category = new Category
            {
                Name = input.Name,
                Description = input.Description,
                ParentId = input.ParentCategoryId,
                FileManagementId = fileId
            };

            await _categoryRepository.AddCategoryAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> UpdateCategory(UpsertCategory input)
        {
            var category = await _categoryRepository.GetCategoryAsync(input.Id);
            if (category == null)
            {
                throw new NotFoundException("کتگوری یافت نشد");
            }

            // update category main image from "FileManagement" table and static folder
            var mainFile = await _categoryRepository.GetFileToTableAsync(category.FileManagementId);

            int? fileId = null;
            if (input.Image != null)
            {
                if (mainFile == null)
                    mainFile = new FileManagement();
                mainFile.path = await _uploaderService.EditFile(FileContainer, input.Image, mainFile.path);
                fileId = await _categoryRepository.UpdateFileToTableAsync(mainFile);
            }
            else if (mainFile != null)
            {
                category.FileManagementId = null;
                await _categoryRepository.UpdateCategoryAsync(category); // جدا کردن وابستگی
                await _uploaderService.DeleteFile(FileContainer, mainFile.path);
                await _categoryRepository.DeleteFileToTableAsync(mainFile);
            }

            category.Name = input.Name;
            category.Description = input.Description;
            category.ParentId = input.ParentCategoryId;
            category.FileManagementId = fileId;

            await _categoryRepository.UpdateCategoryAsync(category);

            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<int> DeleteCategory(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException("کتگوری یافت نشد");
            }

            await _categoryRepository.DeleteCategoryAsync(category);

            var mainFile = await _categoryRepository.GetFileToTableAsync(category.FileManagementId);
            await _categoryRepository.DeleteFileToTableAsync(mainFile);
            await _uploaderService.DeleteFile(FileContainer, mainFile.path);

            return category.Id;
        }

        public async Task<CategoryDTO> GetCategory(int categoryId)
        {
            var category = await _categoryRepository.GetCategoryAsync(categoryId);
            if (category == null)
            {
                throw new NotFoundException("کتگوری یافت نشد!");
            }

            // extracting main image path
            var file = await _categoryRepository.GetFileToTableAsync(category.FileManagementId);

            var map = _mapper.Map<CategoryDTO>(category);
            if (file != null)
                map.ImagePath = file.path;

            return map;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetCategoriesAsync();

            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }
    }
}
