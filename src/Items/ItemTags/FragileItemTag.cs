namespace Spelunker;

public class FragileItemTag : ItemTag
{
	public override void OnAttack(Actor instigator, Item item, Actor target)
	{
		Logger.Log($"The {item.Name} broke!");
		instigator.Inventory.BreakHeldItem();
	}

	public override void OnHitWhileHolding(Actor holder, Item item, Actor instigator)
	{
		Logger.Log($"The {item.Name} broke!");
		instigator.Inventory.BreakHeldItem();
	}
}