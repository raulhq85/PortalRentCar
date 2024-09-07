using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PortalRentCar.DataAcces;
using PortalRentCar.Repositories.Interfaces;
using PortalRentCar.Services.Interfaces;
using PortalRentCar.Shared.Configuracion;
using PortalRentCar.Shared.Request;
using PortalRentCar.Shared.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using PortalRentCar.Entities;
using PortalRentCar.Shared;
using Microsoft.AspNetCore.WebUtilities;

namespace PortalRentCar.Services.Implementaciones
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppSettings _configuration;
        private readonly UserManager<RentCarIdentityUser> _rentCarIdentityUserManager;
        private readonly ILogger<UsuarioService> _logger;
        private readonly IClienteRepository _clienteRepository;
        private readonly IEmailNotificationService _emailNotificationService;

        public UsuarioService(UserManager<RentCarIdentityUser> rentCarIdentityUserManager,
        ILogger<UsuarioService> logger, IClienteRepository clienteRepository, IEmailNotificationService emailNotificationService, IOptions<AppSettings> options)
        {
            _configuration = options.Value;
            _rentCarIdentityUserManager = rentCarIdentityUserManager;
            _logger = logger;
            _clienteRepository = clienteRepository;
            _emailNotificationService = emailNotificationService;

        }

        public async Task<LoginDtoResponse> LoginAsync(LoginDtoRequest request)
        {
            var response = new LoginDtoResponse();

            try
            {
                var identity = await _rentCarIdentityUserManager.FindByNameAsync(request.Usuario);

                if (identity is null)
                    throw new SecurityException("Usuario no existe");

                if (await _rentCarIdentityUserManager.IsLockedOutAsync(identity))
                {
                    throw new SecurityException($"Demasiados intentos fallidos para el usuario {request.Usuario}");
                }

                if (!await _rentCarIdentityUserManager.CheckPasswordAsync(identity, request.Password))
                {
                    response.ErrorMessage = "Usuario o clave incorrecta";
                    _logger.LogWarning($"Intento fallido de Login para el usuario {identity.UserName}");

                    await _rentCarIdentityUserManager.AccessFailedAsync(identity); // Esto aumenta el contador de bloqueo

                    return response;
                }

                var roles = await _rentCarIdentityUserManager.GetRolesAsync(identity);
                var fechaExpiracion = DateTime.Now.AddHours(1);

                // Vamos a devolver los Claims
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, identity.RazonSocial),
                    new Claim(ClaimTypes.Email, identity.Email!),
                    new Claim(ClaimTypes.Expiration, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss")),
                };

                claims.AddRange(roles.Select(x => new Claim(ClaimTypes.Role, x)));

                response.Perfiles = roles.ToList();

                var llaveSimetrica = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Jwt.SecretKey));
                var credenciales = new SigningCredentials(llaveSimetrica, SecurityAlgorithms.HmacSha256);

                var header = new JwtHeader(credenciales);

                var payload = new JwtPayload(
                    _configuration.Jwt.Issuer,
                    _configuration.Jwt.Audience,
                    claims,
                    DateTime.Now,
                    fechaExpiracion
                );


                var jwtToken = new JwtSecurityToken(header, payload);

                response.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                response.RazonSocial = identity.RazonSocial;
                response.Success = true;

                _logger.LogInformation("Se creó el JWT de forma satisfactoria");
            }
            catch (SecurityException ex)
            {
                response.ErrorMessage = ex.Message;
                _logger.LogError(ex, "Error de seguridad {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error inesperado";
                _logger.LogError(ex, "Error al autenticar {Message}", ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> RegisterAsync(RegistrarUsuarioDto request)
        {
            var response = new BaseResponse();

            try

            {
                var identity = new RentCarIdentityUser()
                {
                    RazonSocial = request.NombresCompleto,
                    UserName = request.Usuario,
                    Email = request.Email,
                    EmailConfirmed = true
                };

                var result = await _rentCarIdentityUserManager.CreateAsync(identity, request.Password);
                if (result.Succeeded)
                {
                    await _rentCarIdentityUserManager.AddToRoleAsync(identity, Constantes.RolCliente);

                    var cliente = new Cliente()
                    {
                        RazonSocial = request.NombresCompleto,
                        Correo = request.Email,
                        NroDocumento = request.NroDocumento,
                        FechaCreacion = DateTime.Today,
                        Telefono = request.Telefono
                    };

                    await _clienteRepository.AddAsync(cliente);

                    // Enviar un email
                    await _emailNotificationService.SendEmailNotificationAsync(request.Email, "Portal Rent Car - Registro",
                        $@"<strong><p>Felicidades {request.NombresCompleto}</p></strong>
                     <p>Su cuenta ha sido creada satisfactoriamente</p>");
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (var identityError in result.Errors)
                    {
                        sb.AppendFormat("{0} ", identityError.Description);
                    }

                    response.ErrorMessage = sb.ToString();
                    sb.Clear();
                }

                response.Success = result.Succeeded;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al registrar";
                _logger.LogWarning(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }

            return response;
        }

        public async Task<BaseResponse> ResetPasswordAsync(ResetPasswordDtoRequest request)
        {
            var response = new BaseResponse();
            try
            {
                var user = await _rentCarIdentityUserManager.FindByEmailAsync(request.Email);
                if (user is null)
                    throw new SecurityException("Usuario no existe");

                byte[] decodeByte = WebEncoders.Base64UrlDecode(request.Token);
                var token = Encoding.UTF8.GetString(decodeByte);

                var result = await _rentCarIdentityUserManager.ResetPasswordAsync(user, request.Token, request.Clave);
                if (result.Succeeded)
                {
                    response.Success = true;
                    _logger.LogInformation("Contraseña restablecida con éxito");

                    // TODO: Enviar un correo con el mensaje.
                    await _emailNotificationService.SendEmailNotificationAsync(request.Email, "Reseteo de Contraseña",
                        "Tu contraseña fue cambiado correctamente.");
                }
                else
                {
                    var sb = new StringBuilder();
                    foreach (var identityError in result.Errors)
                    {
                        sb.AppendFormat("{0} ", identityError.Description);
                    }

                    response.ErrorMessage = sb.ToString();
                    sb.Clear();
                    _logger.LogError("Error al restablecer la contraseña");
                }

                response.Success = result.Succeeded;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al restablecer la contraseña";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }

        public async Task<BaseResponse> SendTokenToResetPasswordAsync(GenerateTokenToResetDtoRequest request)
        {
            var response = new BaseResponseGeneric<string>();
            try
            {
                RentCarIdentityUser? user = null;

                if (!string.IsNullOrEmpty(request.Usuario))
                {
                    user = await _rentCarIdentityUserManager.FindByNameAsync(request.Usuario);
                    if (user is null) throw new SecurityException("Usuario no existe");
                }
                else if (!string.IsNullOrEmpty(request.Email))
                {
                    user = await _rentCarIdentityUserManager.FindByEmailAsync(request.Email);
                    if (user is null) throw new SecurityException("Usuario no existe");
                }

                if (user is null)
                {
                    response.ErrorMessage = "Usuario no pudo ser validado";
                    return response;
                }

                var token = await _rentCarIdentityUserManager.GeneratePasswordResetTokenAsync(user);

                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                await _emailNotificationService.SendEmailNotificationAsync(user.Email!, "Rent Car - Solicitud de cambio de contraseña",
                    @$"Por favor, utilice el siguiente token para restablecer su contraseña, haga clic aquí:
                    <p><a href=""{_configuration.UrlFrontend}/reset-password?email={request.Email}&token={token}"">Recuperar clave</a></p>");

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Error al generar el token para restablecer la contraseña";
                _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
            }
            return response;
        }
    }
}
