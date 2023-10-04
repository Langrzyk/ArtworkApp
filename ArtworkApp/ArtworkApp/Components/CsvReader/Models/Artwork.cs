namespace ArtworkApp.Components.CsvReader.Models;

public class Artwork
{
    public string? Title { get; set; }
    public string? Artist { get; set; }
    public string? Date { get; set; }
    public string? Medium { get; set; }
    public string? Classification { get; set; }
    public string? Department { get; set; }
    public DateTime? DateAcquired { get; set; }
}