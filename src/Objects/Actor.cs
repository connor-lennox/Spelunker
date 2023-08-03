namespace Spelunker;


public class Actor : GameObject
{
	// Health Changed: (current health, max health)
	public event Action<int, int>? OnHealthChanged;
	public event System.Action? OnDeath;
	
	public  readonly ActorType ActorType;

	public int Health { get; private set; }

	public bool Alive => Health > 0;

	public Inventory Inventory;

	public Faction Faction;

	public BaseAgent Agent;

	public override bool Blocking => true;

	public Actor(ActorType actorType, Faction faction, BaseAgent agent)
	{
		ActorType = actorType;
		Health = actorType.MaxHealth;
		Faction = faction;
		Inventory = new Inventory(actorType.InventorySize);

		if (agent != null)
		{
			Agent = agent;
			Agent.Actor = this;
		}
	}

	public override ColoredGlyph Glyph => ActorType.Glyph;

	public void TakeDamage(int amount, Actor? instigator)
	{
		if (!Alive) return;
		
		Health = Math.Max(Health - amount, 0);
		SendHealthUpdateEvent();
		if (Health == 0)
		{
			Death();
		}
		
		if (instigator != null)
		{
			Inventory.HeldItem?.OnHitWhileHolding(this, instigator);
		}
	}

	public void HealDamage(int amount)
	{
		if (!Alive) return;
		
		Health = Math.Min(Health + amount, ActorType.MaxHealth);
		SendHealthUpdateEvent();
	}

	// Forces a heal, even on an enemy that is dead.
	public void ForceHeal(int amount)
	{
		Health = Math.Min(Health + amount, ActorType.MaxHealth);
		SendHealthUpdateEvent();
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

	private void SendHealthUpdateEvent()
	{
		OnHealthChanged?.Invoke(Health, ActorType.MaxHealth);
	}

	public override List<string> GetHoverInfo()
	{
		return new List<string>
		{
			ActorType.Name,
			$"HP: {Health}/{ActorType.MaxHealth}",
			$"Holding: {Inventory.HeldItem?.ItemType.Name ?? "Nothing"}"
		};
	}
}