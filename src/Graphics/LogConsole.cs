using SadConsole.UI;

namespace Spelunker;

public class LogConsole : Console
{
	private List<string> _contents = new();

	private int _maxLines;
	
	public LogConsole(int width, int height) : base(width, height)
	{
		_maxLines = height;
		
		var borderParams = Border.BorderParameters.GetDefault()
			.ChangeBorderColors(Color.White, Color.Black);

		// Border constructor assigns itself via side effect
		var _ = new Border(this, borderParams);

		Logger.OnLogReceived += UpdateContents;
	}

	private void UpdateContents(string newLine)
	{
		_contents.Add(newLine);
		if (_contents.Count > _maxLines)
		{
			_contents.RemoveAt(0);
		}
		DrawContents();
	}

	private void DrawContents()
	{
		Surface.Clear();
		for (var i = 0; i < _contents.Count; i++)
		{
			Surface.Print(0, i, _contents[i]);
		}
	}
}