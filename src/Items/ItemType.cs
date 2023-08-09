namespace Spelunker;

public enum TargetingMode
{
	Self,
	Tile
}

public static class TargetingModes
{
	public static TargetingMode GetMode(string name)
	{
		return name.ToLower() switch
		{
			"self" => TargetingMode.Self,
			"tile" => TargetingMode.Tile,
			_ => throw new ArgumentException($"unknown targeting mode: {name}")
		};
	}
}

public class ItemType
{
	public static void RegisterItems(IEnumerable<ItemType> itemTypes)
	{
		foreach (var i in itemTypes)
		{
			ItemTypes[i.Name.ToUpper()] = i;
		}
	}
	private static readonly Dictionary<string, ItemType> ItemTypes = new();
	public static IEnumerable<ItemType> GetAll() => ItemTypes.Values;
	public static ItemType? Get(string name) => ItemTypes.TryGetValue(name.ToUpper(), out var type) ? type : null;

	public static ItemType GetRandom()
	{
		var options = ItemTypes.Values.ToArray();
		return options[new Random().Next(options.Length)];
	}
	
	public readonly string Name;
	public readonly ColoredGlyph Glyph;

	public readonly int MeleeDamage;
	public readonly int Value;
	
	public readonly TargetingMode TargetingMode;
	public readonly int Range;
	
	public readonly List<ItemEffect> UseEffects;
	public readonly List<ItemTag> ItemTags;

	public ItemType(string name, ColoredGlyph glyph, int meleeDamage, int value, TargetingMode targetingMode, int range, List<ItemEffect> useEffects, List<ItemTag> tags)
	{
		Name = name;
		Glyph = glyph;
		MeleeDamage = meleeDamage;
		Value = value;
		TargetingMode = targetingMode;
		Range = range;
		UseEffects = useEffects;
		ItemTags = tags;
	}

	public List<string> GetInfo()
	{
		var knowledge = KnowledgeCatalog.GetKnowledge(this);
		var info = new List<string>
		{
			Name,
			""
		};
		
		if (knowledge.IsEmpty)
		{
			info.Add("No known info");
		}

		if (knowledge.HasKnowledge(KnowledgeType.MeleeDamage))
		{
			info.Add($"Melee Damage: {MeleeDamage}");
		}

		if (knowledge.HasKnowledge(KnowledgeType.Value))
		{
			info.Add($"Value: {Value}");
		}

		if (knowledge.HasKnowledge(KnowledgeType.Effects))
		{
			info.Add("Effects:");
			info.AddRange(UseEffects.Select(effect => $"  {effect}"));
		}

		if (knowledge.HasKnowledge(KnowledgeType.Tags))
		{
			info.Add($"Tags:");
			info.AddRange(ItemTags.Select(tag => $"  {tag}"));
		}

		return info;
	}
}