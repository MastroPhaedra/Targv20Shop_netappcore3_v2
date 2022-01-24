//using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Targv20Shop.Core.Domain;
using Targv20Shop.Core.Dtos;
using Targv20Shop.Core.ServiceInterface;
using Targv20Shop.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Targv20Shop.ApplicationServices.Services
{
    public class FileServices : IFileService
    {
        private readonly Targv20ShopDbContext _context;
        private readonly IWebHostEnvironment _env;
        //private readonly IFileService _file;

        //[Obsolete]
        public FileServices
            (
                Targv20ShopDbContext context,
                IWebHostEnvironment env //,
                //IFileService file
            )
        {
            _context = context;
            _env = env;
            //_file = file;
        }
        public async Task<ExistingFilePath> RemoveImage(ExistingFilePathDto dto)
        {
            var imageId = await _context.ExistingFilePath.FirstOrDefaultAsync(x => x.FilePath == dto.FilePath);

            var photoPath = _env.WebRootPath + "\\multipleFileUpload\\" + dto.FilePath;
            File.Delete(photoPath);

            _context.ExistingFilePath.Remove(imageId);
            await _context.SaveChangesAsync();

            return imageId;
        }

        public async Task<ExistingFilePath> RemoveImages(ExistingFilePathDto[] dto)
        {
            foreach (var dtos in dto)
            {
                var fileId = await _context.ExistingFilePath.FirstOrDefaultAsync(x => x.FilePath == dtos.FilePath);

                var photoPath = _env.WebRootPath + "\\multipleFileUpload\\" + dtos.FilePath;
                File.Delete(photoPath);

                _context.ExistingFilePath.Remove(fileId);
                await _context.SaveChangesAsync();

                //return fileId;
            }
            return null;
        }

        public string ProcessUploadedFile(ProductDto dto, Product product)
        {
            string uniqueFileName = null;

            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (dto.Files != null && dto.Files.Count > 0)
                {
                    if (!Directory.Exists(_env.WebRootPath + "\\multipleFileUpload\\"))
                    {
                        Directory.CreateDirectory(_env.WebRootPath + "\\multipleFileUpload\\");
                    }
                }

                foreach (var photo in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "multipleFileUpload");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(fileStream);

                        ExistingFilePath paths = new ExistingFilePath
                        {
                            Id = Guid.NewGuid(),
                            FilePath = uniqueFileName,
                            ProductId = product.Id
                        };

                        _context.ExistingFilePath.Add(paths);
                    }
                }
            }

            return uniqueFileName;
        }
    }
}
