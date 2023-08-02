using ArtworkApp.DataProviders;
using ArtworkApp.Entities;
using ArtworkApp.Repositories;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Cryptography;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ArtworkApp.UserCommunication;

public class ConsoleCommunication : IUserCommunication
{
    private readonly IRepository<Painting> _paintingRepository;
    private readonly IRepository<Sculpture> _sculptureRepository;
    private readonly IPaintingsProvider _paintingsProvider;
    private readonly ISculptureProvider _sculpturesProvider;
    private const int RECORDS_PER_PAGE = 2;

    public ConsoleCommunication(
        IRepository<Painting> paintingRepository,
        IRepository<Sculpture> sculptureRepository,
        IPaintingsProvider paintingsProvider,
        ISculptureProvider sculpturesProvider)
    {
        _paintingRepository = paintingRepository;
        _sculptureRepository = sculptureRepository;
        _paintingsProvider = paintingsProvider;
        _sculpturesProvider = sculpturesProvider;
    }

    public void HelloDisplay()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n                               Hello in ArtworkApp                            \n" +
                            "             (console application for storing data on works of art)           \n");
        Console.ResetColor();

    }

    public void ChooseOptions()
    {
        bool programRun = true;
        while (programRun)
        {
            ShowMainMenu();
            string option = GetUserInfo("What you want to do?: ");

            try
            {
                switch (option.ToUpper())
                {
                    case "1":
                        Painting paintingToAdd = GetDataForAddPainting();
                        AddArtwork(_paintingRepository, paintingToAdd);
                        break;
                    case "2":
                        Sculpture sculptureToAdd = GetDataForAddSculpture();
                        AddArtwork(_sculptureRepository, sculptureToAdd);
                        break;
                    case "3":
                        ShowUniquePaintingsTypes();
                        break;
                    case "4":
                        ShowUniquePaintingsTechnics();
                        break;
                    case "5":
                        ShowUniqueSculptureMaterials();
                        break;
                    case "6":
                        ShowStatistic(_paintingsProvider);
                        break;
                    case "7":
                        ShowStatistic(_sculpturesProvider);
                        break;
                    case "8":
                        Painting paintingToRemove = GetDataForRemove(_paintingRepository);
                        RemoveArtwork(_paintingRepository, paintingToRemove);
                        break;
                    case "9":
                        Sculpture sculptureToRemove = GetDataForRemove(_sculptureRepository);
                        RemoveArtwork(_sculptureRepository, sculptureToRemove);
                        break;
                    case "10":
                        DataPageing(_paintingsProvider);
                        break;
                    case "11":
                        DataPageing(_sculpturesProvider);
                        break;
                    case "Q":
                        programRun = false;
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please choose correctly!");
                        Console.ResetColor();
                        break;
                }
            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
        
        
    }

    

    private static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("___________________________________________________________________________________");
        Console.WriteLine("                                    MAIN MENU:                                     ");
        Console.WriteLine("     1:  Add Painting");
        Console.WriteLine("     2:  Add Sculpture");
        Console.WriteLine("     3:  Show unique Paintings Type");
        Console.WriteLine("     4:  Show unique Paintings Technics");
        Console.WriteLine("     5:  Show unique Sculptures Material");
        Console.WriteLine("     6:  Show Paintings statistics");
        Console.WriteLine("     7:  Show Sculptures statistics");
        Console.WriteLine("     8:  Remove Painting");
        Console.WriteLine("     9:  Remove Sculpture");
        Console.WriteLine("     10: Show all Painting");
        Console.WriteLine("     11: Show all Sculpture");
        Console.WriteLine("     Q:  Quit application");
        Console.WriteLine("___________________________________________________________________________________");
        Console.ResetColor();
    }
    private string GetUserInfo(string info)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write(info);
        string result = Console.ReadLine() ?? "";
        Console.ResetColor();

        return result;

    }

    private void ShowUniqueSculptureMaterials()
    {
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        Console.WriteLine($"                             UNIQUE SCULPTURES MATERIALS                           ");
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        foreach (var item in _sculpturesProvider.GetUniqueSculpturesMaterial())
        {
            if (item is not null)
            {
                Console.WriteLine($"{item}");
            }
        }
        Console.WriteLine($"-----------------------------------------------------------------------------------");
    }

    private void ShowUniquePaintingsTechnics()
    {
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        Console.WriteLine($"                              UNIQUE PAINTINGS TECHNICS                            ");
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        foreach (var item in _paintingsProvider.GetUniquePaintingsTechnic())
        {
            if (item is not null)
            {
                Console.WriteLine($"{item}");
            }
        }
        Console.WriteLine($"-----------------------------------------------------------------------------------");
    }

    private void ShowUniquePaintingsTypes()
    {
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        Console.WriteLine($"                                UNIQUE PAINTINGS TYPES                             ");
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        foreach(var item in _paintingsProvider.GetUniquePaintingsType())
        {
            if (item is not null)
            {
                Console.WriteLine($"{item}");
            }
        }
        Console.WriteLine($"-----------------------------------------------------------------------------------");
    }

   

    private void ShowStatistic<T>(IDataProvider<T> entityProvider)
        where T :  class, IEntity
    {
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        Console.WriteLine($"                                 {entityProvider.GetProviderType().ToUpper()} STATISTICS                               ");
        Console.WriteLine($"-----------------------------------------------------------------------------------");
        Console.WriteLine($"Maximum Price: {entityProvider.GetMaximumPrice()}");
        Console.WriteLine($"Minimum Price: {entityProvider.GetMinimumPrice()}");
        Console.WriteLine($"Average Price: {entityProvider.GetAveragePrice()}");

        var itemC = entityProvider.TakeCheapiest(1).FirstOrDefault();
        if(itemC is not null)
        {
            Console.WriteLine($"\nThe cheapest {entityProvider.GetProviderType()}s:");
            Console.WriteLine(entityProvider.GetInfo(itemC));
        }

        var itemE = entityProvider.TakeMostExpensive(1).FirstOrDefault();
        if (itemE is not null)
        {
            Console.WriteLine($"\nThe most expensive {entityProvider.GetProviderType()}s:");
            Console.WriteLine(entityProvider.GetInfo(itemE));
        }

        Console.WriteLine($"-----------------------------------------------------------------------------------");
    }

    private Sculpture GetDataForAddSculpture()
    {
        Sculpture sculpture = GetBasicDataForAdd<Sculpture>();
        sculpture.Material = GetUserInfo("Material: ");

        return sculpture;
    }

    private Painting GetDataForAddPainting()
    {
        Painting painting = GetBasicDataForAdd<Painting>();
        painting.Type = GetUserInfo("Type: ");
        painting.Technics = GetUserInfo("Technics: ");

        return painting;
    }
    private T GetBasicDataForAdd<T>() where T : EntityBase, new()
    {
        string title = GetUserInfo("Title: ");
        string priceStrValue = GetUserInfo("Price ($): ");

        if (!decimal.TryParse(priceStrValue, out decimal price))
        {
            throw new Exception("InvalidValue, write ony numbers!");
        }

        string dateStrValue = GetUserInfo("Creation Date (dd-MM-yyyy): ");

        if (!DateTime.TryParseExact(dateStrValue, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt))
        {
            throw new Exception("InvalidValue DateTime, write date in correct format (dd - MM - yyyy)");
        }


        return new T
        { 
            Title = title, 
            CreatedDate = dt,
            Price = price
        };

    }

    private static void AddArtwork<T>(IRepository<T> entityRepository, T artwork) 
        where T : class, IEntity
    {
        entityRepository.Add(artwork);
        entityRepository.Save();
    }

    private static void RemoveArtwork<T>(IRepository<T> entityRepository, T artwork)
        where T : class, IEntity
    {
        entityRepository.Remove(artwork);
        entityRepository.Save();
    }

    private T GetDataForRemove<T>(IRepository<T> entityRepository)
        where T : class, IEntity
    {
        var value = GetUserInfo($"Enter ID to remove: ");
        Int32.TryParse(value, out int id);

        return entityRepository.GetById(id);

    }


    private void DataPageing<T>(IDataProvider<T> repository) where T : class, IEntity
    {
        int top = Console.CursorTop;
        int bottom = 0;
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine($"-----------------------------------------------------------------------------------");
        Console.WriteLine($"                                 {repository.GetProviderType().ToUpper()} REPOSITORY                               ");
        Console.WriteLine($"-----------------------------------------------------------------------------------");
       
        top = Console.CursorTop;
        int pageNumber = 0;
        int pageNumberMax = repository.GetNumberOfPages(RECORDS_PER_PAGE);
        bool doPageing = true;
        do
        {
            
            ShowDataPage(repository, pageNumber);
            FixFormatting(ref bottom, pageNumber);

            Console.WriteLine($"-----------------------------------------------------------------------------------");
            Console.Write($"                            <- [ PAGE: {pageNumber + 1}/{pageNumberMax} | END: Q ] ->                          ");
            var key = Console.ReadKey();
            Console.ResetColor();
            switch (key.Key)
            {
                case ConsoleKey.RightArrow:
                    if (pageNumber < pageNumberMax-1)
                    {
                        pageNumber++;
                    }
                    Console.SetCursorPosition(0, top);
                    break;
                case ConsoleKey.LeftArrow:
                    if (pageNumber > 0)
                    {
                        pageNumber--;
                    }
                    Console.SetCursorPosition(0, top);
                    break;
                case ConsoleKey.Q:
                    doPageing = false;
                    Console.SetCursorPosition((Console.CursorLeft > 0) ? Console.CursorLeft - 1 : 0, Console.CursorTop); ;
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine($"\n-----------------------------------------------------------------------------------");
                    Console.ResetColor();
                    break;
                default:
                    Console.SetCursorPosition((Console.CursorLeft > 0) ? Console.CursorLeft - 1 : 0, Console.CursorTop); ;
                    Console.Write(" ");
                    Console.SetCursorPosition(0, top);
                    break;
            }
        } while (doPageing);

        static void FixFormatting(ref int bottom, int pageNumber)
        {
            int bottomNew;
            if (pageNumber == 0)
            {
                bottom = Console.CursorTop;
            }
            bottomNew = Console.CursorTop;
            if (bottomNew < bottom)
            {
                for (var i = 0; i < (bottom - bottomNew); i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
            }
        }
    }

    private static void ShowDataPage<T>(IDataProvider<T> repository, int pagenumber) where T : class, IEntity
    {
        Console.WriteLine($"");

        var page = repository.GetPage(RECORDS_PER_PAGE, pagenumber);
        foreach( var item in page )
        {
            Console.WriteLine(repository.GetInfo(item));
        }
    }

}
