namespace Spelunker;

/// <summary>
/// Drains HP from the wielder when they USE or ATTACK with the item.
/// </summary>
public class DrainingItemTag : ItemTag
{
	private readonly int _drainAmount;

	public DrainingItemTag(int drainAmount)
	{
		_drainAmount = drainAmount;
	}

	public override void OnAttack(Actor instigator, Item item, Actor target)
	{
		instigator.TakeDamage(_drainAmount, null);
	}

	public override void OnUse(Actor holder, Item item)
	{
		holder.TakeDamage(_drainAmount, null);
	}
	
	public override string ToString()
	{
		return $"DRAINING {_drainAmount}";
	}
}