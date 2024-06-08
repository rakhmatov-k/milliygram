﻿using Milliygram.Service.DTOs.Assets;

namespace Milliygram.Service.Services.Assets;

public interface IAssetService
{
    Task<AssetViewModel> UploadAsync(AssetCreateModel model);
    Task<bool> DeleteAsync(long id);
    Task<AssetViewModel> GetByIdAsync(long id);
}
