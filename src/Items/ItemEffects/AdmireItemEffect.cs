namespace Spelunker;

/// <summary>
/// A "no-op" effect used for things that don't do anything when you use them.
/// </summary>
public class AdmireItemEffect : ItemEffect
{
	public override bool Execute(Actor instigator, Item item, Point target)
	{
		Logger.Log($"You admire the craftsmanship of the {item.ItemType.Name}");
		return true;
	}
}