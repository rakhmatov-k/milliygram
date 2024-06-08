﻿using Microsoft.AspNetCore.Http;
using Milliygram.Domain.Enums;

namespace Milliygram.Service.Helpers;

public static class FileHelper
{
    public static async Task<(string Path, string Name)> CreateFileAsync(IFormFile file, FileType type)
    {
        var directoryPath = Path.Combine(EnvironmentHelper.WebRootPath, type.ToString());
        if (!Directory.Exists(directoryPath))
            Directory.CreateDirectory(directoryPath);

        var fileName = MakeFileName(file.FileName);
        var fullPath = Path.Combine(directoryPath, fileName);

        var stream = File.Create(fullPath);
        await file.CopyToAsync(stream);
        stream.Close();

        return (fullPath, fileName);
    }

    private static string MakeFileName(string fileName)
    {
        string fileExtension = Path.GetExtension(fileName);
        string guid = Guid.NewGuid().ToString();
        return $"{guid}{fileExtension}";
    }
}