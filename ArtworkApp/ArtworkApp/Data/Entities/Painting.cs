﻿namespace ArtworkApp.Data.Entities;

public class Painting : EntityBase
{
    public string? Type { get; set; }

    public string? Technics { get; set; }

    public override string ToString() => $"|Paintings| Id: {Id}, Title: {Title}, CreationDate: {CreatedDate}, " +
        $"Type: {Type}, Technics: {Technics}, Price: {Price}";
}
