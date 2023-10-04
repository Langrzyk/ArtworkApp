using ArtworkApp.Data.Entities;

namespace ArtworkApp.Data.Repositories;

public interface IReadRepository<out T> 
    where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetById(int id);

    int GetItemCount();
}
