using ArtworkApp.Data.Entities;

namespace ArtworkApp.Components.DataProviders.Extensions;

public static class PaintingsHelper
{
    public static IEnumerable<Painting> ByType(this IEnumerable<Painting> query, string type)
    {
        return query.Where(x => x.Type == type);
    }

    public static IEnumerable<Painting> ByTechnics(this IEnumerable<Painting> query, string technics)
    {
        return query.Where(x => x.Technics == technics);
    }
}
