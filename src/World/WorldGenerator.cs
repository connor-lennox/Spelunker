namespace Spelunker;

public class WorldGenerator
{
	private WorldGenerationStrategy _strategy;
	private int _width;
	private int _height;
	
	public WorldGenerator(int width, int height, WorldGenerationStrategy strategy)
	{
		_width = width;
		_height = height;
		_strategy = strategy;
	}

	public World BuildWorld()
	{
		return _strategy.BuildWorld(_width, _height);
	}
}