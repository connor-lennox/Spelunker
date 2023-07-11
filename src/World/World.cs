namespace Spelunker;

public class World
{
	private int _width;
	private int _height;
	private TileType[,] _tiles;
	
	private readonly List<GameObject> _objects = new();
	private Actor _player;

	public World(TileType[,] tiles, int width, int height, Point playerSpawn)
	{
		_width = width;
		_height = height;
		_tiles = tiles;
		
		_player = new Actor(ActorType.Player);
		AddObject(_player, playerSpawn);
		AddObject(new Actor(ActorType.Bandit), (16, 11));
	}

	private void AddObject(GameObject gameObject, Point position)
	{
		gameObject.Position = position;
		gameObject.World = this;
		_objects.Add(gameObject);
	}
	
	public void Render(ScreenSurface surface)
	{
		RenderMap(surface);
		RenderObjects(surface);
	}

	private void RenderMap(ISurfaceSettable surface)
	{
		for (var x = 0; x < _width; x++)
		{
			for (var y = 0; y < _height; y++)
			{
				_tiles[x, y].Glyph.CopyAppearanceTo(surface.Surface[x, y]);
			}
		}
	}

	private void RenderObjects(ISurfaceSettable surface)
	{
		foreach (var gameObject in _objects)
		{
			gameObject.Glyph.CopyAppearanceTo(surface.Surface[gameObject.Position]);
		}
	}

	public bool TilePassable(Point point)
	{
		return _tiles[point.X, point.Y].Passable;
	}

	public void MovePlayer(int dx, int dy)
	{
		_player.ExecuteAction(new MoveAction(dx, dy));
	}
}