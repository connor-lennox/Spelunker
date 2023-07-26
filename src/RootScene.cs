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
        _mainSurface = new ScreenSurface(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        // Add _mainSurface as a child object of this one. This object, RootScene, is a simple object
        // and doesn't display anything itself. Since _mainSurface is going to be a child of it, _mainSurface
        // will be displayed.
        Children.Add(_mainSurface);

        _world = new WorldGenerator(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, new RoomWorldGenerationStrategy(9, 5, 11)).BuildWorld();
        
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
