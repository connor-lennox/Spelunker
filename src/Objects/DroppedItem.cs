namespace Spelunker;

public class DroppedItem : GameObject
{
	public readonly ItemType ItemType;

	public DroppedItem(ItemType itemType)
	{
		ItemType = itemType;
	}

	public override ColoredGlyph Glyph => ItemType.Glyph;
	public override List<string> GetHoverInfo()
	{
		return ItemType.GetInfo();
	}
}