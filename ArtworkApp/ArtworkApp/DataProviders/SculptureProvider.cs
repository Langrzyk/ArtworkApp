using ArtworkApp.Entities;
using ArtworkApp.Repositories;
using System.Text;

namespace ArtworkApp.DataProviders;

public class SculptureProvider : DataProviderBase<Sculpture>, ISculptureProvider
{
    private readonly IRepository<Sculpture> _sculptureRepository;

    public SculptureProvider(IRepository<Sculpture> sculptureRepository)
        : base(sculptureRepository)
    {
        _sculptureRepository = sculptureRepository;

    }

    public List<string> GetUniqueSculpturesMaterial()
    {
        var sculptures = _sculptureRepository.GetAll();
        var result = sculptures.Select(x => x.Material!).Distinct().ToList();

        return result;
    }

    public override string GetInfo(Sculpture entity)
    {
        var sculpture = entity! as Sculpture;
        StringBuilder sb = new(2048);

        string info = $"|P|  Product ID: {sculpture.Id}, Product Title: {sculpture.Title}\n" +
                      $"     Product Material: {sculpture.Material}\n" +
                      $"     Creation date: {sculpture.CreatedDate}\n" +
                      $"     Product Price: {sculpture.Price}\n";

        foreach (var line in info.Split('\n'))
        {
            sb.AppendLine(line + new string(' ', Console.WindowWidth - line.Length));
        }

        return sb.ToString();
    }

    public override string GetProviderType()
    {
        return "Sculpture";
    }

}
