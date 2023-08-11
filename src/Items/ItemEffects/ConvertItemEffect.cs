namespace Spelunker;

public class ConvertItemEffect : ItemEffect
{
	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World?.ActorAtPoint(target) is not { } targetActor) return false;

		targetActor.Faction = instigator.Faction;
		Logger.Log("Wololo!");
		Logger.Log($"The {targetActor.ActorType.Name} became an {instigator.Faction}!");
		return true;
	}

	public override string ToString()
	{
		return "CONVERT";
	}
}