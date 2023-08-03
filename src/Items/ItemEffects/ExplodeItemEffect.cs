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
		
		Logger.Log($"An explosion emerges from the {item.Name}!");
		foreach (var actor in instigator.World.ActorsInRange(target, _radiusSq))
		{
			Logger.Log($"{actor.ActorType.Name} is caught in the explosion!");
			actor.TakeDamage(_damage);
		}

		return true;
	}
}