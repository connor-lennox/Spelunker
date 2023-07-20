using System.Diagnostics;

namespace Spelunker;

public class AttackAction : Action
{
	private readonly int _dx;
	private readonly int _dy;

	public AttackAction(int dx, int dy)
	{
		_dx = dx;
		_dy = dy;
	}

	public override bool Execute(Actor instigator)
	{
		Debug.Assert(instigator.World != null, "instigator.World != null");
		
		var targetPos = instigator.Position + (_dx, _dy);

		if (instigator.World.ObjectAtPoint(targetPos) is not Actor target) return false;
		
		System.Console.WriteLine($"You kick the {target.ActorType.Name}!");
		return true;

	}
}