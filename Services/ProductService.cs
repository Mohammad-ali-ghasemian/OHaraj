using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Shop;
using OHaraj.Core.Domain.Models.Product;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Contracts.Infrastructure;

namespace OHaraj.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string Usercontainer;
        private readonly IFileStorageService _uploaderService;
        public ProductService(
            IProductRepository productRepository,
            IAuthenticationRepository authenticationRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IFileStorageService uploaderService
            )
        {
            _productRepository = productRepository;
            _authenticationRepository = authenticationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            Usercontainer = "Product";
            _uploaderService = uploaderService;
        }

        public async Task<IdentityUser> Current()
        {
            var userPrincipal = _httpContextAccessor.HttpContext?.User;
            if (userPrincipal == null)
            {
                return null;
            }

            var user = await _authenticationRepository.GetUserByPrincipalAsync(userPrincipal);
            return user;
        }

        public async Task<ProductDTO> AddProduct(UpsertProduct input)
        {
            int? fileId = null;
            if (input.MainImage != null)
            {
                fileId = await _productRepository.AddFileToTableAsync(new FileManagement
                {
                    Type = Core.Enums.FileType.Image,
                    path = await _uploaderService.SaveFile(Usercontainer, input.MainImage, wattermark: true);
                });
            }

            List<ProductImages>? otherImages = null;
            if (input.OtherImages != null)
            {
                int imgId;
                int pivot = 1;
                foreach (var image in input.OtherImages)
                {
                    imgId = await _productRepository.AddFileToTableAsync(new FileManagement
                    {
                        Type = Core.Enums.FileType.Image,
                        path = await _uploaderService.SaveFile(Usercontainer, image, wattermark: true)
                    });

                    otherImages.Add(new ProductImages
                    {
                        Order = pivot,
                        FileManagementId = imgId
                    });
                    ++pivot;
                }
            }


            Product product = new Product
            {
                Name = input.Name,
                Weight = input.Weight,
                Width = input.Width,
                Height = input.Height,
                Length = input.Length,
                Quantity = input.Quantity,
                ShortContent = input.ShortContent,
                Content = input.Content,
                Price = input.Price,
                DiscountPercent = input.DiscountPercent,
                IsActive = input.IsActive,
                CategoryId = input.CategoryId,
                ModelId = input.ModelId,
                FileManagementId = fileId,
                ProductImages = otherImages
            };

            await _productRepository.AddProductAsync(product);
            
            return _mapper.Map<ProductDTO>(product);

        }

        public Task<int> DeleteProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetAllProducts()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> GetProduct(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDTO> UpdateProduct(UpsertProduct input)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDTO>> GetProductsByModel(int ModelId)
        {
            throw new NotImplementedException();
        }
    }
}
