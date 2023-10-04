namespace ArtworkApp.Data.Entities;

public class Sculpture : EntityBase
{
    public string? Material { get; set; }

    public override string ToString() => $"|Sculpture| Id: {Id}, Title: {Title}, CreationDate: {CreatedDate}, " +
        $"Material: {Material}, Price: {Price}";
}
