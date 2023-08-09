namespace Spelunker;

public class BlinkItemEffect : ItemEffect
{
	public override bool Execute(Actor instigator, Item item, Point target)
	{
		if (!instigator.World!.TilePassable(target))
		{
			Logger.Log($"{instigator.ActorType.Name} failed to warp.");
			return false;
		}
		
		Logger.Log($"{instigator.ActorType.Name} warped!");
		instigator.Position = target;
		return true;
	}

	public override string ToString()
	{
		return "BLINK";
	}
}