namespace Spelunker;

public class AutouseItemTag : ItemTag
{
	public override void OnAttack(Actor instigator, Item item, Actor target)
	{
		item.Use(instigator, target.Position);
	}

	public override string ToString()
	{
		return "AUTOUSE";
	}
}