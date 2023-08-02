using SadConsole.Input;

namespace Spelunker;

public class WorldSurface : ScreenSurface
{
	public event Action<Point, List<string>?>? OnHoverInfoUpdate;
	
	private World _world;
	
	public WorldSurface(int width, int height, World world) : base(width, height)
	{
		_world = world;
		MouseMove += UpdateInfoBox;
	}

	public void DrawWorld()
	{
		Surface.Clear();
		_world.Render(this);
	}

	private void UpdateInfoBox(object? sender, MouseScreenObjectState e)
	{
		var point = e.CellPosition;
		if (_world.PositionVisible(point))
		{
			var obj = _world.ActorAtPoint(point) ?? (GameObject?)_world.ItemAtPoint(point);
			OnHoverInfoUpdate?.Invoke(point, obj?.GetHoverInfo());
		}
		else
		{
			OnHoverInfoUpdate?.Invoke(point, null);
		}
	}
}