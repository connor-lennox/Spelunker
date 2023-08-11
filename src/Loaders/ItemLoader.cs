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
				case "RESURRECT":
					// RESURRECT
					effects.Add(new ResurrectItemEffect());
					break;
				case "EXPLODE":
					// EXPLODE <damage> <radius>
					effects.Add(new ExplodeItemEffect(int.Parse(tokens[idx++]), int.Parse(tokens[idx++])));
					break;
				case "BLINK":
					// BLINK
					effects.Add(new BlinkItemEffect());
					break;
				case "SUMMON":
					// SUMMON <actor type>
					effects.Add(new SummonItemEffect(ActorType.Get(tokens[idx++])));
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
		var tokens = tagString.Split(' ');

		var idx = 0;
		while (idx < tokens.Length)
		{
			var tagId = tokens[idx++];
			if (tagId.Length == 0) continue;

			switch (tagId.ToUpper())
			{
				case "FRAGILE":
					tags.Add(new FragileItemTag());
					break;
				case "AUTOUSE":
					tags.Add(new AutouseItemTag());
					break;
				case "CONSUMABLE":
					tags.Add(new ConsumableItemTag());
					break;
				case "SPIKY":
					tags.Add(new SpikyItemTag(int.Parse(tokens[idx++])));
					break;
				case "DRAINING":
					tags.Add(new DrainingItemTag(int.Parse(tokens[idx++])));
					break;
				default:
					throw new ArgumentException($"unknown item tag '{tagId}'");
			}
		}

		return tags;
	}
}