using SadConsole.UI;

namespace Spelunker;

public class StatusConsole : Console
{
	public static StatusConsole? Instance;

	private int _currentHealth;
	private int _maxHealth;
	private Inventory _inventory;
	
	public StatusConsole(int width, int height) : base(width, height)
	{
		Instance = this;
		var borderParams = Border.BorderParameters.GetDefault()
			.ChangeBorderColors(Color.White, Color.Black);

		// Border constructor assigns itself via side effect
		var _ = new Border(this, borderParams);
	}

	private void Redraw()
	{
		Surface.Clear();
		
		Surface.Print(0, 0, $"HP: {_currentHealth}/{_maxHealth}");
	}
	
	public void RegisterPlayer(Actor actor)
	{
		_currentHealth = actor.Health;
		_maxHealth = actor.ActorType.MaxHealth;
		_inventory = actor.Inventory;
		
		actor.OnHealthChanged += UpdateHealth;
		actor.OnInventoryChanged += UpdateInventory;
		
		Redraw();
	}
	
	private void UpdateHealth(int health, int maxHealth)
	{
		_currentHealth = health;
		_maxHealth = maxHealth;
		Redraw();
	}

	private void UpdateInventory(Inventory inventory)
	{
		_inventory = inventory;
		Redraw();
	}
}