using System;
using System.Collections.Generic;
using System.Text;

namespace Sdk.Api.Models
{
    public class JwtSettings
    {
        public string JwtSecretKey { get; set; }
        public int ExpiryMinutes { get; set; }
    }
}
