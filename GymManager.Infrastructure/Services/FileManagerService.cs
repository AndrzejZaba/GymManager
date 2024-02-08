﻿using GymManager.Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace GymManager.Infrastructure.Services;

public class FileManagerService : IFileManagerService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileManagerService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }
    public async Task Upload(IEnumerable<IFormFile> files)
    {
        var folderRoot = Path.Combine(_webHostEnvironment.WebRootPath, "Content", "Files");

        if (!Directory.Exists(folderRoot))
            Directory.CreateDirectory(folderRoot);

        if (files == null || !files.Any())
            return;

        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var filePath = Path.Combine(folderRoot, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }

    }
}
