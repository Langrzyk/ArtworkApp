using ArtworkApp.Entities;
using System.Diagnostics;
using System.Text.Json;
using System.Xml.Linq;

namespace ArtworkApp.Repositories;

public class RepositoryInFiles<T> : IRepository<T>
    where T : class, IEntity
{
    private readonly List<T> _itemsSet = new();
    private readonly List<T> _itemsToAdd = new();
    private readonly List<int> _itemsIdToRemove = new();
    private readonly string _filename = $"{typeof(T).Name}.txt";

    public RepositoryInFiles()
    {
    }

    public void Add(T item)
    {
        item.Id = _itemsToAdd.Count + 1;
        _itemsToAdd.Add(item);
    }

    public IEnumerable<T> GetAll()
    {
        CreateFileIfNotExist();

        List<string> jsonItemsAll = File.ReadAllLines(_filename).ToList();

        foreach (var jsonItem in jsonItemsAll)
        {
            if(jsonItem is not null)
            {
                _itemsSet.Add(JsonSerializer.Deserialize<T>(jsonItem));
            }
        }

        return _itemsSet.ToList();
    }

    public T GetById(int id)
    {
        IEnumerable<T> items = GetAll();
        return items.Single(x => x.Id == id);
    }

    public void Remove(T item)
    {
        _itemsIdToRemove.Add(item.Id);
    }

    public void Save()
    {
        CreateFileIfNotExist();

        string tempFile = Path.GetTempFileName();

        using (var sReader = new StreamReader(_filename))
        {
            using (var sWriter = new StreamWriter(tempFile))
            {
                string readJson;
                int id = 0;

                while ((readJson = sReader.ReadLine()) != null)
                {
                    var item = JsonSerializer.Deserialize<T>(readJson);
                    if (item is not null)
                    {
                        id = item.Id;
                        if (!_itemsIdToRemove.Contains(id))
                        {
                            sWriter.WriteLine(readJson);
                        }
                    }
                }

                foreach (var itemToAdd in _itemsToAdd)
                {
                    itemToAdd.Id += id;
                    sWriter.WriteLine(JsonSerializer.Serialize<T>(itemToAdd));
                }
            }
        }

        File.Delete(_filename);
        File.Move(tempFile, _filename);
    }
    private void CreateFileIfNotExist()
    {
        if (!File.Exists(_filename))
        {
            using (File.Create(_filename)) { };
        }
    }
}
