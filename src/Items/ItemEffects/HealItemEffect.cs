namespace Spelunker;

public class HealItemEffect : ItemEffect
{
	private int _amount;

	public HealItemEffect(int amount)
	{
		_amount = amount;
	}

	public override bool Execute(Actor instigator, Point target)
	{
		if (instigator.World?.ObjectAtPoint(target) is not Actor targetActor) return false;
		
		targetActor.HealDamage(_amount);
		return true;
	}
}