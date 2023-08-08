using Spelunker;

Settings.WindowTitle = "Spelunker";

var gameStartup = new Game.Configuration()
    .SetScreenSize(GameSettings.GameWidth, GameSettings.GameHeight)
    .SetStartingScreen<Spelunker.Scenes.RootScene>();

var itemLoader = new ItemLoader();
var items = itemLoader.Load("data/items.dat");
ItemType.RegisterItems(items);

var actorLoader = new ActorLoader();
var actors = actorLoader.Load("data/actors.dat");
ActorType.RegisterActors(actors);

KnowledgeCatalog.LoadKnowledge();

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();
