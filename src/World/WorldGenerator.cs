namespace Spelunker;

public class WorldGenerator
{
	private MapGenerationStrategy _strategy;
	public readonly int Width;
	public readonly int Height;
	
	public WorldGenerator(int width, int height, MapGenerationStrategy strategy)
	{
		Width = width;
		Height = height;
		_strategy = strategy;
	}

	public Map BuildMap()
	{
		return _strategy.BuildMap(Width, Height);
	}
}