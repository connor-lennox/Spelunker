namespace Spelunker;

public class ActorType
{
	public static readonly ActorType Player = new(new ColoredGlyph(Color.Yellow, Color.TransparentBlack, '@'), 26);
	public static readonly ActorType Bandit = new(new ColoredGlyph(Color.White, Color.TransparentBlack, 'b'), 2);
	
	public readonly ColoredGlyph Glyph;
	public readonly int InventorySize;

	public ActorType(ColoredGlyph glyph, int inventorySize)
	{
		Glyph = glyph;
		InventorySize = inventorySize;
	}
}