using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalRentCar.Shared.Configuracion
{
    public class AppSettings
    {
        public string UrlFrontend { get; set; }
        public SmtpConfiguration SmtpConfiguration { get; set; }
        public Jwt Jwt { get; set; }
        public StorageConfiguration StorageConfiguration { get; set; }

        public CloudinaryConfiguration CloudinaryConfiguration { get; set; }

        //public string GitHubToken { get; set; }  // Token de acceso personal para GitHub
        //public string GitHubOwner { get; set; }  // Propietario del repositorio
        //public string GitHubRepository { get; set; }  // Nombre del repositorio
        //public string GitHubBranchName { get; set; }  // Rama en la que se subirá el archivo

    }

    public class Jwt
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }

    public class SmtpConfiguration
    {
        public string Servidor { get; set; }
        public string Remitente { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int Puerto { get; set; }
        public bool UsarSsl { get; set; }
    }



    public class StorageConfiguration
    {
        public string Path { get; set; }
        public string PublicUrl { get; set; }
    }

    public class CloudinaryConfiguration
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

}
