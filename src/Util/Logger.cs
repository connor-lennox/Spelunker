namespace Spelunker;

public static class Logger
{
	public static readonly List<LogMessage> LogContents = new();
	public static event Action<LogMessage>? OnLogReceived;

	public static void Log(string contents)
	{
		var message = new LogMessage(GameTime.Time, contents);
		LogContents.Add(message);
		OnLogReceived?.Invoke(message);
	}
}

public class LogMessage
{
	public readonly int Time;
	public readonly string Contents;

	public LogMessage(int time, string contents)
	{
		Time = time;
		Contents = contents;
	}
}