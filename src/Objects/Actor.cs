namespace Spelunker;


public class Actor : GameObject
{

	public event System.Action? OnDeath;
	
	public  readonly ActorType ActorType;

	private int _health;

	public Inventory Inventory;

	public Faction Faction;

	public override bool Blocking => true;

	public Actor(ActorType actorType, Faction faction)
	{
		ActorType = actorType;
		_health = actorType.MaxHealth;
		Faction = faction;
		Inventory = new Inventory(actorType.InventorySize);
	}

	public override ColoredGlyph Glyph => ActorType.Glyph;

	public void TakeDamage(int amount)
	{
		_health = Math.Max(_health - amount, 0);
		if (_health == 0)
		{
			Death();
		}
	}

	public void HealDamage(int amount)
	{
		_health = Math.Min(_health + amount, ActorType.MaxHealth);
	}
	
	public bool ExecuteAction(Action action)
	{
		return action.Execute(this);
	}

	/// <summary>
	/// Moves the Actor in a direction, or attacks an Actor that is in the target tile.
	/// </summary>
	/// <param name="dx"></param>
	/// <param name="dy"></param>
	/// <returns></returns>
	public bool MoveInDirection(int dx, int dy)
	{
		if (ExecuteAction(new MoveAction(dx, dy)))
		{
			return true;
		}

		if (ExecuteAction(new AttackAction(dx, dy)))
		{
			return true;
		}

		return false;
	}

	private void Death()
	{
		OnDeath?.Invoke();
	}
}