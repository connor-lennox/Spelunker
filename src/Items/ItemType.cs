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
			ItemTypes[i.Name] = i;
		}
	}
	private static readonly Dictionary<string, ItemType> ItemTypes = new();
	public static ItemType Get(string name) => ItemTypes[name];
	
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
}