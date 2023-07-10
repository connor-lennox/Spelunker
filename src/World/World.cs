namespace Spelunker;

public class World
{
	private int _width;
	private int _height;
	private TileType[,] _tiles;
	
	private readonly List<GameObject> _objects = new();
	private Actor _player;

	public World(TileType[,] tiles, int width, int height)
	{
		_width = width;
		_height = height;
		_tiles = tiles;
		
		_player = new Actor(ActorType.Player);
		AddObject(_player, 10, 10);
		AddObject(new Actor(ActorType.Bandit), 16, 11);
	}

	private void AddObject(GameObject gameObject, int x, int y)
	{
		gameObject.X = x;
		gameObject.Y = y;
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
			gameObject.Glyph.CopyAppearanceTo(surface.Surface[gameObject.X, gameObject.Y]);
		}
	}

	public bool TilePassable(int x, int y)
	{
		return _tiles[x, y].Passable;
	}

	public void MovePlayer(int dx, int dy)
	{
		_player.ExecuteAction(new MoveAction(dx, dy));
	}
}