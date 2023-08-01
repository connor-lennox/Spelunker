using SadConsole.Input;

namespace Spelunker;

public class HistoryInputHandler : InputHandler
{
	private HistoryConsole _console;

	public HistoryInputHandler(HistoryConsole console)
	{
		_console = console;
	}

	public override bool HandleInput(Engine engine, Keyboard keyboard)
	{
		if (keyboard.IsKeyPressed(Keys.Up))
		{
			_console.ShiftOffset(1);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Down))
		{
			_console.ShiftOffset(-1);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.PageUp))
		{
			_console.ShiftOffset(10);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.PageDown))
		{
			_console.ShiftOffset(-10);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Escape) || keyboard.IsKeyPressed(Keys.V))
		{
			_console.IsVisible = false;
			engine.PopInputHandler();
			return true;
		}

		return false;
	}
}