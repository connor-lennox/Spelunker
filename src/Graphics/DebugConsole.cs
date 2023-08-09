using SadConsole.UI;

namespace Spelunker;

public class DebugConsole : Console
{
	private string _command = "";
	
	public DebugConsole(int width) : base(width, 1)
	{
		Surface.DefaultBackground = Color.Black;
		var borderParams = Border.BorderParameters.GetDefault()
			.ChangeBorderColors(Color.White, Color.Black);

		// Border constructor assigns itself via side effect
		var _ = new Border(this, borderParams);
	}

	public void AddCharacter(char c)
	{
		_command += c;
		Redraw();
	}

	public void RemoveCharacter()
	{
		if (_command.Length == 0)
		{
			return;
		}
		_command = _command[..^1];
		Redraw();
	}

	public void ExecuteCommand()
	{
		DebugCommandExecutor.RunCommand(_command);
		Hide();
	}

	public void Show()
	{
		_command = "";
		Redraw();
		IsVisible = true;
	}

	public void Hide()
	{
		IsVisible = false;
	}

	private void Redraw()
	{
		Surface.Clear();
		Surface.Print(0, 0, _command);
	}
}