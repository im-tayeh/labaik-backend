using Microsoft.Extensions.DependencyInjection;

namespace Labaik.Application;

/// <summary>Registers application-layer services (use cases, validators, pipeline behaviours).</summary>
public static class DependencyInjection
{
    /// <summary>Adds application services. MediatR and FluentValidation are registered here from phase 1.</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services) => services;
}
