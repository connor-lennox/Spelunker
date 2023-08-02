using SadConsole.Input;

namespace Spelunker;

public class DefaultInputHandler : InputHandler
{
	private static readonly Dictionary<Keys, int> ItemSwapKeys = new Dictionary<Keys, int>()
	{
		{ Keys.D1, 0 },
		{ Keys.D2, 1 },
		{ Keys.D3, 2 },
		{ Keys.D4, 3 },
		{ Keys.D5, 4 },
		{ Keys.D6, 5 },
		{ Keys.D7, 6 },
		{ Keys.D8, 7 },
		{ Keys.D9, 8 },
		{ Keys.D0, 9 }
	};

	public override bool HandleInput(Engine engine, Keyboard keyboard)
	{
		// Basic movement
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

		// Pickup item
		if (keyboard.IsKeyPressed(Keys.P))
		{
			engine.DoPlayerTurn(new PickupItemAction(engine.Player.Position));
			return true;
		}
		
		// Swap held item
		foreach (var entry in ItemSwapKeys.Where(entry => keyboard.IsKeyPressed(entry.Key)))
		{
			engine.DoPlayerTurn(new SwapHeldItemAction(entry.Value));
			return true;
		}
		
		// Open history
		if (keyboard.IsKeyPressed(Keys.V))
		{
			engine.OpenHistory();
			return true;
		}

		return false;
	}
}