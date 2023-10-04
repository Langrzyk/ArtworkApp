using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArtworkApp.Components.CsvReader.Models;

public class Artist
{
    public string? Name { get; set; }
    public string? ArtistBio { get; set; }
    public string? Nationality { get; set; }
    public string? Gender { get; set; }
    public int? BeginDate { get; set; }
    public int?  EndDate { get; set; }
}