using SadConsole.Input;

namespace Spelunker;

public class GameOverInputHandler : InputHandler
{
	public override bool HandleInput(Engine engine, Keyboard keyboard)
	{
		// Open history
		if (keyboard.IsKeyPressed(Keys.V))
		{
			engine.OpenHistory();
			return true;
		}
		
		return false;
	}
}