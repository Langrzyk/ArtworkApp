using ArtworkApp.Data.Entities;
using ArtworkApp.Data.Repositories;

namespace ArtworkApp.Components.DataProviders;

public abstract class DataProviderBase<T> : IDataProvider<T>
    where T : EntityBase
{

    private readonly IRepository<T> _entitiesRepository;

    public DataProviderBase(IRepository<T> entitiesRepository)
    {
        _entitiesRepository = entitiesRepository;
    }

    public abstract string GetInfo(T entity);
    public abstract string GetProviderType();

    public decimal GetAveragePrice()
    {
        var paintings = _entitiesRepository.GetAll();
        decimal result = paintings.Average(x => x.Price);

        return result;
    }

    public decimal GetMaximumPrice()
    {
        var paintings = _entitiesRepository.GetAll();
        decimal result = paintings.Max(x => x.Price);

        return result;
    }

    public decimal GetMinimumPrice()
    {
        var paintings = _entitiesRepository.GetAll();
        decimal result = paintings.Min(x => x.Price);

        return result;
    }

    public int GetNumberOfPages(int recordsPerPage)
    {
        int result = (_entitiesRepository.GetItemCount() + recordsPerPage - 1) / recordsPerPage;

        return result;
    }

    public List<T> GetPage(int recordsPerPage, int pageNumber)
    {
        var paintings = _entitiesRepository.GetAll();

        return paintings
            .OrderBy(x => x.Id)
            .Skip(pageNumber * recordsPerPage)
            .Take(recordsPerPage)
            .ToList();
    }

    public List<T> TakeCheapiest(int howMany)
    {
        var paintings = _entitiesRepository.GetAll();

        return paintings
            .OrderBy(x => x.Price)
            .Take(howMany)
            .ToList();
    }

    public List<T> TakeMostExpensive(int howMany)
    {
        var paintings = _entitiesRepository.GetAll();

        return paintings
            .OrderByDescending(x => x.Price)
            .Take(howMany)
            .ToList();
    }
}
