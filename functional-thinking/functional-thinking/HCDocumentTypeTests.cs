using System.Collections.Immutable;
using NUnit.Framework;

namespace functional_thinking;

[TestFixture]
public class HCDocumentTypeTests
{
[Test]
public void DocumentTypesImmutableArray()
{
    var documentTypes = new HCDocumentTypes
    {
        Items = new List<HCDocumentType>
        {
            new HCDocumentType { Id = "1", SystemName = "SystemName1" },
            new HCDocumentType { Id = "2", SystemName = "SystemName2" },
        }.ToImmutableArray()
    };
    
    Assert.That(documentTypes.Items, Has.Count.EqualTo(2));
}

[Test]
public void DocumentTypesImmutableList()
{
    var hcDocumentTypes = new HCDocumentTypes
    {
        Items = new List<HCDocumentType>
        {
            new HCDocumentType { Id = "1", SystemName = "SystemName1" },
            new HCDocumentType { Id = "2", SystemName = "SystemName2" },
        }.ToImmutableList()
    };
    
    Assert.That(hcDocumentTypes.Items, Has.Count.EqualTo(2));
}
}