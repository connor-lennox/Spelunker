namespace Spelunker;

public class Map
{
	public readonly TileType[,] Tiles;
	public readonly int Width;
	public readonly int Height;

	public readonly Dictionary<Point, Actor> Actors = new();
	public readonly Dictionary<Point, DroppedItem> DroppedItems = new();


	public readonly Point PlayerSpawnPoint;

	public Map(TileType[,] tiles, Point playerSpawnPoint)
	{
		Tiles = tiles;
		Width = tiles.GetLength(0);
		Height = tiles.GetLength(1);
		PlayerSpawnPoint = playerSpawnPoint;
	}

	public void AddActor(Actor actor, Point position)
	{
		Actors[position] = actor;
	}

	public void AddItem(DroppedItem item, Point position)
	{
		DroppedItems[position] = item;
	}

	public bool CanPlaceActor(Point position) => !Actors.ContainsKey(position);
	public bool CanPlaceItem(Point position) => !DroppedItems.ContainsKey(position);
}