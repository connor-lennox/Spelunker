namespace Spelunker;

/// <summary>
/// A single instance of an Item. Refers to its ItemType for static data.
/// </summary>
public class Item
{
	public readonly ItemType ItemType;

	public Item(ItemType itemType)
	{
		ItemType = itemType;
	}

	public string Name => ItemType.Name;
	public ColoredGlyph Glyph => ItemType.Glyph;
	
	public void Use(Actor instigator, Point target)
	{
		foreach (var effect in ItemType.UseEffects)
		{
			effect.Execute(instigator, this, target);
		}

		foreach (var tag in ItemType.ItemTags)
		{
			tag.OnUse(instigator, this);
		}
	}

	public void OnAttack(Actor instigator, Actor target)
	{
		foreach (var tag in ItemType.ItemTags)
		{
			tag.OnAttack(instigator, this, target);
		}
	}

	public void OnHitWhileHolding(Actor holder, Actor instigator)
	{
		foreach (var tag in ItemType.ItemTags)
		{
			tag.OnHitWhileHolding(holder, this, instigator);
		}
	}

	public void OnPickup(Actor holder)
	{
		foreach (var tag in ItemType.ItemTags)
		{
			tag.OnPickup(holder, this);
		}
	}
}