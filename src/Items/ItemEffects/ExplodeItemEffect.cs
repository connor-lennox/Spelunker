namespace Spelunker;

public class ExplodeItemEffect : ItemEffect
{
	private readonly int _damage;
	private readonly int _radius;

	public ExplodeItemEffect(int damage, int radius)
	{
		_radius = radius;
		_damage = damage;
	}

	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World == null) return false;
		
		Logger.Log($"An explosion erupts from the {item.Name}!");
		foreach (var actor in instigator.World.ActorsInRange(target, _radius * _radius))
		{
			Logger.Log($"{actor.ActorType.Name} is caught in the explosion for {_damage} damage!");
			actor.TakeDamage(_damage, instigator);
		}

		return true;
	}

	public override string ToString()
	{
		return $"EXPLODE {_damage} {_radius}";
	}
}