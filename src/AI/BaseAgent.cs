namespace Spelunker;

public abstract class BaseAgent
{
	public abstract Action Decide();

	public Actor Actor;

	protected Actor FindPlayer()
	{
		return Actor.World.Player;
	}

	protected List<Point> FindPath(Point target)
	{
		return Pathfinder.GetPath(
			Actor.World ?? throw new InvalidOperationException("Actor can't pathfind if not in world!"),
			Actor.Position,
			target);
	}
}