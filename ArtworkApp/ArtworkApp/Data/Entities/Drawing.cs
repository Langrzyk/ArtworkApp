namespace ArtworkApp.Data.Entities;

public class Drawing : Painting
{
    public Drawing() { Technics = "pencil"; }

    public override string ToString() => $"|Drawings| Id: {Id}, Title: {Title}, CreationDate: {CreatedDate}," +
        $"Type: {Type}, Technics: {Technics}";
}
