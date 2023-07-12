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
	public readonly string Name;
	public readonly ColoredGlyph Glyph;

	public readonly TargetingMode TargetingMode;
	public readonly int Range;
	
	public readonly List<ItemEffect> UseEffects;

	public ItemType(string name, ColoredGlyph glyph, TargetingMode targetingMode, int range, List<ItemEffect> useEffects)
	{
		Name = name;
		Glyph = glyph;
		TargetingMode = targetingMode;
		Range = range;
		UseEffects = useEffects;
	}
}