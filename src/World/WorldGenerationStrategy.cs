namespace Spelunker;

public abstract class WorldGenerationStrategy
{
	protected readonly Random Random = new();
	
	public abstract World BuildWorld(int width, int height);
}

public class RoomWorldGenerationStrategy : WorldGenerationStrategy
{
	private readonly int _numRooms;
	private readonly int _roomMinDim;
	private readonly int _roomMaxDim;

	public RoomWorldGenerationStrategy(int numRooms, int roomMinDim, int roomMaxDim)
	{
		_numRooms = numRooms;
		_roomMinDim = roomMinDim;
		_roomMaxDim = roomMaxDim;
	}

	public override World BuildWorld(int width, int height)
	{
		var tiles = new TileType[width, height];
		for (var x = 0; x < width; x++)
		{
			for (var y = 0; y < height; y++)
			{
				tiles[x, y] = TileType.Wall;
			}
		}

		var rooms = new List<Room>();
		for (var i = 0; i < _numRooms; i++)
		{
			var r = RandomRoom(width, height);
			if (!rooms.Any(e => e.Intersects(r)))
			{
				rooms.Add(r);
			}
		}

		foreach (var room in rooms)
		{
			CarveRoom(tiles, room);
		}

		for (var i = 0; i < rooms.Count - 1; i++)
		{
			TunnelBetween(tiles, rooms[i], rooms[i+1]);
		}

		var world = new World(tiles, width, height, rooms[0].Center());

		foreach (var room in rooms)
		{
			SpawnEnemies(world, room);
			SpawnItems(world, room);
		}
		
		return world;
	}

	private Room RandomRoom(int dungeonWidth, int dungeonHeight)
	{
		var w = Random.Next(_roomMinDim, _roomMaxDim);
		var h = Random.Next(_roomMinDim, _roomMaxDim);
		var x = Random.Next(0, dungeonWidth - w - 1);
		var y = Random.Next(0, dungeonHeight - h - 1);

		return new Room(x, y, w, h);
	}
	
	private void CarveRoom(TileType[,] tiles, Room room)
	{
		foreach (var (x, y) in room.InternalPoints())
		{
			tiles[x, y] = TileType.Floor;
		}
	}

	private void SpawnEnemies(World world, Room room)
	{
		var numEnemies = Random.Next(3);
		var candidatePoints = room.InternalPoints().ToArray();
		for (var i = 0; i < numEnemies; i++)
		{
			var p = candidatePoints[Random.Next(candidatePoints.Length)];
			if (world.ActorAtPoint(p) == null)
			{
				world.AddActor(new Actor(ActorType.GetRandom(), Faction.Enemy, new ChargerAgent()), p);
			}
		}
	}

	private void SpawnItems(World world, Room room)
	{
		var numItems = Random.Next(1, 4);
		var candidatePoints = room.InternalPoints().ToArray();
		for (var i = 0; i < numItems; i++)
		{
			var p = candidatePoints[Random.Next(candidatePoints.Length)];
			if (world.ItemAtPoint(p) == null)
			{
				world.AddItem(new DroppedItem(ItemType.GetRandom()), p);
			}
		}
	}

	private void TunnelBetween(TileType[,] tiles, Room room1, Room room2)
	{
		var p1 = room1.Center();
		var p2 = room2.Center();
		var corner = Random.Next(0, 2) == 0 ? new Point(p1.X, p2.Y) : new Point(p2.X, p1.Y);
		
		foreach (var (x, y) in new BresenhamEnumerator(p1, corner))
		{
			tiles[x, y] = TileType.Floor;
		}
		foreach (var (x, y) in new BresenhamEnumerator(corner, p2))
		{
			tiles[x, y] = TileType.Floor;
		}
	}
	
	private readonly struct Room
	{
		private readonly int _x1;
		private readonly int _y1;
		private readonly int _x2;
		private readonly int _y2;

		public Room(int x, int y, int width, int height)
		{
			_x1 = x;
			_y1 = y;
			_x2 = x + width;
			_y2 = y + height;
		}

		public IEnumerable<(int, int)> InternalPoints()
		{
			for (var x = _x1 + 1; x < _x2; x++)
			{
				for (var y = _y1 + 1; y < _y2; y++)
				{
					yield return (x, y);
				}
			}
		}

		public Point Center()
		{
			return new Point((_x2 + _x1) / 2, (_y2 + _y1) / 2);
		}

		public bool Intersects(Room other) => _x1 <= other._x2 && _x2 >= other._x1 && _y1 <= other._y2 && _y2 >= other._y1;
	}
}