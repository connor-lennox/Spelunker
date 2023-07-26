using System.Diagnostics;

namespace Spelunker;

public class MoveAction : Action
{
	private readonly Point _endPoint;

	public MoveAction(Point endPoint)
	{
		_endPoint = endPoint;
	}

	public override bool Execute(Actor instigator)
	{
		Debug.Assert(instigator.World != null, "instigator.World != null");
		
		if (!instigator.World.TilePassable(_endPoint)) return false;

		instigator.Position = _endPoint;

		return true;

	}
}