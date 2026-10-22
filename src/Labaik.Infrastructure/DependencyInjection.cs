using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Labaik.Infrastructure;

/// <summary>Registers infrastructure services (database, identity, email, storage, clock).</summary>
public static class DependencyInjection
{
    /// <summary>Adds infrastructure services. The database is registered here from phase 1.</summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        services.AddSingleton(TimeProvider.System);

        return services;
    }
}
