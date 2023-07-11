using Point = SadRogue.Primitives.Point;

namespace Spelunker;

public class World
{
	public int Width { get; }
	public int Height { get; }
	private TileType[,] _tiles;
	
	private readonly List<GameObject> _objects = new();
	private Actor _player;

	private Viewshed _playerViewshed;

	public World(TileType[,] tiles, int width, int height, Point playerSpawn)
	{
		Width = width;
		Height = height;
		_tiles = tiles;

		_playerViewshed = new Viewshed(this);
		
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
		for (var x = 0; x < Width; x++)
		{
			for (var y = 0; y < Height; y++)
			{
				if (_playerViewshed[x, y] == VisibilityStatus.Visible || _playerViewshed[x, y] == VisibilityStatus.Seen)
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

	public bool PointInBounds(Point point)
	{
		return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
	}
	
	public bool TilePassable(Point point)
	{
		return _tiles[point.X, point.Y].Passable;
	}

	public bool TileTransparent(Point point)
	{
		return _tiles[point.X, point.Y].Transparent;
	}

	public void MovePlayer(int dx, int dy)
	{
		_player.ExecuteAction(new MoveAction(dx, dy));
		_playerViewshed.CalculateFrom(_player.Position);
	}
}