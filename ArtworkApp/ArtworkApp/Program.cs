using ArtworkApp.Data;
using ArtworkApp.Entities;
using ArtworkApp.Repositories;

var paintingsRepository = new SqlRepository<Painting>(new ArtworkAppDbContext());
AddPaintings(paintingsRepository);
AddDrawings(paintingsRepository);
WriteAllToConsole(paintingsRepository);

var sculpturesRepository = new SqlRepository<Sculpture>(new ArtworkAppDbContext());
AddSculpture(sculpturesRepository);
WriteAllToConsole(sculpturesRepository);


static void AddPaintings(IRepository<Painting> paintingsRepository)
{
    paintingsRepository.Add(new Painting
    {
        Title = "Castle of Glass",
        CreatedDate = new DateTime(2019, 4, 2),
        Type = "landscape",
        Technics = "acrylic"
    });

    paintingsRepository.Add(new Painting
    {
        Title = "Eleonora",
        CreatedDate = new DateTime(2019, 6, 12),
        Type = "Portrait",
        Technics = "oil"
    });

    paintingsRepository.Save();
}

static void AddDrawings(IWriteRepository<Drawing> paintingsRepository)
{
    paintingsRepository.Add(new Drawing
    {
        Title = "Cat",
        CreatedDate = new DateTime(2019, 4, 2),
        Type = "landscape"
    });

    paintingsRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

static void AddSculpture(IRepository<Sculpture> sculpturesRepository)
{
    sculpturesRepository.Add(new Sculpture
    {
        Title = "Tower",
        CreatedDate = new DateTime(2019, 12, 22),
        Material = "ceramic"
    });

    sculpturesRepository.Save();
}