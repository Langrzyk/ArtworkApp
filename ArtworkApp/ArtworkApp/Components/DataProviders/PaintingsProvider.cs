using System.Text;
using ArtworkApp.Data.Entities;
using ArtworkApp.Data.Repositories;

namespace ArtworkApp.Components.DataProviders;

public class PaintingsProvider : DataProviderBase<Painting>, IPaintingsProvider
{
    private readonly IRepository<Painting> _paintingRepository;

    public PaintingsProvider(IRepository<Painting> paintingRepository) 
        : base(paintingRepository)
    {
        _paintingRepository = paintingRepository;
    }

    public List<string> GetUniquePaintingsTechnic()
    {
        var paintings = _paintingRepository.GetAll();
        var result = paintings.Select(x => x.Technics!).Distinct().ToList();

        return result;
    }

    public List<string> GetUniquePaintingsType()
    {
        var paintings = _paintingRepository.GetAll();
        var result = paintings.Select(x => x.Type!).Distinct().ToList();

        return result;
    }

    public override string GetInfo(Painting entity)
    {
        var painting = entity! as Painting;
        StringBuilder sb = new(2048);
        string info = $"|P|  Product ID: {painting.Id}, Product Title: {painting.Title}\n" +
                      $"     Product Type: {painting.Type}, Product Technics: {painting.Technics}\n" +
                      $"     Creation date: {painting.CreatedDate}\n" +
                      $"     Product Price: {painting.Price}\n";

        foreach(var line in info.Split('\n'))
        {
            sb.AppendLine(line + new string(' ', Console.WindowWidth - line.Length));
        }

        return sb.ToString();
    }

    public override string GetProviderType()
    {
        return "Painting";
    }

}
