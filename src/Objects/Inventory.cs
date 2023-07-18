namespace Spelunker;

public class Inventory
{
	public readonly int MaxSize;
	public List<Item> Items;

	public int CurrentlyHeld;

	public Item HeldItem => Items[CurrentlyHeld];
	
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
		
		// If you aren't holding anything and pick something up, you are now holding it.
		Items.Add(item);
		if (CurrentlyHeld == -1)
		{
			CurrentlyHeld = Items.Count - 1;
		}

		return true;
	}

	public bool SwapHeldItem(int idx)
	{
		if (idx >= Items.Count)
		{
			return false;
		}

		CurrentlyHeld = idx;
		return true;
	}
	
	public bool BreakHeldItem()
	{
		if (CurrentlyHeld == -1)
		{
			return false;
		}
		
		Items.RemoveAt(CurrentlyHeld);
		CurrentlyHeld = -1;
		return true;
	}
}