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
		if (instigator.World?.ActorAtPoint(target) is not { } targetActor) return false;
		
		targetActor.HealDamage(_amount);
		return true;
	}
}