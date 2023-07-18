using Spelunker;

Settings.WindowTitle = "Spelunker";

var gameStartup = new Game.Configuration()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    .SetStartingScreen<Spelunker.Scenes.RootScene>();

var itemLoader = new ItemLoader();
var items = itemLoader.Load("data/items.dat");
ItemType.RegisterItems(items);

var actorLoader = new ActorLoader();
var actors = actorLoader.Load("data/actors.dat");
ActorType.RegisterActors(actors);

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();
