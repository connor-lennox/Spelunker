using SadConsole.Input;

namespace Spelunker;

public class UseItemInputHandler : InputHandler
{
	private readonly GridSelector _selector;
	private Item _item;

	public UseItemInputHandler(GridSelector selector, Item item)
	{
		_selector = selector;
		_item = item;
	}

	public override bool HandleInput(Engine engine, Keyboard keyboard)
	{
		// Basic movement
		if (keyboard.IsKeyPressed(Keys.Up))
		{
			_selector.MoveSelection(0, -1);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Down))
		{
			_selector.MoveSelection(0, 1);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Left))
		{
			_selector.MoveSelection(-1, 0);
			return true;
		}
		if (keyboard.IsKeyPressed(Keys.Right))
		{
			_selector.MoveSelection(1, 0);
			return true;
		}

		if (keyboard.IsKeyPressed(Keys.Enter))
		{
			engine.DoPlayerTurn(new UseItemAction(_selector.CurrentlySelected, _item));
			engine.FinishPlayerUseItem();
		}

		return false;
	}
}