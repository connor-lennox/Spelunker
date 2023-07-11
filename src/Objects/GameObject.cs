namespace Spelunker;

public abstract class GameObject
{
	public Point Position { get; set; }
	public World? World { get; set; }

	public abstract ColoredGlyph Glyph { get; }
}