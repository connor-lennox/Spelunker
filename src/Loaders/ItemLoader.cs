namespace Spelunker;

/// <summary>
/// Item Format:
/// Name, Glyph, Color, Value, TargetingMode, Range, Effects
/// </summary>
public class ItemLoader : BaseLoader<ItemType>
{
	protected override int ExpectedColumns => 7;

	protected override ItemType Translate(string[] entry)
	{
		var name = entry[0];
		var glyph = entry[1][0];
		var color = Colors.FromName(entry[2]);
		var value = int.Parse(entry[3]);
		var targetingMode = TargetingModes.GetMode(entry[4]);
		var range = int.Parse(entry[5]);
		var effects = ParseEffects(entry[6]);

		return new ItemType(
			name, 
			new ColoredGlyph(color, Color.TransparentBlack, glyph),
			value,
			targetingMode, 
			range, 
			effects
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
}