namespace Spelunker;

public static class KnowledgeCatalog
{
	private static Dictionary<ItemType, ItemKnowledge> _itemKnowledge = new();

	public static void SaveKnowledge()
	{
		KnowledgePersistence.SaveKnowledge(_itemKnowledge);
	}
	
	public static void LoadKnowledge()
	{
		_itemKnowledge = KnowledgePersistence.LoadKnowledge();
	}
	
	public static ItemKnowledge GetKnowledge(ItemType itemType)
	{
		_itemKnowledge.TryAdd(itemType, new ItemKnowledge());
		return _itemKnowledge[itemType];
	}

	public static void GrantItemKnowledge(ItemType itemType)
	{
		_itemKnowledge[itemType] = new ItemKnowledge(127);
		StatusConsole.Instance?.ForceRedraw();
	}
	
	public static void GrantAllKnowledge()
	{
		_itemKnowledge.Clear();
		foreach (var item in ItemType.GetAll())
		{
			_itemKnowledge[item] = new ItemKnowledge(127);
		}
	}

	public static void GrantKnowledge(ItemType itemType, KnowledgeType knowledgeType)
	{
		GetKnowledge(itemType).AddKnowledge(knowledgeType);
		// To allow knowledge to display properly right away, force redraw the status console:
		StatusConsole.Instance?.ForceRedraw();
	}
}