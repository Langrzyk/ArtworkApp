using ArtworkApp.Entities;

namespace ArtworkApp.DataProviders;

public interface ISculptureProvider : IDataProvider<Sculpture>
{
    List<string> GetUniqueSculpturesMaterial();

}
