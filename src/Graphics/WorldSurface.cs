namespace Spelunker;

public class WorldSurface : ScreenSurface
{
	private World _world;
	
	public WorldSurface(int width, int height, World world) : base(width, height)
	{
		_world = world;
	}

	public void DrawWorld()
	{
		Surface.Clear();
		_world.Render(this);
	}
}