namespace Spelunker;

public class Actor : GameObject
{
	private ActorType _actorType;
	
	public Actor(ActorType actorType)
	{
		_actorType = actorType;
	}

	public override ColoredGlyph Glyph => _actorType.Glyph;

	public bool ExecuteAction(Action action)
	{
		return action.Execute(this);
	}
}