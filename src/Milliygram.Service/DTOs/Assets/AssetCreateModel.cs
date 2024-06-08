using Microsoft.AspNetCore.Http;
using Milliygram.Domain.Enums;

namespace Milliygram.Service.DTOs.Assets;

public class AssetCreateModel
{
    public IFormFile File { get; set; }
    public FileType FileType { get; set; }
}