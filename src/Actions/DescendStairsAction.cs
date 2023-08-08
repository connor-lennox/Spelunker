namespace Spelunker;

public class DescendStairsAction : Action
{
	public override bool Execute(Actor instigator)
	{
		if (instigator.World.TileAtPoint(instigator.Position) != TileType.Stairs)
		{
			return false;
		}
		
		Logger.Log($"{instigator.ActorType.Name} descends the stairs...");
		return true;
	}
}