using SadConsole.Input;

namespace Spelunker;

public class DefaultInputHandler : InputHandler
{
	public override bool HandleInput(Engine engine, Keyboard keyboard)
	{
		if (keyboard.IsKeyPressed(Keys.Up))
		{
			engine.DoPlayerTurn(new BumpAction(new Point(0, -1)));
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Down))
		{
			engine.DoPlayerTurn(new BumpAction(new Point(0, 1)));
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Left))
		{
			engine.DoPlayerTurn(new BumpAction(new Point(-1, 0)));
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Right))
		{
			engine.DoPlayerTurn(new BumpAction(new Point(1, 0)));
			return true;
		}

		if (keyboard.IsKeyPressed(Keys.V))
		{
			engine.OpenHistory();
			return true;
		}

		return false;
	}
}