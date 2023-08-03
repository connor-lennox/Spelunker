namespace Spelunker;

public class DamageItemEffect : ItemEffect
{
	private readonly int _damage;

	public DamageItemEffect(int damage)
	{
		_damage = damage;
	}

	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World?.ActorAtPoint(target) is not { } targetActor) return false;
		
		targetActor.TakeDamage(_damage, instigator);
		return true;
	}
}