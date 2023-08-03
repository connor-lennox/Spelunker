namespace Spelunker;

public class Inventory
{
	public event Action<Inventory>? OnInventoryChanged;

	public readonly int MaxSize;
	public List<Item> Items;

	public int CurrentlyHeld;

	public Item? HeldItem => CurrentlyHeld >= 0 && CurrentlyHeld < Items.Count ? Items[CurrentlyHeld] : null;
	
	public Inventory(int maxSize)
	{
		MaxSize = maxSize;
		Items = new List<Item>();
	}

	public bool AddItem(Item item)
	{
		if (Items.Count >= MaxSize)
		{
			return false;
		}
		
		// If you pick something up, you are now holding it.
		Items.Add(item);
		CurrentlyHeld = Items.Count - 1;

		OnInventoryChanged?.Invoke(this);
		return true;
	}

	public bool SwapHeldItem(int idx)
	{
		if (idx >= MaxSize || idx < 0)
		{
			return false;
		}

		CurrentlyHeld = idx;
		
		OnInventoryChanged?.Invoke(this);
		return true;
	}
	
	public bool BreakHeldItem()
	{
		if (HeldItem == null)
		{
			return false;
		}
		
		Items.RemoveAt(CurrentlyHeld);
		CurrentlyHeld = -1;
		
		OnInventoryChanged?.Invoke(this);
		return true;
	}
}