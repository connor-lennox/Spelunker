namespace Spelunker;

/// <summary>
/// Item Format:
/// Name, Glyph, Color, MeleeDamage, Value, TargetingMode, Range, Effects, Tags
/// </summary>
public class ItemLoader : BaseLoader<ItemType>
{
	protected override int ExpectedColumns => 9;

	protected override ItemType Translate(string[] entry)
	{
		var name = entry[0];
		var glyph = entry[1][0];
		var color = Colors.FromName(entry[2]);
		var meleeDamage = int.Parse(entry[3]);
		var value = int.Parse(entry[4]);
		var targetingMode = TargetingModes.GetMode(entry[5]);
		var range = int.Parse(entry[6]);
		var effects = ParseEffects(entry[7]);
		var tags = ParseTags(entry[8]);

		return new ItemType(
			name, 
			new ColoredGlyph(color, Color.TransparentBlack, glyph),
			meleeDamage,
			value,
			targetingMode, 
			range, 
			effects,
			tags
		);
	}

	private static List<ItemEffect> ParseEffects(string effectString)
	{
		List<ItemEffect> effects = new();
		var tokens = effectString.Split(' ');

		var idx = 0;
		while (idx < tokens.Length)
		{
			var effectId = tokens[idx++];

			// All effects are defined here
			switch (effectId.ToUpper())
			{
				case "DAMAGE":
					// DAMAGE <damage>
					effects.Add(new DamageItemEffect(int.Parse(tokens[idx++])));
					break;
				case "HEAL":
					// HEAL <amount>
					effects.Add(new HealItemEffect(int.Parse(tokens[idx++])));
					break;
				case "EXPLODE":
					// EXPLODE <damage> <radius>
					effects.Add(new ExplodeItemEffect(int.Parse(tokens[idx++]), int.Parse(tokens[idx++])));
					break;
				case "ADMIRE":
					effects.Add(new AdmireItemEffect());
					break;
				default:
					throw new ArgumentException($"unknown item effect verb {effectId}");
			}
		}

		return effects;
	}

	private static List<ItemTag> ParseTags(string tagString)
	{
		var tags = new List<ItemTag>();
		foreach (var tag in tagString.Split(' '))
		{
			if (tag.Length > 0)
			{
				switch (tag.ToUpper())
				{
					case "FRAGILE":
						tags.Add(ItemTag.GetTag<FragileItemTag>());
						break;
					case "AUTOUSE":
						tags.Add(ItemTag.GetTag<AutouseItemTag>());
						break;
					default:
						throw new ArgumentException($"unknown item tag {tag}");
				}
			}
		}

		return tags;
	}
}