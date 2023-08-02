using ArtworkApp.DataProviders;
using ArtworkApp.Entities;
using ArtworkApp.Repositories;
using ArtworkApp.Services;
using ArtworkApp.UserCommunication;

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
