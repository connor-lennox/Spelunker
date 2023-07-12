namespace Spelunker;

public abstract class ItemEffect
{
	public abstract bool Execute(Actor instigator, Item item, Point target);
}