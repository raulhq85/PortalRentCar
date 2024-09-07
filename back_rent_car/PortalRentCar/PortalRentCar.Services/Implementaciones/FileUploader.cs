using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octokit;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Configuracion;
using Account = CloudinaryDotNet.Account;

namespace PortalRentCar.Services.Implementaciones;

public class FileUploader : IFileUploader
{
    private readonly AppSettings _appSettings;
    private readonly ILogger<FileUploader> _logger;
    private readonly Cloudinary _cloudinary;

    public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> logger)
    {
        _appSettings = options.Value;
        _logger = logger;


        // Configurar Cloudinary
        var account = new Account(
            _appSettings.CloudinaryConfiguration.CloudName,
            _appSettings.CloudinaryConfiguration.ApiKey,
            _appSettings.CloudinaryConfiguration.ApiSecret
        );
        _cloudinary = new Cloudinary(account);


    }


    public async Task<string> UploadFileAsync(string? base64Imagen, string? archivo)
    {

        if (string.IsNullOrWhiteSpace(base64Imagen) || string.IsNullOrWhiteSpace(archivo))
            return string.Empty;

        try
        {
            // Convertir Base64 a byte[]
            var fileBytes = Convert.FromBase64String(base64Imagen);

            // Convertir byte[] a Stream
            using var stream = new MemoryStream(fileBytes);

            // Subir a Cloudinary
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(archivo, stream),
                PublicId = archivo,
                Overwrite = true
            };
            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            _logger.LogInformation("El archivo se subió a Cloudinary correctamente.");

            // Retornar la URL pública de la imagen
            return uploadResult.SecureUrl.ToString();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir archivos a Cloudinary: {Message}", ex.Message);
            return string.Empty;
        }

    }



}
