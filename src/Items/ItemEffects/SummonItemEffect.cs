namespace Spelunker;

public class SummonItemEffect : ItemEffect
{
	private ActorType _actorType;

	public SummonItemEffect(ActorType actorType)
	{
		_actorType = actorType;
	}

	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (instigator.World.ActorAtPoint(target) != null)
		{
			return false;
		}
		
		instigator.World.AddActor(new Actor(_actorType, instigator.Faction, new ChargerAgent()), target);
		Logger.Log($"The {_actorType.Name} appears!");
		return true;
	}

	public override string ToString()
	{
		return $"SUMMON {_actorType.Name.ToUpper()}";
	}
}