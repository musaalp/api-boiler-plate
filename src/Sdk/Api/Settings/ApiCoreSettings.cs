using Sdk.Api.Models;

namespace Sdk.Api.Settings
{
    public class ApiCoreSettings
    {
        public string ApiName { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }
}
