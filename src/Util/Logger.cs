namespace Spelunker;

public static class Logger
{
	public static readonly List<string> LogContents = new();
	public static event Action<string>? OnLogReceived;

	public static void Log(string contents)
	{
		LogContents.Add(contents);
		OnLogReceived?.Invoke(contents);
	}
}