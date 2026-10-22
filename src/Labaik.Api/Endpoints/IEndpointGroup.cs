namespace Labaik.Api.Endpoints;

/// <summary>A feature area that maps its endpoints under the /api/v1 route group.</summary>
internal interface IEndpointGroup
{
    void Map(IEndpointRouteBuilder app);
}
