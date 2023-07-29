using SadConsole.Input;

namespace Spelunker.Scenes;

class RootScene : ScreenObject
{
    private World _world;

    private Engine _engine;

    private WorldSurface _worldSurface;

    private HoverInfoConsole _hoverInfoConsole;
    
    public RootScene()
    {
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

        _worldSurface = new WorldSurface(GameSettings.ScreenWorldWidth, GameSettings.ScreenWorldHeight, _world);
        _worldSurface.OnHoverInfoUpdate += DrawHoverInfo;
        Children.Add(_worldSurface);

        _hoverInfoConsole = new HoverInfoConsole(GameSettings.InfoBoxWidth, 1);
        _hoverInfoConsole.BasePosition = new Point(GameSettings.ScreenWorldWidth - GameSettings.InfoBoxWidth - 1,
            GameSettings.ScreenWorldHeight - 1);
        _hoverInfoConsole.Hide();
        Children.Add(_hoverInfoConsole);
    }

    public override void Render(TimeSpan delta)
    {
        _worldSurface.DrawWorld();
        base.Render(delta);
    }

    public override bool ProcessKeyboard(Keyboard keyboard)
    {
        return _engine.ReceiveInput(keyboard);
    }

    public void DrawHoverInfo(Point position, List<string>? contents)
    {
        if (contents != null)
        {
            _hoverInfoConsole.DisplayAt(position, contents);
        }
        else
        {
            _hoverInfoConsole.Hide();
        }
    }
}
