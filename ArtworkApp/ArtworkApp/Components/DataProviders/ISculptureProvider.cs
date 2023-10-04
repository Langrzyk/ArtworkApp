using ArtworkApp.Data.Entities;

namespace ArtworkApp.Components.DataProviders;

public interface ISculptureProvider : IDataProvider<Sculpture>
{
    List<string> GetUniqueSculpturesMaterial();

}
