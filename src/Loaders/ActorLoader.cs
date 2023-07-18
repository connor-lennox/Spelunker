namespace Spelunker;

/// <summary>
/// Actor Format:
/// Name, Glyph, Color, Health, InventorySize, SpawnWeight
/// </summary>
public class ActorLoader : BaseLoader<ActorType>
{
	protected override int ExpectedColumns => 6;

	protected override ActorType Translate(string[] entry)
	{
		var name = entry[0];
		var glyph = entry[1][0];
		var color = Colors.FromName(entry[2]);
		var health = int.Parse(entry[3]);
		var inventorySize = int.Parse(entry[4]);
		var spawnWeight = int.Parse(entry[5]);

		return new ActorType(
			name, 
			new ColoredGlyph(color, Color.TransparentBlack, glyph), 
			health, 
			inventorySize, 
			spawnWeight
		);
	}
}