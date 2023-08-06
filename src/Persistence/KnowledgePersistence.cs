namespace Spelunker;

public static class KnowledgePersistence
{
	public static string GetDefaultKnowledgeDataPath()
	{
		return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TripleSeven", "Spelunker", "knowledge.sav");
	}

	public static void SaveKnowledge(Dictionary<ItemType, ItemKnowledge> knowledge)
	{
		SaveKnowledge(GetDefaultKnowledgeDataPath(), knowledge);
	}
	
	public static void SaveKnowledge(string filepath, Dictionary<ItemType, ItemKnowledge> knowledge)
	{
		using var writer = new StreamWriter(filepath);
		foreach (var pair in knowledge)
		{
			writer.WriteLine($"{pair.Key.Name} {pair.Value.ToInt()}");
		}
	}

	public static Dictionary<ItemType, ItemKnowledge> LoadKnowledge()
	{
		return LoadKnowledge(GetDefaultKnowledgeDataPath());
	}
	
	public static Dictionary<ItemType, ItemKnowledge> LoadKnowledge(string filepath)
	{
		var dict = new Dictionary<ItemType, ItemKnowledge>();
		using var reader = new StreamReader(filepath);
		while (reader.ReadLine() is { } line)
		{
			var parts = line.Split();
			dict[ItemType.Get(parts[0])] = new ItemKnowledge(int.Parse(parts[1]));
		}

		return dict;
	}
}