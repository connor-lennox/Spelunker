namespace Spelunker;

public class ResurrectItemEffect : ItemEffect
{
	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World?.ActorAtPoint(target) is not { } targetActor) return false;

		if (targetActor.Alive)
		{
			Logger.Log($"The {targetActor.ActorType.Name} was already alive, so it couldn't be resurrected.");
			return true;
		}

		targetActor.ForceHeal(1);
		Logger.Log($"The {targetActor.ActorType.Name} was resurrected!");
		return true;
	}
}