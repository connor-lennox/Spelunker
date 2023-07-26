namespace Spelunker;

public static class Pathfinder
{
	public static List<Point> GetPath(World world, Point start, Point end)
	{
		// A* grid pathfinding
		var open = new PriorityQueue<PathfindingNode, int>();
		open.Enqueue(new PathfindingNode(start, null, 0), 0);
		
		var closed = new HashSet<Point> { start };

		while (open.TryDequeue(out var node, out _))
		{
			foreach (var step in GetNeighboringPositions(node.Position))
			{
				if (step == end)
				{
					// Reconstruct path and flip for result
					// Note: even if the end node is solid we can still path-find towards it!
					return BuildPath(new PathfindingNode(step, node, node.Depth + 1));
				}
				
				// No range restrictions in place for pathfinding
				if (!world.PointInBounds(step)) continue;
				if (!world.TilePassable(step)) continue;
				if (closed.Contains(step)) continue;

				var newDepth = node.Depth + 1;
				open.Enqueue(new PathfindingNode(step, node, newDepth), newDepth);
				closed.Add(step);
			}
		}

		// On pathfinding failure, return an empty list
		return new List<Point>();
	}

	private static List<Point> BuildPath(PathfindingNode endNode)
	{
		List<Point> result = new();
		var walk = endNode;
		while (walk != null)
		{
			// Walk path backwards until the root is reached (root identified via null parent)
			result.Add(walk.Position);
			walk = walk.Parent;
		}

		// Current List is backwards, flip before returning
		result.Reverse();
		// The first element of the path is the current start position. This is redundant.
		result.RemoveAt(0);
		
		return result;
	}
	
	private static IEnumerable<Point> GetNeighboringPositions(Point center)
	{
		yield return new Point(center.X - 1, center.Y);
		yield return new Point(center.X + 1, center.Y);
		yield return new Point(center.X, center.Y - 1);
		yield return new Point(center.X, center.Y + 1);
	}
}

internal record PathfindingNode(Point Position, PathfindingNode? Parent, int Depth);