namespace Spelunker;

public class TileType
{
	public static TileType Floor = new TileType(new ColoredGlyph(Color.DarkGray, Color.TransparentBlack, '.'), true);
	public static TileType Wall = new TileType(new ColoredGlyph(Color.Gray, Color.TransparentBlack, '#'), false);
	
	public readonly ColoredGlyph Glyph;
	public readonly bool Passable;

	public TileType(ColoredGlyph glyph, bool passable)
	{
		Glyph = glyph;
		Passable = passable;
	}
}