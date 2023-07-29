using SadConsole.UI;

namespace Spelunker;

public class HoverInfoConsole : Console
{
	private Border _border;

	private static readonly Border.BorderParameters BorderParameters = Border.BorderParameters.GetDefault()
		.ChangeBorderColors(Color.Yellow, Color.Black);

	public HoverInfoConsole(int width, int height) : base(width, height)
	{
		Surface.DefaultBackground = Color.Black;
		_border = new Border(this, BorderParameters);
	}

	public void DisplayAt(Point position, List<string> contents)
	{
		Resize(contents.Select(c => c.Length).Max(), contents.Count, true);
		Position = position;
		for (var i = 0; i < contents.Count; i++)
		{
			Surface.Print(0, i, contents[i]);
		}
		RebuildBorder();
		IsVisible = true;
	}

	public void Hide()
	{
		IsVisible = false;
	}

	public void RebuildBorder()
	{
		Children.Remove(_border);
		// Border constructor assigns itself via side effect
		_border = new Border(this, BorderParameters);
	}
}