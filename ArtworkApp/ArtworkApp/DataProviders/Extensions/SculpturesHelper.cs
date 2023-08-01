﻿using ArtworkApp.Entities;

namespace ArtworkApp.DataProviders.Extensions;

public static class SculpturesHelper
{
    public static IEnumerable<Sculpture> ByMaterial(this IEnumerable<Sculpture> query, string material)
    {
        return query.Where(x => x.Material == material);
    }
}
