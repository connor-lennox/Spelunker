namespace Spelunker;

public class ExplodeItemEffect : ItemEffect
{
	private readonly int _damage;
	private readonly int _radiusSq;

	public ExplodeItemEffect(int damage, int radius)
	{
		_radiusSq = radius * radius;
		_damage = damage;
	}

	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World == null) return false;
		
		foreach (var obj in instigator.World.ActorsInRange(target, _radiusSq))
		{
			(obj as Actor)?.TakeDamage(_damage);
		}

		return true;
	}
}