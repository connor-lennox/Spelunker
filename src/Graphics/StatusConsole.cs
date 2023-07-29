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
		
		Surface.Print(0, 2, "Inventory:");
		for (var i = 0; i < _inventory.MaxSize; i++)
		{
			var slotText = _inventory.Items.Count > i ? _inventory.Items[i].Name : "";
			var slotColor = _inventory.CurrentlyHeld == i ? Color.Yellow : Color.White;
			Surface.Print(0, 3+i, $"{(i + 1) % 10}: {slotText}", slotColor);
		}
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