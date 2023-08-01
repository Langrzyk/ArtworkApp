using ArtworkApp.Entities;

namespace ArtworkApp.DataProviders.Extensions;

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
