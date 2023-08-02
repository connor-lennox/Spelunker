namespace Spelunker;

public class PickupItemAction : PositionalAction
{
	public PickupItemAction(Point targetPoint) : base(targetPoint)
	{
	}

	public override bool Execute(Actor instigator)
	{
		var droppedItem = instigator.World?.ItemAtPoint(_targetPoint);
		if (droppedItem == null) return false;
		
		if (instigator.Inventory.AddItem(new Item(droppedItem.ItemType)))
		{
			Logger.Log($"You pick up the {droppedItem.ItemType.Name}.");
			instigator.World.RemoveItem(droppedItem);
			return true;
		}

		Logger.Log($"Your inventory is full!");
		return false;
	}
}