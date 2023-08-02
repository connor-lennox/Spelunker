namespace Spelunker;

public class SwapHeldItemAction : Action
{
	private readonly int _newItemIdx;

	public SwapHeldItemAction(int newItemIdx)
	{
		_newItemIdx = newItemIdx;
	}

	public override bool Execute(Actor instigator)
	{
		return instigator.Inventory.SwapHeldItem(_newItemIdx);
	}
}