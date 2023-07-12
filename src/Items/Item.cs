namespace Spelunker;

/// <summary>
/// A single instance of an Item. Refers to its ItemType for static data.
/// </summary>
public class Item
{
	public readonly ItemType ItemType;

	public Item(ItemType itemType)
	{
		ItemType = itemType;
	}

	public string Name => ItemType.Name;
	public ColoredGlyph Glyph => ItemType.Glyph;
	
	public void Use(Actor instigator, Point target)
	{
		foreach (var effect in ItemType.UseEffects)
		{
			effect.Execute(instigator, target);
		}
	}
}