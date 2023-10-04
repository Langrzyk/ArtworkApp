using ArtworkApp.Data.Entities;

namespace ArtworkApp.Components.DataProviders;

public interface IPaintingsProvider : IDataProvider<Painting>
{
    List<string> GetUniquePaintingsType();
    List<string> GetUniquePaintingsTechnic();

}

