namespace Spelunker;

public class FragileItemTag : ItemTag
{
	public override void OnAttack(Actor instigator, Item item, Actor target)
	{
		instigator.Inventory.BreakHeldItem();
	}

	public override void OnHitWhileHolding(Actor holder, Item item, Actor instigator)
	{
		instigator.Inventory.BreakHeldItem();
	}
}