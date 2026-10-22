using System.ComponentModel.DataAnnotations;

namespace Labaik.Api.Options;

/// <summary>Settings for the Seq structured-log server, used in local development only.</summary>
internal sealed class SeqOptions
{
    public const string SectionName = "Seq";

    /// <summary>Ingestion URL, for example http://localhost:5341. When missing, the Seq sink is disabled.</summary>
    [Url]
    public string? ServerUrl { get; set; }
}
