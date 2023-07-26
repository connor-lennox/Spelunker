namespace Spelunker;


public class Actor : GameObject
{

	public event System.Action? OnDeath;
	
	public  readonly ActorType ActorType;

	private int _health;

	public bool Alive => _health > 0;

	public Inventory Inventory;

	public Faction Faction;

	public BaseAgent Agent;

	public override bool Blocking => true;

	public Actor(ActorType actorType, Faction faction, BaseAgent agent)
	{
		ActorType = actorType;
		_health = actorType.MaxHealth;
		Faction = faction;
		Inventory = new Inventory(actorType.InventorySize);

		if (agent != null)
		{
			Agent = agent;
			Agent.Actor = this;
		}
	}

	public override ColoredGlyph Glyph => ActorType.Glyph;

	public void TakeDamage(int amount)
	{
		if (!Alive) return;
		
		_health = Math.Max(_health - amount, 0);
		if (_health == 0)
		{
			Death();
		}
	}

	public void HealDamage(int amount)
	{
		if (!Alive) return;
		
		_health = Math.Min(_health + amount, ActorType.MaxHealth);
	}
	
	public bool ExecuteAction(Action action)
	{
		if (!Alive) return false;
		
		return action.Execute(this);
	}

	public Action GetAgentAction() => Agent.Decide();

	private void Death()
	{
		OnDeath?.Invoke();
	}
}