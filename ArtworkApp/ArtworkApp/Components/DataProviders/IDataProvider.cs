using ArtworkApp.Data.Entities;

namespace ArtworkApp.Components.DataProviders;

public interface IDataProvider<T> where T : class, IEntity
{
    List<T> GetPage(int recordsPerPage, int pageNumber);

    int GetNumberOfPages(int recordsPerPage);

    string GetInfo(T entity);
    string GetProviderType();
    decimal GetMinimumPrice();
    decimal GetMaximumPrice();
    decimal GetAveragePrice();

    List<T> TakeCheapiest(int howMany);
    List<T> TakeMostExpensive(int howMany);
}
