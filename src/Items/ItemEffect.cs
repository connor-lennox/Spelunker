namespace Spelunker;

public abstract class ItemEffect
{
	public abstract bool Execute(Actor instigator, Point target);
}