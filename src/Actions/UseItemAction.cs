namespace Spelunker;

public class UseItemAction : PositionalAction
{
	private readonly Item _item;

	public UseItemAction(Point targetPoint, Item item) : base(targetPoint)
	{
		_item = item;
	}

	public override bool Execute(Actor instigator)
	{
		_item.Use(instigator, _targetPoint);
		return true;
	}
}