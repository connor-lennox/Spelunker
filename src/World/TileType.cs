namespace Spelunker;

public class TileType
{
	public static TileType Floor = new TileType(new ColoredGlyph(Color.DarkGray, Color.TransparentBlack, '.'), true, true);
	public static TileType Wall = new TileType(new ColoredGlyph(Color.Gray, Color.TransparentBlack, '#'), false, false);
	
	public readonly ColoredGlyph Glyph;
	public readonly bool Passable;
	public readonly bool Transparent;

	public TileType(ColoredGlyph glyph, bool passable, bool transparent)
	{
		Glyph = glyph;
		Passable = passable;
		Transparent = transparent;
	}
}