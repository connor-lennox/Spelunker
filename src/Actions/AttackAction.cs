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

		var heldItem = instigator.Inventory.HeldItem;

		if (heldItem != null)
		{
			System.Console.WriteLine($"{instigator.ActorType.Name} swings the {heldItem.Name}, striking the {target.ActorType.Name} for {heldItem.ItemType.MeleeDamage} damage!");
			target.TakeDamage(heldItem.ItemType.MeleeDamage);
			heldItem.OnAttack(instigator, target);
		}
		else
		{
			System.Console.WriteLine($"{instigator.ActorType.Name} punches the {target.ActorType.Name} for 1 damage!");
			target.TakeDamage(1);
		}
		
		return true;

	}
}