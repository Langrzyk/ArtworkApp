using ArtworkApp.Entities;

namespace ArtworkApp.Repositories;

public interface IRepository<T> : IReadRepository<T>, IWriteRepository<T> 
    where T : class, IEntity
{

}
