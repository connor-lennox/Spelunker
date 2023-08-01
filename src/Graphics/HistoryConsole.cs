using SadConsole.UI;

namespace Spelunker;

public class HistoryConsole : Console
{
	private int _maxLines;
	private int _offset;

	private List<LogMessage> _contents;
	
	public HistoryConsole(int width, int height) : base(width, height)
	{
		_maxLines = height;
		
		Surface.DefaultBackground = Color.Black;
		var borderParams = Border.BorderParameters.GetDefault()
			.ChangeBorderColors(Color.White, Color.Black);

		// Border constructor assigns itself via side effect
		var _ = new Border(this, borderParams);
		
		UpdateContents();
	}

	public void UpdateContents()
	{
		_contents = Logger.LogContents.SkipLast(_offset).TakeLast(_maxLines).ToList();
		DrawContents();
	}

	public void DrawContents()
	{
		Surface.Clear();
		var drawOffset = _maxLines - _contents.Count;
		for (var i = 0; i < _contents.Count; i++)
		{
			Surface.Print(0, i + drawOffset, $"[{_contents[i].Time:0000}]:", Color.CornflowerBlue);
			Surface.Print(8, i + drawOffset, _contents[i].Contents);
		}
	}

	public void ShiftOffset(int amount)
	{
		_offset = Math.Clamp(_offset + amount, 0, Logger.LogContents.Count - 1);
		UpdateContents();
	}

	public void ResetOffset()
	{
		_offset = 0;
		UpdateContents();
	}
}