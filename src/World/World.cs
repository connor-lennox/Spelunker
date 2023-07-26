using Point = SadRogue.Primitives.Point;

namespace Spelunker;

public class World
{
	public int Width { get; }
	public int Height { get; }
	private TileType[,] _tiles;
	
	private readonly List<GameObject> _objects = new();
	private Actor _player;
	private readonly List<Actor> _actors = new();

	private Viewshed _playerViewshed;

	public World(TileType[,] tiles, int width, int height, Point playerSpawn)
	{
		Width = width;
		Height = height;
		_tiles = tiles;
		
		_player = new Actor(ActorType.Get("Player"), Faction.Player);
		AddActor(_player, playerSpawn);
		
		_playerViewshed = new Viewshed(this);
		_playerViewshed.CalculateFrom(_player.Position);
	}

	public void AddObject(GameObject gameObject, Point position)
	{
		gameObject.Position = position;
		gameObject.World = this;
		_objects.Add(gameObject);
	}

	public void AddActor(Actor actor, Point position)
	{
		_actors.Add(actor);
		AddObject(actor, position);
		actor.OnDeath += () => ActorDied(actor);
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

	public GameObject? ObjectAtPoint(Point point)
	{
		// TODO: Optimize this
		foreach (var obj in _objects)
		{
			if (obj.Position == point)
			{
				return obj;
			}
		}

		return null;
	}

	public Actor? ActorAtPoint(Point point)
	{
		foreach (var actor in _actors)
		{
			if (actor.Position == point)
			{
				return actor;
			}
		}

		return null;
	}

	public List<GameObject> ObjectsInRange(Point point, int radiusSq)
	{
		return _objects.Where(obj =>
			(point.X - obj.Position.X) * (point.X - obj.Position.X) +
			(point.Y - obj.Position.Y) * (point.Y - obj.Position.Y) <= radiusSq).ToList();
	}
	
	public List<Actor> ActorsInRange(Point point, int radiusSq)
	{
		return _actors.Where(obj =>
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

	public void MovePlayer(int dx, int dy)
	{
		_player.MoveInDirection(dx, dy);
		_playerViewshed.CalculateFrom(_player.Position);
		DoEnemyTurn();
	}

	private void DoEnemyTurn()
	{
		foreach (var enemy in _actors.Where(enemy => enemy.Faction == Faction.Enemy))
		{
			// System.Console.WriteLine($"{enemy.ActorType.Name} takes their turn");
		}
	}

	private void ActorDied(Actor actor)
	{
		System.Console.WriteLine($"{actor.ActorType.Name} has been slain!");
		_actors.Remove(actor);
		_objects.Remove(actor);
	}
}