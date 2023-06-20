namespace ArtworkApp.Entities;

public class EntityBase : IEntity
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedDate { get; set; }
}
