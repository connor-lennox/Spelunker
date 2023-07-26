namespace Spelunker;

public class ChargerAgent : BaseAgent
{
	public override Action Decide()
	{
		// If not on screen do nothing.
		if (!Actor.World.PositionVisible(Actor.Position))
		{
			return new WaitAction();
		}
		
		var target = FindPlayer();
		var dist = Math.Abs(Actor.Position.X - target.Position.X) + Math.Abs(Actor.Position.Y - target.Position.Y);

		if (dist <= 1)
		{
			return new AttackAction(target.Position);
		}

		var path = FindPath(target.Position);

		if (path.Count > 0)
		{
			return new MoveAction(path[0]);
		}

		return new WaitAction();
	}
}