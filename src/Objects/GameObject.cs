namespace Spelunker;

public abstract class GameObject
{
	public int X { get; set; }
	public int Y { get; set; }
	public World? World { get; set; }

	public abstract ColoredGlyph Glyph { get; }
}