namespace Spelunker;

public class ConsumableItemTag : ItemTag
{
	public override void OnUse(Actor holder, Item item)
	{
		Logger.Log($"The {item.Name} is consumed.");
		holder.Inventory.BreakHeldItem();
	}
}