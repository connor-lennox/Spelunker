namespace Spelunker;

public abstract class PositionalAction : Action
{
	protected readonly Point _targetPoint;

	public PositionalAction(Point targetPoint)
	{
		_targetPoint = targetPoint;
	}
}