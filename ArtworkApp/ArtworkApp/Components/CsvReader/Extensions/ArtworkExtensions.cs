using ArtworkApp.Components.CsvReader.Models;

namespace ArtworkApp.Components.CsvReader.Extensions;

public static class ArtworkExtensions
{
    public static IEnumerable<Artwork> ToArtwork(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(';');

            yield return new Artwork
            {
                Title = columns[0],
                Artist = columns[1],
                Date = columns[2],
                Medium = columns[3],
                Classification = columns[4],
                Department = columns[5],
                DateAcquired = DateTime.Parse(columns[6]),
            };
        }

    }
}