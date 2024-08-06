using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortalRentCar.Shared.Configuracion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PortalRentCar.Services.Interfaces;

namespace PortalRentCar.Services.Implementaciones
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly ILogger<EmailNotificationService> _logger;
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailNotificationService(ILogger<EmailNotificationService> logger, IOptions<AppSettings> options)
        {
            _logger = logger;
            _smtpConfiguration = options.Value.SmtpConfiguration;
        }

        public async Task SendEmailNotificationAsync(string email, string subject, string message)
        {

            try
            {
                var smtpClient = new SmtpClient(_smtpConfiguration.Servidor)
                {
                    Port = _smtpConfiguration.Puerto,
                    Credentials = new NetworkCredential(_smtpConfiguration.Usuario, _smtpConfiguration.Password),
                    EnableSsl = _smtpConfiguration.UsarSsl,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_smtpConfiguration.Usuario, _smtpConfiguration.Remitente),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true,
                };

                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);

                _logger.LogInformation("Correo enviado correctamente a {email}", email);
            }
            catch (SmtpException ex)
            {
                _logger.LogError("Error enviando correo a {email}. Exception: {exception}", email, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error inesperado enviando correo a {email}. Exception: {exception}", email, ex.Message);
            }





            //try
            //{
            //    var smtpClient = new SmtpClient(_smtpConfiguration.Servidor)
            //    {
            //        Port = _smtpConfiguration.Puerto,
            //        Credentials = new NetworkCredential(_smtpConfiguration.Usuario, _smtpConfiguration.Password),
            //        EnableSsl = _smtpConfiguration.UsarSsl,
            //    };

            //    var mailMessage = new MailMessage
            //    {
            //        From = new MailAddress(_smtpConfiguration.Usuario, _smtpConfiguration.Remitente),
            //        Subject = subject,
            //        Body = message,
            //        IsBodyHtml = true,
            //    };

            //    mailMessage.To.Add(email);

            //    var sendTask = smtpClient.SendMailAsync(mailMessage);
            //    await sendTask;

            //    if (sendTask.IsCompletedSuccessfully)
            //        _logger.LogInformation("Correo enviado correctamente a {email}", email);
            //    else
            //        _logger.LogError("Error enviando correo a {email}", email);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error enviando email a {email} {message}", email, ex.Message);
            //}
        }
    }
}
