using ArtworkApp.Data.Entities;
using ArtworkApp.Data.Repositories;
using ArtworkApp.UserCommunication;

namespace ArtworkApp.Services.EventHandlerService;

public class EventHandlerService : IEventHandlerService
{
    private readonly IRepository<Painting> _paintingRepository;
    private readonly IRepository<Sculpture> _sculptureRepository;

    public EventHandlerService(
        IRepository<Painting> paintingRepository,
        IRepository<Sculpture> sculptureRepository)
    {
        _paintingRepository = paintingRepository;
        _sculptureRepository = sculptureRepository;

    }

    public void ListenForEvents()
    {
        _paintingRepository.ItemAdded += OnItemAdded;
        _paintingRepository.ItemRemoved += OnItemRemoved;
        _sculptureRepository.ItemAdded += OnItemAdded;
        _sculptureRepository.ItemRemoved += OnItemRemoved;
    }

    private void OnItemRemoved(object? sender, IEntity e)
    {
        if (sender is not null)
        {
            var senderName = sender.GetType().Name;
            SaveInLogFile($"{senderName.Substring(0, senderName.Length - 2)}", $"{e.GetType().Name}Removed", e.ToString() ?? "");
        }
    }

    private static void OnItemAdded(object? sender, IEntity e)
    {
        if (sender is not null)
        {
            var senderName = sender.GetType().Name;
            SaveInLogFile($"{senderName.Substring(0, senderName.Length - 2)}", $"{e.GetType().Name}Added", e.ToString() ?? "");
        }
    }

    private static void SaveInLogFile(string repository, string action, string comment)
    {
        using (var writer = File.AppendText($"ArtworkAppLOG.txt"))
        {
            writer.WriteLine($"[{DateTime.Now}]-{repository}-{action}-[{comment}]");
        }
    }
}
