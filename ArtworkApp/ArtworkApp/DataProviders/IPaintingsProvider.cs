using ArtworkApp.Entities;

namespace ArtworkApp.DataProviders;

public interface IPaintingsProvider : IDataProvider<Painting>
{
    List<string> GetUniquePaintingsType();
    List<string> GetUniquePaintingsTechnic();

}

