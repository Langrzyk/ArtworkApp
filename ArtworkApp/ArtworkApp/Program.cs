using ArtworkApp.Data;
using ArtworkApp.Entities;
using ArtworkApp.Repositories;
using System.Globalization;

IRepository<Painting> paintingRepository;
IRepository<Sculpture> sculptureRepository;

Console.WriteLine("Artwork - console application for storing data on works of art.\n");

Console.WriteLine("Do you want to work with MS SQL Server (1) or with data stored in .txt files (2)?");

switch (GetValueFromUser().ToUpper())
{
    case "1":
        Console.WriteLine("--- WORK WITH MS SQL SERVER ---");
        paintingRepository = GetRepository<Painting>(repositoryType.MS_SQL_SERVER);
        sculptureRepository = GetRepository<Sculpture>(repositoryType.MS_SQL_SERVER);
        break;

    case "2":
        Console.WriteLine("--- WORK WITH .TXT FILES ---");
        paintingRepository = GetRepository<Painting>(repositoryType.TEXT_FILES);
        sculptureRepository = GetRepository<Sculpture>(repositoryType.TEXT_FILES);
        break;

    default:
        Console.WriteLine($"Invalid operation. Close App.\n", Console.ForegroundColor = ConsoleColor.Red);
        Console.ResetColor();
        return;
}

Console.Write("Do you want to view the all currently stored data? Y/N:");
if (GetValueFromUser().ToUpper() == "Y")
{
    WriteAllToConsole(paintingRepository);
    WriteAllToConsole(sculptureRepository);
}

while (true)
{
    Console.WriteLine("\nWhat you want to do?\n" +
                        "1 - Add Artwork\n" +
                        "2 - Remove Artwork\n" +
                        "3 - Show Artwork by ID\n" +
                        "4 - View stored data\n" +
                        "Q - Close app");

    string? actionChoese = GetValueFromUser();
    if (actionChoese.ToUpper() == "Q")
    {
        return;
    }

    Console.WriteLine("Which type of artwork?");
    entitiesType artworkType = GetArtworkType();

    switch (actionChoese.ToUpper())
    {
        case "1":
            Console.WriteLine("-- Add Artwork --");
            try
            {
                switch (artworkType)
                {
                    case entitiesType.PAINTINGS:
                        Painting painting = GetDataForAddPainting();
                        paintingRepository.Add(painting);
                        paintingRepository.Save();
                        break;
                    case entitiesType.SCULPTURES:
                        Sculpture sculpture = GetDataForAddSculpture();
                        sculptureRepository.Add(sculpture);
                        sculptureRepository.Save();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                Console.WriteLine();
            }
            break;

        case "2":
            Console.WriteLine("-- Remove Artwork --");
            Console.Write($"Select ID of {artworkType} to remove:");
            try
            {
                int idToRemove = Int32.Parse(GetValueFromUser());
                switch (artworkType)
                {
                    case entitiesType.PAINTINGS:
                        Painting paintingToRemove = GetItemByID(paintingRepository, idToRemove);
                        paintingRepository.Remove(paintingToRemove);
                        paintingRepository.Save();
                        break;
                    case entitiesType.SCULPTURES:
                        Sculpture sculpture = GetItemByID(sculptureRepository, idToRemove);
                        sculptureRepository.Add(sculpture);
                        sculptureRepository.Save();
                        break;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                Console.WriteLine();
            }
            break;

        case "3":
            Console.WriteLine("-- Show Artwork by ID --");
            Console.Write($"Select ID of {artworkType} to show:");
            try
            {
                int idToRemove = Int32.Parse(GetValueFromUser());
                switch (artworkType)
                {
                    case entitiesType.PAINTINGS:
                        Painting paintingToRemove = GetItemByID(paintingRepository, idToRemove);
                        Console.WriteLine(paintingToRemove);
                        break;
                    case entitiesType.SCULPTURES:
                        Sculpture sculpture = GetItemByID(sculptureRepository, idToRemove);
                        Console.WriteLine(sculpture);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{e.Message}", Console.ForegroundColor = ConsoleColor.Red);
                Console.ResetColor();
                Console.WriteLine();
            }
            break;

        case "4":
            Console.WriteLine("-- View stored data --");
            switch (artworkType)
            {
                case entitiesType.PAINTINGS:
                    WriteAllToConsole(paintingRepository);
                    break;
                case entitiesType.SCULPTURES:
                    WriteAllToConsole(sculptureRepository);
                    break;
            }
            break;
        case "Q":
            Console.WriteLine("--- CLOSE APP ---");
            return;

        default:
            Console.WriteLine($"Invalid operation.\n", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
            continue;
    }
}

static string GetValueFromUser()
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    string result = Console.ReadLine() ?? "";
    Console.ResetColor();

    return result;
}

static T GetItemByID<T>(IRepository<T> repository,  int id) where T : class, IEntity, new()
{
    try 
    {
        T item = repository.GetById(id);
        return item;
    }
    catch(Exception e)
    {
        throw new Exception("Invalid Id number.");
    }
}

static IRepository<T> GetRepository<T>(repositoryType repoType) where T : class, IEntity
{
    switch (repoType)
    {
        case repositoryType.MS_SQL_SERVER:
            return new SqlRepository<T>(new ArtworkAppDbContext());

        case repositoryType.TEXT_FILES:
            return new RepositoryInFiles<T>();

        default:
            return null;
    }
}

static Painting GetDataForAddPainting()
{
    Painting painting = GetBasicDataForAdd<Painting>();
    Console.Write("Type: ");
    painting.Type = GetValueFromUser();

    Console.Write("Technics: ");
    painting.Technics = GetValueFromUser();

    return painting;
}

static Sculpture GetDataForAddSculpture()
{
    Sculpture sculpture = GetBasicDataForAdd<Sculpture>();
    Console.Write("Material: ");
    sculpture.Material = GetValueFromUser();

    return sculpture;
}

static T GetBasicDataForAdd<T>() where T : EntityBase, new()
{
    Console.Write("Title: ");
    string title = GetValueFromUser();
    Console.Write("CreationDate (dd-MM-yyyy): ");
    string datetimeStr = GetValueFromUser();

    if (DateTime.TryParseExact(datetimeStr,"dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
    {
        return new T
        { 
            Title = title, 
            CreatedDate = dt 
        };
    }
    else
    {
        throw new Exception($"Invalid DateTime");
    }
}

static entitiesType GetArtworkType()
{
    Console.WriteLine("P - Painting\n" +
                      "S - Sculpture");

    switch (GetValueFromUser().ToUpper())
    {
        case "P":
            return entitiesType.PAINTINGS;
        case "S":
            return entitiesType.SCULPTURES;
        default:
            Console.WriteLine("Choose one of the options:");
            return GetArtworkType();
    }

}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}

enum repositoryType
{
    MS_SQL_SERVER,
    TEXT_FILES
}

enum entitiesType
{
    PAINTINGS,
    SCULPTURES
}