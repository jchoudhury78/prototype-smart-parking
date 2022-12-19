using Microsoft.Extensions.Configuration;

namespace SmartParking.Domain
{
    public static class ConnectionService
    {
        public static IConfiguration Configuration { get; set; }
    }
}
