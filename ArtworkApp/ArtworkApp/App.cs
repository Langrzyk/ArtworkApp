using ArtworkApp.Components.CsvReader;
using ArtworkApp.Services.EventHandlerService;
using ArtworkApp.Services.XmlService;
using ArtworkApp.UserCommunication;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace ArtworkApp;

public class App : IApp
{
    private readonly IUserCommunication _userCommunication;
    private readonly IEventHandlerService _eventHandlerService;

    public App(
        IUserCommunication userCommunication,
        IEventHandlerService eventHandlerService)
    { 
        _userCommunication = userCommunication;
        _eventHandlerService = eventHandlerService;
    } 



    public void Run()
    {
        _eventHandlerService.ListenForEvents();
        _userCommunication.HelloDisplay();
        _userCommunication.ChooseOptions();

    }
}
