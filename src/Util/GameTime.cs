namespace Spelunker;

public static class GameTime
{
	public static int Time { get; private set; }

	public static void NextTurn()
	{
		Time++;
	}
}