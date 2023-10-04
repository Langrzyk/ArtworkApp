using ArtworkApp;
using ArtworkApp.Components.CsvReader;
using ArtworkApp.Components.DataProviders;
using ArtworkApp.Data;
using ArtworkApp.Data.Entities;
using ArtworkApp.Data.Repositories;
using ArtworkApp.Services.EventHandlerService;
using ArtworkApp.Services.XmlService;
using ArtworkApp.UserCommunication;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new();
services.AddSingleton<IApp, App>();
services.AddSingleton<IRepository<Painting>, RepositoryInFiles<Painting>>();
services.AddSingleton<IRepository<Sculpture>, RepositoryInFiles<Sculpture>>();
services.AddSingleton<IPaintingsProvider, PaintingsProvider>();
services.AddSingleton<ISculptureProvider, SculptureProvider>();
services.AddSingleton<IUserCommunication, ConsoleCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();
services.AddSingleton<ICsvReader, CsvReader>();
services.AddSingleton<IXmlService, XmlService>();

ServiceProvider serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
