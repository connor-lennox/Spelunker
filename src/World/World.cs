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
		
		_player = new Actor(ActorType.Player);
		AddObject(_player, playerSpawn);
		AddObject(new Actor(ActorType.Bandit), (16, 11));
		
		_playerViewshed = new Viewshed(this);
		_playerViewshed.CalculateFrom(_player.Position);
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
				switch (_playerViewshed[x, y])
				{
					case VisibilityStatus.Visible:
						_tiles[x, y].VisibleGlyph.CopyAppearanceTo(surface.Surface[x, y]);
						break;
					case VisibilityStatus.Seen:
						_tiles[x, y].SeenGlyph.CopyAppearanceTo(surface.Surface[x, y]);
						break;
				}
			}
		}
	}

	private void RenderObjects(ISurfaceSettable surface)
	{
		foreach (var gameObject in _objects)
		{
			if (_playerViewshed[gameObject.Position] == VisibilityStatus.Visible)
			{
				gameObject.Glyph.CopyAppearanceTo(surface.Surface[gameObject.Position]);
			}
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