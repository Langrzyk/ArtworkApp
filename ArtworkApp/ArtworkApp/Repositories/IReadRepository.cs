using ArtworkApp.Entities;

namespace ArtworkApp.Repositories;

public interface IReadRepository<out T> 
    where T : class, IEntity
{
    IEnumerable<T> GetAll();
    T GetById(int id);
}
