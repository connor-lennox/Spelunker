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

		var item = new Item(droppedItem.ItemType);
		if (instigator.Inventory.AddItem(item))
		{
			Logger.Log($"You pick up the {droppedItem.ItemType.Name}.");
			instigator.World!.RemoveItem(droppedItem);
			item.OnPickup(instigator);
			return true;
		}

		Logger.Log($"Your inventory is full!");
		return false;
	}
}