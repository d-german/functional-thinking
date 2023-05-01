using System.Collections.Immutable;

namespace functional_thinking;

public record HCDocumentType
{
    public string Id { get; init; }
    public string SystemName { get; init; }
}

public record HCDocumentTypes
{
    public IImmutableList<HCDocumentType> Items { get; init; }
}