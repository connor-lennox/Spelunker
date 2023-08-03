namespace Spelunker;

public class ConsumableItemTag : ItemTag
{
	public override void OnUse(Actor holder, Item item)
	{
		holder.Inventory.BreakHeldItem();
	}
}