using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Services.Interfaces
{
    public interface IEmailNotificationService
    {
        Task SendEmailNotificationAsync(string email, string subject, string message);
    }
}
