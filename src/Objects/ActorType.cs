using System.Diagnostics;

namespace Spelunker;

public class ActorType
{
	public static void RegisterActors(IEnumerable<ActorType> actorTypes)
	{
		foreach (var a in actorTypes)
		{
			ActorTypes[a.Name.ToUpper()] = a;
		}
	}
	
	private static readonly Dictionary<string, ActorType> ActorTypes = new();
	public static ActorType Get(string name) => ActorTypes[name.ToUpper()];

	public static ActorType GetRandom()
	{
		var options = ActorTypes.Values.ToArray();
		var weightSum = options.Sum(o => o.SpawnWeight);
		var rand = new Random().Next(weightSum);
		var count = 0;
		foreach (var option in options)
		{
			if (option.SpawnWeight <= 0) continue;
			count += option.SpawnWeight;
			if (count >= rand)
			{
				return option;
			}
		}

		throw new UnreachableException("ActorType GetRandom found no valid ActorType??");
	}
	
	public readonly string Name;
	public readonly ColoredGlyph Glyph;
	public readonly int MaxHealth;
	public readonly int MeleeDamage;
	public readonly int InventorySize;
	public readonly int SpawnWeight;

	public ActorType(string name, ColoredGlyph glyph, int maxHealth, int meleeDamage, int inventorySize, int spawnWeight)
	{
		Name = name;
		Glyph = glyph;
		MaxHealth = maxHealth;
		MeleeDamage = meleeDamage;
		InventorySize = inventorySize;
		SpawnWeight = spawnWeight;
	}
}