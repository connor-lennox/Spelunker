namespace Spelunker;

public class HealItemEffect : ItemEffect
{
	private int _amount;

	public HealItemEffect(int amount)
	{
		_amount = amount;
	}

	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World?.ActorAtPoint(target) is not { Alive: true } targetActor) return false;
		
		Logger.Log($"The {targetActor.ActorType.Name} is healed for {_amount} health.");
		targetActor.HealDamage(_amount);
		return true;
	}
}