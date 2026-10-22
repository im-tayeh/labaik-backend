namespace Labaik.Api.Endpoints;

/// <summary>Discovers every <see cref="IEndpointGroup"/> in this assembly and maps it under /api/v1.</summary>
internal static class EndpointRegistration
{
    public const string ApiV1Prefix = "/api/v1";

    public static RouteGroupBuilder MapApiV1(this IEndpointRouteBuilder app)
    {
        var v1 = app.MapGroup(ApiV1Prefix);

        var groupTypes = typeof(EndpointRegistration).Assembly
            .GetTypes()
            .Where(type => type is { IsClass: true, IsAbstract: false } && type.IsAssignableTo(typeof(IEndpointGroup)));

        foreach (var groupType in groupTypes)
        {
            var group = (IEndpointGroup)Activator.CreateInstance(groupType)!;
            group.Map(v1);
        }

        return v1;
    }
}
