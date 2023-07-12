using Spelunker;

Settings.WindowTitle = "Spelunker";

var gameStartup = new Game.Configuration()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    .SetStartingScreen<Spelunker.Scenes.RootScene>();

var loader = new ItemLoader();
var items = loader.Load("data/items.dat");

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();
