using ArtworkApp.Components.CsvReader.Models;

namespace ArtworkApp.Components.CsvReader;

    public interface ICsvReader
    {
        List<Artist> ProcessArtists(string filePath);
        List<Artwork> ProcessArtworks(string filePath);

    }

