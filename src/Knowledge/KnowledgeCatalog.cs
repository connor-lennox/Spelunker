namespace Spelunker;

public static class KnowledgeCatalog
{
	private static Dictionary<ItemType, ItemKnowledge> ItemKnowledge = new();

	public static void LoadKnowledge(string filepath)
	{
		
	}
	
	public static ItemKnowledge GetKnowledge(ItemType itemType)
	{
		ItemKnowledge.TryAdd(itemType, new ItemKnowledge());
		return ItemKnowledge[itemType];
	}
}