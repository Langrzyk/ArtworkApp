using ArtworkApp.Components.CsvReader;
using ArtworkApp.Components.CsvReader.Models;
using ArtworkApp.Data.Entities;
using ArtworkApp.Data.Repositories;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace ArtworkApp.Services.XmlService;

public class XmlService : IXmlService
{
    private readonly ICsvReader _csvReader;
    private List<Artist> _artistsRecords ;
    private List<Artwork> _artworksRecords;

    public XmlService(ICsvReader csvReader)
    {
        _csvReader = csvReader;
        _artistsRecords = _csvReader.ProcessArtists("Resources\\Files\\ArtistsApp.csv");
        _artworksRecords = _csvReader.ProcessArtworks("Resources\\Files\\ArtworksApp.csv");
    }


    public void CreateXml()
    { 
        var artworksByArtistAndType = _artworksRecords.GroupBy(x => new { x.Classification , x.Artist})
            .Select(y => new
            {
                Artist = y.Key.Artist,
                Type = y.Key.Classification,                
                Artworks = y.ToList(),
                Count = y.Count()
            })
            .OrderBy(x=> x.Artist);

        var artistsWithArtworksJoined = _artistsRecords.GroupJoin(
                artworksByArtistAndType,
                artist => artist.Name,
                artwork => artwork.Artist,
                (a, w) =>
                new
                {
                    Artist = a,
                    Artwork = w
                })
                .OrderBy(x => x.Artist.Name);

        var artistsWithArtworksGroupedByNationality = artistsWithArtworksJoined.GroupBy(x => x.Artist.Nationality)
            .Select(y => new
            {
                Nationality = y.Key,
                Artists = y.ToList(),
                Count = y.Count()
            })
            .OrderBy(x => x.Nationality);

        var document = new XDocument();
        var artworks = new XElement("ExhibitionArtist", artistsWithArtworksGroupedByNationality
            .Select(x =>
            new XElement("Artists",
            new XAttribute("Count", x.Count),
            new XAttribute("Nationality", x.Nationality),
            x.Artists
                .Select(y =>
                new XElement("Artist",
                new XAttribute("Gender", y.Artist.Gender),
                new XAttribute("Name", y.Artist.Name),
                y.Artwork
                    .Select(z =>
                    new XElement("Artworks",
                    new XAttribute("Count", z.Count),
                    new XAttribute("Type", z.Type),
                    z.Artworks
                        .Select(q =>
                        new XElement("Artwork",
                        new XAttribute("Date", q.Date),
                        new XAttribute("Medium", q.Medium),
                        new XAttribute("Title", q.Title)
                        ))
                    ))
                ))
            ))
        );

        document.Add(artworks);
        document.Save("artworks.xml");
    }

    public void QueryXml()
    {
        var document = XDocument.Load("artworks.xml");
        var femaleArtists = document
            .Element("ExhibitionArtist")?
            .Elements("Artists")
            .Elements("Artist")
            .Where(x => x.Attribute("Gender")?.Value == "Female")
            .Select(x => x.Attribute("Name")?.Value);


        foreach (var name in femaleArtists)
        {
            Console.WriteLine(name);
        }

    }
}
