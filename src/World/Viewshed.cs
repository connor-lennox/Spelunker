namespace Spelunker;

public enum VisibilityStatus
{
	Hidden,
	Seen,
	Visible,
}

public class Viewshed
{
	private World _world;
	private VisibilityStatus[,] _map;

	public Viewshed(World world)
	{
		_world = world;
		_map = new VisibilityStatus[world.Width, world.Height];
	}

	public VisibilityStatus this[Point p] => _map[p.X, p.Y];
	public VisibilityStatus this[int x, int y] => _map[x, y];

	public void Reset()
	{
		for (var x = 0; x < _map.GetLength(0); x++)
		{
			for (var y = 0; y < _map.GetLength(1); y++)
			{
				_map[x, y] = VisibilityStatus.Hidden;
			}
		}
	}
	
	public void RevealAll()
	{
		for (var x = 0; x < _map.GetLength(0); x++)
		{
			for (var y = 0; y < _map.GetLength(1); y++)
			{
				_map[x, y] = VisibilityStatus.Visible;
			}
		}
	}
	
	public void CalculateFrom(Point center)
	{
		for (var octant = 0; octant < 8; octant++)
		{
			RefreshOctant(center, octant);
		}
	}

	private void RefreshOctant(Point center, int octant)
	{
		var line = new ShadowLine();
		var fullShadow = false;

		for (var row = 0;; row++)
		{
			var pos = center + TransformOctet(row, 0, octant);
			if (!_world.PointInBounds(pos)) break;

			for (var col = 0; col <= row; col++)
			{
				var innerPos = center + TransformOctet(row, col, octant);
				if (!_world.PointInBounds(innerPos)) break;

				if (fullShadow)
				{
					SetNotVisible(innerPos.X, innerPos.Y);
				}
				else
				{
					var projection = ProjectTile(row, col);

					var visible = !line.IsInShadow(projection);
					if (visible)
					{
						SetVisible(innerPos.X, innerPos.Y);
					}
					else
					{
						SetNotVisible(innerPos.X, innerPos.Y);
					}

					if (visible && !_world.TileTransparent(innerPos))
					{
						line.Add(projection);
						fullShadow = line.IsFullShadow();
					}
				}
			}
		}
	}

	private void SetVisible(int x, int y)
	{
		_map[x, y] = VisibilityStatus.Visible;
	}

	private void SetNotVisible(int x, int y)
	{
		_map[x, y] = _map[x, y] == VisibilityStatus.Hidden ? VisibilityStatus.Hidden : VisibilityStatus.Seen;
	}
	
	private static Shadow ProjectTile(int row, int col)
	{
		var topLeft = (float)col / (row + 2);
		var bottomRight = (col + 1f) / (row + 1);
		return new Shadow(topLeft, bottomRight);
	}

	private static Point TransformOctet(int x, int y, int octant) =>
		octant switch
		{
			0 => (y, -x),
			1 => (x, -y),
			2 => (x, y),
			3 => (y, x),
			4 => (-y, x),
			5 => (-x, y),
			6 => (-x, -y),
			7 => (-y, -x),
			_ => throw new ArgumentException("invalid octet")
		};
}

internal class ShadowLine
{
	public readonly List<Shadow> Shadows = new();

	public bool IsInShadow(Shadow projection)
	{
		return Shadows.Any(shadow => shadow.Contains(projection));
	}

	public bool IsFullShadow() => Shadows is [{ Start: 0, End: 1 }];

	public void Add(Shadow shadow)
	{
		var index = 0;
		for (; index < Shadows.Count; index++)
		{
			if (Shadows[index].Start >= shadow.Start) break;
		}

		var overlappingPrevious = index > 0 && Shadows[index - 1].End > shadow.Start;
		var overlappingNext = index < Shadows.Count && Shadows[index].Start < shadow.End;

		if (overlappingNext)
		{
			if (overlappingPrevious)
			{
				Shadows[index - 1].End = Shadows[index].End;
				Shadows.RemoveAt(index);
			}
			else
			{
				Shadows[index].Start = shadow.Start;
			}
		}
		else
		{
			if (overlappingPrevious)
			{
				Shadows[index - 1].End = shadow.End;
			}
			else
			{
				Shadows.Insert(index, shadow);
			}
		}
	}
}

internal class Shadow
{
	public float Start;
	public float End;

	public Shadow(float start, float end)
	{
		Start = start;
		End = end;
	}

	public bool Contains(Shadow other)
	{
		return Start <= other.Start && End >= other.End;
	}
}