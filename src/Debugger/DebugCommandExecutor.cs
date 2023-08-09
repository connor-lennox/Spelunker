namespace Spelunker;

public static class DebugCommandExecutor
{
	private static Engine _engine = null!;

	public static void Setup(Engine engine)
	{
		_engine = engine;
	}

	public static void RunCommand(string command)
	{
		var tokens = command.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

		if (tokens.Length == 0)
		{
			return;
		}
		
		switch (tokens[0])
		{
			case "GIVE":
				RunGiveCommand(string.Join(' ', tokens[1..]));
				break;
			case "IDENTIFY":
				RunIdentifyCommand(string.Join(' ', tokens[1..]));
				break;
			case "REVEAL":
				RunRevealCommand();
				break;
			default:
				System.Console.WriteLine($"Unknown command: {tokens[0]}");
				break;
		}
	}

	private static void RunGiveCommand(string itemName)
	{
		var itemType = ItemType.Get(itemName);
		if (itemType == null)
		{
			System.Console.WriteLine($"Unknown item type \"{itemName}\"");
			return;
		}
		_engine.Player.Inventory.AddItem(new Item(itemType));
		System.Console.WriteLine($"Gave a {itemType.Name} to the player.");
	}

	private static void RunIdentifyCommand(string itemName)
	{
		var itemType = ItemType.Get(itemName);
		if (itemType == null)
		{
			System.Console.WriteLine($"Unknown item type \"{itemName}\"");
			return;
		}
		
		KnowledgeCatalog.GrantItemKnowledge(itemType);
	}

	private static void RunRevealCommand()
	{
		_engine.World.RevealMap();
	}
}