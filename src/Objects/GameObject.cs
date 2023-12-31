namespace Spelunker;

public abstract class GameObject
{
	public Point Position { get; set; }
	public World? World { get; set; }

	public abstract ColoredGlyph Glyph { get; }

	public virtual bool Blocking => false;

	public abstract List<string> GetHoverInfo();
}