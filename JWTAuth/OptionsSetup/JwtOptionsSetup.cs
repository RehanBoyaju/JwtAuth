using Infrastructure.Authentication;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace JWTAuth.OptionsSetup
{
    public class JwtOptionsSetup : IConfigureOptions<JwtOptions>
    {
        private readonly IConfiguration _configuration;
        private const string SectionName = "Jwt";
        public JwtOptionsSetup(IConfiguration configuration) {
            _configuration = configuration;
        }

        public void Configure(JwtOptions options)
        {
            _configuration.GetSection(SectionName).Bind(options);
        }
    }
}
        