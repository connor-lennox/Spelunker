namespace Spelunker;

public class TileType
{
	public static readonly TileType Floor = new(
		new ColoredGlyph(Color.DarkGray, Color.TransparentBlack, '.'),
		new ColoredGlyph(new Color(30, 30, 30), Color.TransparentBlack, '.'),
		true,
		true
	);
	
	public static readonly TileType Wall = new(
		new ColoredGlyph(Color.Gray, Color.TransparentBlack, '#'),
		new ColoredGlyph(new Color(30, 30, 30), Color.TransparentBlack, '#'),
		false,
		false);
	
	public readonly ColoredGlyph VisibleGlyph;
	public readonly ColoredGlyph SeenGlyph;
	public readonly bool Passable;
	public readonly bool Transparent;

	public TileType(ColoredGlyph visibleGlyph, ColoredGlyph seenGlyph, bool passable, bool transparent)
	{
		VisibleGlyph = visibleGlyph;
		SeenGlyph = seenGlyph;
		Passable = passable;
		Transparent = transparent;
	}
}