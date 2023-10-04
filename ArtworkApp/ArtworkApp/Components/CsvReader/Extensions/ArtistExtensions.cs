using System.Globalization;
using ArtworkApp.Components.CsvReader.Models;
using Microsoft.Data.SqlClient.DataClassification;

namespace ArtworkApp.Components.CsvReader.Extensions;

public static class ArtistExtensions
{
    public static IEnumerable<Artist> ToArtist(this IEnumerable<string> source)
    {
        foreach (var line in source)
        {
            var columns = line.Split(';');

            yield return new Artist
            {
                Name = columns[0],
                ArtistBio = columns[1],
                Nationality = columns[2],
                Gender = columns[3],
                BeginDate = int.Parse(columns[4]),
                EndDate = int.Parse(columns[4])
            };
        }

    }
}