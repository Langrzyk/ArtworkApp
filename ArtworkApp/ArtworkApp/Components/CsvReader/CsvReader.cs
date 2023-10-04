using ArtworkApp.Components.CsvReader.Extensions;
using ArtworkApp.Components.CsvReader.Models;

namespace ArtworkApp.Components.CsvReader;

public class CsvReader : ICsvReader
{
    public List<Artist> ProcessArtists(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Artist>();
        }

        var items = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToArtist();

        return items.ToList();
    }
    public List<Artwork> ProcessArtworks(string filePath)
    {
        if (!File.Exists(filePath))
        {
            return new List<Artwork>();
        }

        var items = File.ReadAllLines(filePath)
            .Skip(1)
            .Where(x => x.Length > 1)
            .ToArtwork();

        return items.ToList();
    }
}