using System.Text.Json;

namespace ArtworkApp.Data.Entities.Extensions;

public static class EntityExtensions
{
    public static T? Copy<T>(this T itemToCopy)
        where T : IEntity
    {
        string json = JsonSerializer.Serialize<T>(itemToCopy);
        return JsonSerializer.Deserialize<T>(json);
    }
}
