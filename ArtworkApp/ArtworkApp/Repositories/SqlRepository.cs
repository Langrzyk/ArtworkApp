using ArtworkApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArtworkApp.Repositories;

public class SqlRepository<T> : IRepository<T>
    where T : class, IEntity
{
    private readonly DbContext _dbContext;

    private readonly DbSet<T> _dbSet;

    public SqlRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public event EventHandler<T>? ItemAdded;
    public event EventHandler<T>? ItemRemoved;
    public event EventHandler? ContextSaved;

    public void Add(T item)
    {
        _dbSet.Add(item);
        ItemAdded?.Invoke(this, item);
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public T GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public int GetItemCount()
    {
        var result = _dbSet.Count();

        return result;
    }

    public void Remove(T item)
    {
        _dbSet.Remove(item);
        ItemRemoved?.Invoke(this, item);
    }

    public void Save()
    {
        _dbContext.SaveChanges();
        ContextSaved?.Invoke(this, new EventArgs());
    }
}
