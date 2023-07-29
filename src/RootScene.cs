using SadConsole.Input;

namespace Spelunker.Scenes;

class RootScene : ScreenObject
{
    private ScreenSurface _mainSurface;

    private World _world;

    private Engine _engine;
    
    public RootScene()
    {
        // Create a surface that's the same size as the screen.
        _mainSurface = new ScreenSurface(GameSettings.ScreenWorldWidth, GameSettings.ScreenWorldHeight);

        // Add _mainSurface as a child object of this one. This object, RootScene, is a simple object
        // and doesn't display anything itself. Since _mainSurface is going to be a child of it, _mainSurface
        // will be displayed.
        Children.Add(_mainSurface);

        var statusConsole = new StatusConsole(GameSettings.GameWidth - GameSettings.ScreenWorldWidth - 2,
            GameSettings.ScreenWorldHeight - 1);
        statusConsole.Position = new Point(GameSettings.ScreenWorldWidth + 1, 1);
        Children.Add(statusConsole);
        
        var logConsole =
            new LogConsole(GameSettings.GameWidth - 2, GameSettings.GameHeight - GameSettings.ScreenWorldHeight - 2);
        logConsole.Position = new Point(1, GameSettings.ScreenWorldHeight + 1);
        Children.Add(logConsole);
        
        _world = new WorldGenerator(GameSettings.ScreenWorldWidth, GameSettings.ScreenWorldHeight, new RoomWorldGenerationStrategy(9, 5, 11)).BuildWorld();
        
        _engine = new Engine();
        _engine.LoadWorld(_world);
    }

    public override void Render(TimeSpan delta)
    {
        _mainSurface.Clear();
        _world.Render(_mainSurface);
        base.Render(delta);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        return _engine.ReceiveInput(keyboard);
    }
}
