namespace Spelunker;

public class Actor : GameObject
{
	private ActorType _actorType;

	private int _health;
	
	public Actor(ActorType actorType)
	{
		_actorType = actorType;
	}

	public override ColoredGlyph Glyph => _actorType.Glyph;

	public void TakeDamage(int amount)
	{
		_health -= amount;
	}

	public void HealDamage(int amount)
	{
		_health += amount;
	}
	
	public bool ExecuteAction(Action action)
	{
		return action.Execute(this);
	}
}