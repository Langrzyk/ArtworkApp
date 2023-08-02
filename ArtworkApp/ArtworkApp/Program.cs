using ArtworkApp;
using ArtworkApp.Data;
using ArtworkApp.DataProviders;
using ArtworkApp.Entities;
using ArtworkApp.Repositories;
using ArtworkApp.Services;
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

ServiceProvider serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();
