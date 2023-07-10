using SadConsole.Input;

namespace Spelunker.Scenes;

class RootScene : ScreenObject
{
    private ScreenSurface _mainSurface;

    private World _world;
    
    public RootScene()
    {
        // Create a surface that's the same size as the screen.
        _mainSurface = new ScreenSurface(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT);

        // Add _mainSurface as a child object of this one. This object, RootScene, is a simple object
        // and doesn't display anything itself. Since _mainSurface is going to be a child of it, _mainSurface
        // will be displayed.
        Children.Add(_mainSurface);

        _world = new WorldGenerator(GameSettings.GAME_WIDTH, GameSettings.GAME_HEIGHT, new RoomWorldGenerationStrategy(9, 5, 11)).BuildWorld();
    }

    public override void Render(TimeSpan delta)
    {
        _mainSurface.Clear();
        _world.Render(_mainSurface);
        base.Render(delta);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        var handled = false;
        
        if (keyboard.IsKeyPressed(Keys.Up))
        {
            _world.MovePlayer(0, -1);
            handled = true;
        }
        else if (keyboard.IsKeyPressed(Keys.Down))
        {
            _world.MovePlayer(0, 1);
            handled = true;
        }
        else if (keyboard.IsKeyPressed(Keys.Left))
        {
            _world.MovePlayer(-1, 0);
            handled = true;
        }
        else if (keyboard.IsKeyPressed(Keys.Right))
        {
            _world.MovePlayer(1, 0);
            handled = true;
        }
        return handled;
    }
}
