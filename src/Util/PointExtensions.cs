namespace Spelunker;

public static class PointExtensions
{
	public static float SqDistance(this Point point, Point other)
	{
		return (point.X - other.X) * (point.X - other.X) + (point.Y - other.Y) * (point.Y - other.Y);
	}
}