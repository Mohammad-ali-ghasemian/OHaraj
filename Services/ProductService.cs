using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OHaraj.Core.Domain.DTOs;
using OHaraj.Core.Domain.Entities.Handling;
using OHaraj.Core.Domain.Entities.Shop;
using OHaraj.Core.Domain.Models.Product;
using OHaraj.Core.Interfaces.Repositories;
using OHaraj.Core.Interfaces.Services;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Exceptions;

namespace OHaraj.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string FileContainer;
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
            FileContainer = "Product";
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
                    path = await _uploaderService.SaveFile(FileContainer, input.MainImage, wattermark: true)
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
                        path = await _uploaderService.SaveFile(FileContainer, image, wattermark: true)
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

        public async Task<ProductDTO> UpdateProduct(UpdateProduct input)
        {
            var product = await _productRepository.GetProductAsync(input.Id);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد");
            }

            // update product main image from "FileManagement" table and static folder
            var mainFile = await _productRepository.GetFileToTableAsync(product.FileManagementId);

            int? fileId = null;
            if (input.MainImage != null)
            {
                mainFile.path = await _uploaderService.EditFile(FileContainer, input.MainImage, mainFile.path);
                fileId = await _productRepository.UpdateFileToTableAsync(mainFile);
            }
            else
            {
                await _uploaderService.DeleteFile(FileContainer, mainFile.path);
                await _productRepository.DeleteFileToTableAsync(mainFile);
            }

            // update product other images from "FileManagement" table and static folder
            foreach (var image in product.ProductImages)
            {
                var x = await _productRepository.GetFileToTableAsync(image.FileManagementId);
                await _productRepository.DeleteFileToTableAsync(x);
                await _uploaderService.DeleteFile(FileContainer, x.path);
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
                        path = await _uploaderService.SaveFile(FileContainer, image, wattermark: true)
                    });

                    otherImages.Add(new ProductImages
                    {
                        Order = pivot,
                        FileManagementId = imgId
                    });
                    ++pivot;
                }
            }

            product.Name = input.Name;
            product.Weight = input.Weight;
            product.Width = input.Width;
            product.Height = input.Height;
            product.Length = input.Length;
            product.Quantity = input.Quantity;
            product.ShortContent = input.ShortContent;
            product.Content = input.Content;
            product.Price = input.Price;
            product.DiscountPercent = input.DiscountPercent;
            product.IsActive = input.IsActive;
            product.CategoryId = input.CategoryId;
            product.ModelId = input.ModelId;
            product.FileManagementId = fileId;
            product.ProductImages = otherImages;

            await _productRepository.UpdateProductAsync(product);

            return _mapper.Map<ProductDTO>(product);

        }

        public async Task<int> DeleteProduct(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد");
            }

            await _productRepository.DeleteProductAsync(product);
            return product.Id;
        }

        public async Task<ProductDTO> GetProduct(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد!");
            }

            return _mapper.Map<ProductDTO>(product, opt => opt.Items["LikesNumberValue"] = product.ProductLikes.Count);
            
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProducts(string filter = null)
        {
            IEnumerable<Product> products;
            if (filter == "Active")
            {
                products = await _productRepository.GetActiveProductsAsync();
            }
            else if (filter == "Deactive")
            {
                products = await _productRepository.GetDeactiveProductsAsync();
            }
            else
            {
                products = await _productRepository.GetProductsAsync();
            }
            return products.Select(product =>
                _mapper.Map<ProductDTO>(product, opt => opt.Items["LikesNumberValue"] = product.ProductLikes.Count)
            );
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategotyAsync(categoryId);
            return products.Select(product =>
                _mapper.Map<ProductDTO>(product, opt => opt.Items["LikesNumberValue"] = product.ProductLikes.Count)
            );
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsByModel(int modelId)
        {
            var products = await _productRepository.GetProductsByModelAsync(modelId);
            return products.Select(product =>
                _mapper.Map<ProductDTO>(product, opt => opt.Items["LikesNumberValue"] = product.ProductLikes.Count)
            );
        }

        public async Task<bool> IsLikedByUser(int productId)
        {
            var user = await Current();
            return await _productRepository.IsLikedByUser(new ProductLike { ProductId = productId, UserId = user.Id}) != null ? true : false;
        }

        public async Task<CommentDTO> AddComment(UpsertComment input)
        {
            var user = await Current();
            ProductComment comment = new ProductComment
            {
                Text = input.Text,
                DateAdded = DateTime.Now,
                UserId = user.Id,
                ProductId = input.ProductId,
                IsApproved = false
            };

            await _productRepository.AddProductCommentAsync(comment);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<CommentDTO> UpdateComment(UpsertComment input)
        {
            var comment = await _productRepository.GetCommentAsync(input.Id);
            if (comment == null)
            {
                throw new NotFoundException("کامنت یافت نشد");
            }

            comment.Text = input.Text;
            comment.ProductId = input.ProductId;
            comment.IsApproved = false;
            comment.ProductId = input.ProductId;

            await _productRepository.UpdateProductCommentAsync(comment);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<CommentDTO> ToggleApprovalComment(int commentId, bool isApprove)
        {
            var comment = await _productRepository.GetCommentAsync(commentId);
            if (comment == null)
            {
                throw new NotFoundException("کامنت یافت نشد");
            }

            comment.IsApproved = isApprove;
            await _productRepository.UpdateProductCommentAsync(comment);
            return _mapper.Map<CommentDTO>(comment);
        }

        public async Task<int> DeleteComment(int commentId)
        {
            var comment = await _productRepository.GetCommentAsync(commentId);
            if (comment == null)
            {
                throw new NotFoundException("کامنت یافت نشد");
            }
            var user = await Current();
            var roles = await _authenticationRepository.GetUserRolesAsync(user);
            if (!roles.Any(r => r == "Admin" || r == "SuperAdmin"))
            {
                if (user.Id != comment.UserId)
                {
                    throw new BadRequestException("دسترسی غیرمجاز");
                }
            }

            await _productRepository.DeleteProductCommentAsync(comment);
            return commentId;
        }

        // Delete all product's comments
        public async Task<int> DeleteAllProductComments(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد");
            }

            await _productRepository.DeleteAllProductCommentsAsync(productId);
            return productId;
        }

        public async Task<CommentDTO> GetComment(int commentId)
        {
            var comment = await _productRepository.GetCommentAsync(commentId);
            if (comment == null)
            {
                throw new NotFoundException("کامنت یافت نشد");
            }

            return _mapper.Map<CommentDTO>(comment);
        }

        // Get all product's comments
        public async Task<IEnumerable<CommentDTO>> GetProductAllComments(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد");
            }

            var comments = await _productRepository.GetProductAllCommentsAsync(productId);

            return comments.Select(comment =>
                _mapper.Map<CommentDTO>(comment));
        }

        // Get product's (only) verified comments
        public async Task<IEnumerable<CommentDTO>> GetProductVerifiedComments(int productId, int? number)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد");
            }

            IEnumerable<ProductComment> comments;
            switch (number)
            {
                case 5:
                    comments = await _productRepository.GetProductFiveNewestVerifiedCommentsAsync(productId);
                    break;
                case 10:
                    comments = await _productRepository.GetProductTenNewestVerifiedCommentsAsync(productId);
                    break;
                case null:
                    comments = await _productRepository.GetProductAllVerifiedCommentsAsync(productId);
                    break;
                default:
                    throw new BadRequestException("تعداد نامعتبر");
            }

            return comments.Select(comment =>
                _mapper.Map<CommentDTO>(comment));
        }

        // Get product's (only) unverified comments
        public async Task<IEnumerable<CommentDTO>> GetProductUnverifiedComments(int productId)
        {
            var product = await _productRepository.GetProductAsync(productId);
            if (product == null)
            {
                throw new NotFoundException("محصول یافت نشد");
            }

            var comments = await _productRepository.GetProductUnverifiedCommentsAsync(productId);
            return comments.Select(comment =>
                _mapper.Map<CommentDTO>(comment));
        }

        // Get all unverified comments (everywhere)
        public async Task<IEnumerable<CommentDTO>> GetAllUnverifiedComments()
        {
            var comments = await _productRepository.GetAllUnverifiedCommentsAsync();
            return comments.Select(comment =>
                _mapper.Map<CommentDTO>(comment));
        }

        // Get user's all comments
        public async Task<IEnumerable<CommentDTO>> GetUserAllComments(string userId)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            var current = await Current();
            if (current.Id != userId)
            {
                throw new BadRequestException("دسترسی غیرمجاز");
            }

            var comments = await _productRepository.GetUserAllCommentsAsync(userId);
            return comments.Select(comment =>
                _mapper.Map<CommentDTO>(comment));
        }

        // Get user's (only) unverified comments
        public async Task<IEnumerable<CommentDTO>> GetUserUnverifiedComments(string userId)
        {
            var user = await _authenticationRepository.GetUserByIdAsync(userId);
            if (user == null )
            {
                throw new NotFoundException("کاربر یافت نشد");
            }

            var current = await Current();
            if (current.Id != userId)
            {
                throw new BadRequestException("دسترسی غیرمجاز");
            }

            var comments = await _productRepository.GetUserUnverifiedCommentsAsync(userId);
            return comments.Select(comment =>
                _mapper.Map<CommentDTO>(comment));
        }
    }
}
