using SadConsole.UI;

namespace Spelunker;

public class StatusConsole : Console
{
	public static StatusConsole? Instance;

	private int _currentHealth;
	private int _maxHealth;
	private Inventory _inventory;
	private World _world;
	
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
		
		Surface.Print(0, 0, $"Depth: {_world.CurrentDepth}");
		
		Surface.Print(0, 2, $"HP: {_currentHealth}/{_maxHealth}");
		
		Surface.Print(0, 4, "Inventory:");
		for (var i = 0; i < _inventory.MaxSize; i++)
		{
			var slotText = _inventory.Items.Count > i ? _inventory.Items[i].Name : "";
			var slotColor = _inventory.CurrentlyHeld == i ? Color.Yellow : Color.White;
			Surface.Print(0, 5+i, $"{(i + 1) % 10}: {slotText}", slotColor);
		}

		var heldInfo = _inventory.HeldItem?.ItemType.GetInfo();
		if (heldInfo != null)
		{
			var heldStatusTop = _inventory.MaxSize + 4;
			Surface.Print(0, heldStatusTop, "Held:");
			for (var i = 0; i < heldInfo.Count; i++)
			{
				Surface.Print(0, heldStatusTop + 1 + i, heldInfo[i]);
			}
		}
	}
	
	public void RegisterPlayer(Actor actor)
	{
		_currentHealth = actor.Health;
		_maxHealth = actor.ActorType.MaxHealth;
		_inventory = actor.Inventory;
		_world = actor.World;
		
		actor.OnHealthChanged += UpdateHealth;
		actor.Inventory.OnInventoryChanged += UpdateInventory;
		
		Redraw();
	}

	public void ForceRedraw()
	{
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