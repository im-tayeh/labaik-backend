namespace Labaik.Api.Options;

/// <summary>Controls whether the OpenAPI document and the Scalar UI are exposed.</summary>
internal sealed class OpenApiDocsOptions
{
    public const string SectionName = "OpenApi";

    public const string DocumentName = "v1";

    /// <summary>True in Development, in tests, and on Render while the Flutter team integrates.</summary>
    public bool Enabled { get; set; }
}
