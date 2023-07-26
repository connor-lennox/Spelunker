using SadConsole.Input;

namespace Spelunker;

public abstract class InputHandler
{
	public abstract bool HandleInput(Engine engine, Keyboard keyboard);
}