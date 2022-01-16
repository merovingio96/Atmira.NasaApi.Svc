using Microsoft.Extensions.Configuration;

namespace Atmira.NasaApi.Svc.Configuration
{
    public static class Configurations
    {
        private static IConfiguration _configuration = Startup.Configuration;

        public static string ApiNasaUrl => _configuration["ApiNasa:Url"];

        public static string ApiNasaKey => _configuration["ApiNasa:ApiKey"];
    }
}