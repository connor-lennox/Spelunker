namespace Spelunker;

public static class KnowledgeCatalog
{
	private static Dictionary<ItemType, ItemKnowledge> ItemKnowledge = new();

	public static void SaveKnowledge()
	{
		KnowledgePersistence.SaveKnowledge(ItemKnowledge);
	}
	
	public static void LoadKnowledge()
	{
		ItemKnowledge = KnowledgePersistence.LoadKnowledge();
	}
	
	public static ItemKnowledge GetKnowledge(ItemType itemType)
	{
		ItemKnowledge.TryAdd(itemType, new ItemKnowledge());
		return ItemKnowledge[itemType];
	}

	public static void GrantAllKnowledge()
	{
		ItemKnowledge.Clear();
		foreach (var item in ItemType.GetAll())
		{
			ItemKnowledge[item] = new ItemKnowledge(127);
		}
	}

	public static void GrantKnowledge(ItemType itemType, KnowledgeType knowledgeType)
	{
		GetKnowledge(itemType).AddKnowledge(knowledgeType);
	}
}