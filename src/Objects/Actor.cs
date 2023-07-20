namespace Spelunker;

public class Actor : GameObject
{
	public  readonly ActorType ActorType;

	private int _health;

	public Inventory Inventory;

	public override bool Blocking => true;

	public Actor(ActorType actorType)
	{
		ActorType = actorType;
		_health = actorType.MaxHealth;
		Inventory = new Inventory(actorType.InventorySize);
	}

	public override ColoredGlyph Glyph => ActorType.Glyph;

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
}