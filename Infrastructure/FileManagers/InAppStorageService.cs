using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Project.Application.Contracts.Infrastructure;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Text;

namespace Project.Infrastructure.FileStorage
{
    public class InAppStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public InAppStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public Task DeleteFile(string containerName, string fileRoute)
        {
            if (string.IsNullOrEmpty(fileRoute))
            {
                return Task.CompletedTask;
            }

            var fileName = Path.GetFileName(fileRoute);
            var fileDirectory = Path.Combine(env.WebRootPath, containerName, fileName);

            if (File.Exists(fileDirectory))
            {
                File.Delete(fileDirectory);
            }

            return Task.CompletedTask;
        }

        public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
        {
            await DeleteFile(fileRoute, containerName);
            return await SaveFile(containerName, file);
        }

        public async Task<string> SaveFile(string containerName, IFormFile file, bool wattermark = false)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, containerName);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }


            string route = Path.Combine(folder, fileName);
            if (wattermark)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var content = ms.ToArray();
                    var encoder = new JpegEncoder()
                    {
                        Quality = 50
                    };
                    // Load the image using ImageSharp
                    using (var image = Image.Load(content))
                    {
                        // Load your watermark image
                        using (var watermark = Image.Load("wwwroot\\watermark.png"))
                        {
                            watermark.Mutate(ctx => ctx.Resize(new ResizeOptions
                            {
                                Size = new Size(image.Width / 4, image.Height / 4),
                                Mode = ResizeMode.Max
                            }));

                            // Apply the watermark
                            image.Mutate(ctx => ctx.DrawImage(watermark, new Point(image.Width - watermark.Width - 10, image.Height - watermark.Height - 10), 1f));
                            image.Save(route, encoder);
                        }
                    }
                }
            }
            else
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    var content = ms.ToArray();
                    await File.WriteAllBytesAsync(route, content);
                }
            }



            return Path.Combine("/", containerName, fileName).Replace("\\", "/");
        }
    }
}