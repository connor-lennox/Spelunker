namespace Spelunker;

public class BumpAction : Action
{
	private Point _delta;
	
	public BumpAction(Point delta)
	{
		_delta = delta;
	}

	public override bool Execute(Actor instigator)
	{
		var targetPos = instigator.Position + _delta;
		
		if (instigator.World.TilePassable(targetPos))
		{
			return new MoveAction(targetPos).Execute(instigator);
		}
		
		if (instigator.World.ObjectAtPoint(targetPos) is Actor)
		{
			return new AttackAction(targetPos).Execute(instigator);
		}

		return false;
	}
}