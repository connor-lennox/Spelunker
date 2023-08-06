namespace Spelunker;

public class SpikyItemTag : ItemTag
{
	private int _spikeDamage;

	public SpikyItemTag(int spikeDamage)
	{
		_spikeDamage = spikeDamage;
	}

	public override void OnHitWhileHolding(Actor holder, Item item, Actor instigator)
	{
		Logger.Log($"The {instigator.ActorType.Name} takes {_spikeDamage} damage from spikes!");
		
		// `null` instigator stops infinite spike damage chains
		instigator.TakeDamage(_spikeDamage, null);
	}

	public override string ToString()
	{
		return $"SPIKY {_spikeDamage}";
	}
}