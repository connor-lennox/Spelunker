using System.Diagnostics;

namespace Spelunker;

public class MoveAction : PositionalAction
{
	public MoveAction(Point targetPoint) : base(targetPoint)
	{
	}

	public override bool Execute(Actor instigator)
	{
		Debug.Assert(instigator.World != null, "instigator.World != null");
		
		if (!instigator.World.TilePassable(_targetPoint)) return false;

		instigator.Position = _targetPoint;

		return true;

	}
}