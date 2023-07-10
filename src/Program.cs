Settings.WindowTitle = "Spelunker";

var gameStartup = new Game.Configuration()
    .SetScreenSize(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT)
    .SetStartingScreen<Spelunker.Scenes.RootScene>();

Game.Create(gameStartup);
Game.Instance.Run();
Game.Instance.Dispose();
