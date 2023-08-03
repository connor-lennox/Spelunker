namespace Spelunker;

public class GridSelector
{
	public static readonly Color BackgroundColor = Color.DarkBlue;
	public static readonly Color HighlightedColor = Color.White;
	
	public readonly Point Center;
	public readonly int Range;

	private World _world;
	
	public Point CurrentlySelected { get; private set; }

	public GridSelector(Point center, int range, World world)
	{
		Center = center;
		Range = range;
		_world = world;
		CurrentlySelected = Center;
	}

	public IEnumerable<Point> GetPoints()
	{
		for (var x = -Range; x <= Range; x++)
		{
			for (var y = -Range + Math.Abs(x); y <= Range - Math.Abs(x); y++)
			{
				var p = Center + new Point(x, y);
				if (_world.PointInBounds(p) && _world.PositionVisible(p) && _world.TileTransparent(p))
				{
					yield return p;
				}
			}
		}
	}

	public void MoveSelection(int dx, int dy)
	{
		var p = CurrentlySelected + new Point(dx, dy);
		if (Math.Abs(p.X - Center.X) + Math.Abs(p.Y - Center.Y) <= Range &&
		    _world.PositionVisible(p) &&
		    _world.TileTransparent(p))
		{
			CurrentlySelected = p;
		}
	}
}