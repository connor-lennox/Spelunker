using SadConsole.Input;

namespace Spelunker;

public class DebugInputHandler : InputHandler
{
	private static readonly Dictionary<Keys, char> CharInputKeys = new()
	{
		{ Keys.A, 'A' },
		{ Keys.B, 'B' },
		{ Keys.C, 'C' },
		{ Keys.D, 'D' },
		{ Keys.E, 'E' },
		{ Keys.F, 'F' },
		{ Keys.G, 'G' },
		{ Keys.H, 'H' },
		{ Keys.I, 'I' },
		{ Keys.J, 'J' },
		{ Keys.K, 'K' },
		{ Keys.L, 'L' },
		{ Keys.M, 'M' },
		{ Keys.N, 'N' },
		{ Keys.O, 'O' },
		{ Keys.P, 'P' },
		{ Keys.Q, 'Q' },
		{ Keys.R, 'R' },
		{ Keys.S, 'S' },
		{ Keys.T, 'T' },
		{ Keys.U, 'U' },
		{ Keys.V, 'V' },
		{ Keys.W, 'W' },
		{ Keys.X, 'X' },
		{ Keys.Y, 'Y' },
		{ Keys.Z, 'Z' },
		{ Keys.Space, ' ' },
		{ Keys.D1, '1' },
		{ Keys.D2, '2' },
		{ Keys.D3, '3' },
		{ Keys.D4, '4' },
		{ Keys.D5, '5' },
		{ Keys.D6, '6' },
		{ Keys.D7, '7' },
		{ Keys.D8, '8' },
		{ Keys.D9, '9' },
		{ Keys.D0, '0' }
	};
	
	private readonly DebugConsole _debugConsole;

	public DebugInputHandler(DebugConsole debugConsole)
	{
		_debugConsole = debugConsole;
	}

	public override bool HandleInput(Engine engine, Keyboard keyboard)
	{
		// Type character
		foreach (var entry in CharInputKeys.Where(entry => keyboard.IsKeyPressed(entry.Key)))
		{
			_debugConsole.AddCharacter(entry.Value);
			return true;
		}

		// Backspace
		if (keyboard.IsKeyPressed(Keys.Back))
		{
			_debugConsole.RemoveCharacter();
			return true;
		}
		
		// Execute
		if (keyboard.IsKeyPressed(Keys.Enter))
		{
			_debugConsole.ExecuteCommand();
			engine.CloseDebugConsole();
			return true;
		}
		
		// Cancel
		if (keyboard.IsKeyPressed(Keys.Escape))
		{
			_debugConsole.Hide();
			engine.CloseDebugConsole();
		}
		
		return false;
	}
}