using Point = SadRogue.Primitives.Point;

namespace Spelunker;

public class World
{
	public event System.Action? MapChanged;
	public event Action<Actor>? ActorAdded;
	
	private readonly WorldGenerator _worldGenerator;

	public int CurrentDepth { get; private set; }
	
	public int Width { get; }
	public int Height { get; }
	private readonly TileType[,] _tiles;
	
	public readonly List<Actor> Actors = new();
	public readonly List<DroppedItem> Items = new();
	
	public readonly Actor Player;
	
	private readonly Viewshed _playerViewshed;

	private GridSelector? _selector;

	public World(WorldGenerator generator)
	{
		_worldGenerator = generator;
		Width = generator.Width;
		Height = generator.Height;
		_tiles = new TileType[Width, Height];
		
		Player = new Actor(ActorType.Get("Player"), Faction.Ally, null);
		_playerViewshed = new Viewshed(this);
		
		MoveToNextMap();
	}

	public void MoveToNextMap()
	{
		SwapToMap(_worldGenerator.BuildMap());
		CurrentDepth += 1;
	}

	private void SwapToMap(Map map)
	{
		for (var x = 0; x < Width; x++)
		{
			for (var y = 0; y < Height; y++)
			{
				_tiles[x, y] = map.Tiles[x, y];
			}
		}

		Actors.Clear();
		foreach (var (position, actor) in map.Actors)
		{
			AddActor(actor, position);
		}
		AddActor(Player, map.PlayerSpawnPoint);
		
		Items.Clear();
		foreach (var (position, item) in map.DroppedItems)
		{
			AddItem(item, position);
		}
		
		_playerViewshed.Reset();
		_playerViewshed.CalculateFrom(Player.Position);
		
		MapChanged?.Invoke();
	}

	public void AddActor(Actor actor, Point position)
	{
		SetupGameObject(actor, position);
		Actors.Add(actor);
		ActorAdded?.Invoke(actor);
	}

	public void AddItem(DroppedItem item, Point position)
	{
		SetupGameObject(item, position);
		Items.Add(item);
	}

	private void SetupGameObject(GameObject obj, Point position)
	{
		obj.Position = position;
		obj.World = this;
	}

	public void RemoveActor(Actor actor)
	{
		Actors.Remove(actor);
	}

	public void RemoveItem(DroppedItem item)
	{
		Items.Remove(item);
	}
	
	public void Render(ScreenSurface surface)
	{
		RenderMap(surface);
		RenderObjects(surface);
		RenderSelector(surface);
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
		foreach (var item in Items.Where(item => PositionVisible(item.Position)))
		{
			item.Glyph.CopyAppearanceTo(surface.Surface[item.Position]);
		}
		
		foreach (var actor in Actors.Where(actor => PositionVisible(actor.Position)))
		{
			actor.Glyph.CopyAppearanceTo(surface.Surface[actor.Position]);
		}
	}

	private void RenderSelector(ISurfaceSettable surface)
	{
		if (_selector == null) return;
		foreach (var p in _selector.GetPoints())
		{
			surface.Surface[p].Background = p == _selector.CurrentlySelected
				? GridSelector.HighlightedColor
				: GridSelector.BackgroundColor;
		}
	}
	
	public Actor? ActorAtPoint(Point point)
	{
		return Actors.FirstOrDefault(obj => obj.Position == point);
	}

	public DroppedItem? ItemAtPoint(Point point)
	{
		return Items.FirstOrDefault(obj => obj.Position == point);
	}

	public List<Actor> ActorsInRange(Point point, int radiusSq)
	{
		return Actors.Where(obj =>
			(point.X - obj.Position.X) * (point.X - obj.Position.X) +
			(point.Y - obj.Position.Y) * (point.Y - obj.Position.Y) <= radiusSq).ToList();
	}

	public bool PointInBounds(Point point)
	{
		return point.X >= 0 && point.X < Width && point.Y >= 0 && point.Y < Height;
	}

	public TileType TileAtPoint(Point point)
	{
		return _tiles[point.X, point.Y];
	}
	
	public bool TilePassable(Point point)
	{
		var objBlocking = (ActorAtPoint(point) != null && ActorAtPoint(point)!.Blocking) || 
		                  (ItemAtPoint(point) != null && ItemAtPoint(point)!.Blocking);
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

	public void StartSelection(GridSelector gridSelector)
	{
		_selector = gridSelector;
	}

	public void FinishSelection()
	{
		_selector = null;
	}

	public void RevealMap()
	{
		_playerViewshed.RevealAll();
	}
}