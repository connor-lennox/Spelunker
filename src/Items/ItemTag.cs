namespace Spelunker;

public abstract class ItemTag
{
	public virtual void OnAttack(Actor instigator, Item item, Actor target) {}
	public virtual void OnHitWhileHolding(Actor holder, Item item, Actor instigator) {}
	public virtual void OnUse(Actor holder, Item item) {}
	public virtual void OnPickup(Actor holder, Item item) {}
}