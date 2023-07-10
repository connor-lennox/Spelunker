using System.Diagnostics;

namespace Spelunker;

public class MoveAction : Action
{
	private readonly int _dx;
	private readonly int _dy;

	public MoveAction(int dx, int dy)
	{
		_dx = dx;
		_dy = dy;
	}

	public override bool Execute(Actor instigator)
	{
		Debug.Assert(instigator.World != null, "instigator.World != null");
		if (!instigator.World.TilePassable(instigator.X + _dx, instigator.Y + _dy)) return false;
		
		instigator.X += _dx;
		instigator.Y += _dy;

		return true;

	}
}