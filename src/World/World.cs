using Point = SadRogue.Primitives.Point;

namespace Spelunker;

public class World
{
	public int Width { get; }
	public int Height { get; }
	private readonly TileType[,] _tiles;
	
	public readonly List<GameObject> Objects = new();
	public readonly Actor Player;
	
	private readonly Viewshed _playerViewshed;

	public World(TileType[,] tiles, int width, int height, Point playerSpawn)
	{
		Width = width;
		Height = height;
		_tiles = tiles;
		
		Player = new Actor(ActorType.Get("Player"), Faction.Player, null);
		AddObject(Player, playerSpawn);
		
		_playerViewshed = new Viewshed(this);
		_playerViewshed.CalculateFrom(Player.Position);
	}

	public void AddObject(GameObject gameObject, Point position)
	{
		gameObject.Position = position;
		gameObject.World = this;
		Objects.Add(gameObject);
	}

	public void RemoveObject(GameObject gameObject)
	{
		Objects.Remove(gameObject);
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
		foreach (var gameObject in Objects.Where(gameObject => gameObject is DroppedItem && PositionVisible(gameObject.Position)))
		{
			gameObject.Glyph.CopyAppearanceTo(surface.Surface[gameObject.Position]);
		}
		
		foreach (var gameObject in Objects.Where(gameObject => gameObject is Actor && PositionVisible(gameObject.Position)))
		{
			gameObject.Glyph.CopyAppearanceTo(surface.Surface[gameObject.Position]);
		}
	}

	public GameObject? ObjectAtPoint(Point point)
	{
		return Objects.FirstOrDefault(obj => obj.Position == point);
	}

	public List<GameObject> ObjectsInRange(Point point, int radiusSq)
	{
		return Objects.Where(obj =>
			(point.X - obj.Position.X) * (point.X - obj.Position.X) +
			(point.Y - obj.Position.Y) * (point.Y - obj.Position.Y) <= radiusSq).ToList();
	}

	public bool PointInBounds(Point point)
	{
		return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
	}
	
	public bool TilePassable(Point point)
	{
		var objBlocking = ObjectAtPoint(point) != null && ObjectAtPoint(point)!.Blocking;
		return _tiles[point.X, point.Y].Passable && !objBlocking;
	}

	public bool TileTransparent(Point point)
	{
		return _tiles[point.X, point.Y].Transparent;
	}

	public bool PositionVisible(Point point)
	{
		return _playerViewshed[point] == VisibilityStatus.Visible;
	}

	public void UpdateVisibility()
	{
		_playerViewshed.CalculateFrom(Player.Position);
	}
}