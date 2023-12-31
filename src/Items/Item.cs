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

		if (instigator.Faction == Faction.Ally)
		{
			KnowledgeCatalog.GrantKnowledge(ItemType, KnowledgeType.Effects);
		}
	}

	public void OnAttack(Actor instigator, Actor target)
	{
		foreach (var tag in ItemType.ItemTags)
		{
			tag.OnAttack(instigator, this, target);
		}
		
		if (instigator.Faction == Faction.Ally)
		{
			KnowledgeCatalog.GrantKnowledge(ItemType, KnowledgeType.MeleeDamage);
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
		
		if (holder.Faction == Faction.Ally)
		{
			KnowledgeCatalog.GrantKnowledge(ItemType, KnowledgeType.Description);
			KnowledgeCatalog.GrantKnowledge(ItemType, KnowledgeType.Tags);
			KnowledgeCatalog.GrantKnowledge(ItemType, KnowledgeType.Value);
		}
	}
}