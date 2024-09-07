using System;

namespace PortalRentCar.Services.Interfaces;

public interface IFileUploader
{
    Task<string> UploadFileAsync(string? base64Imagen, string? archivo);
}
