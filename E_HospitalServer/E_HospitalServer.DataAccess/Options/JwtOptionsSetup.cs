using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace E_HospitalServer.DataAccess.Options;

public sealed class JwtOptionsSetup(IConfiguration configuration) : IConfigureOptions<JwtOptions>
{
    public void Configure(JwtOptions options)
    {       
        configuration.GetSection("Jwt").Bind(options);
    }
}