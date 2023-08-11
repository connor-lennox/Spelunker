namespace Spelunker;

public class ConvertItemEffect : ItemEffect
{
	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World?.ActorAtPoint(target) is not { } targetActor) return false;

		Logger.Log("Wololo!");

		// You can't convert the player. Not allowed.
		if (targetActor.World.Player == targetActor)
		{
			Logger.Log("The Player resisted conversion!");
			return true;
		}
		
		targetActor.Faction = instigator.Faction;
		Logger.Log($"The {targetActor.ActorType.Name} became an {instigator.Faction}!");
		return true;
	}

	public override string ToString()
	{
		return "CONVERT";
	}
}