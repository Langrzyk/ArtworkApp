using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtworkApp.Data.Entities;

public class EntityBase : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Title { get; set; }

    public DateTime CreatedDate { get; set; }

    public decimal Price { get; set; }
}
