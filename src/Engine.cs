using SadConsole.Input;

namespace Spelunker;

public class Engine
{
	public World World;

	private InputHandler _inputHandler = new DefaultInputHandler();
	
	private Actor _player;
	private List<Actor> _actors = new();

	// List of dead actors, used to avoid concurrent modification
	private readonly List<Actor> _dead = new();

	public void LoadWorld(World world)
	{
		World = world;
		_player = world.Player;
		_player.OnDeath += GameOver;
		
		// Fetch all actors from the World and assign the basic death event
		_actors = world.Objects.Where(o => o is Actor).Cast<Actor>().ToList();
		_actors.ForEach(a => a.OnDeath += () => ActorDied(a));
	}

	public bool ReceiveInput(Keyboard keyboard)
	{
		return _inputHandler.HandleInput(this, keyboard);
	}

	public void DoPlayerTurn(Action playerAction)
	{
		playerAction.Execute(_player);
		World.UpdateVisibility();
		foreach (var ally in _actors.Where(ally => ally.Faction == Faction.Player && ally != _player))
		{
			ally.GetAgentAction().Execute(ally);
		}
		CleanupDead();
		
		DoEnemyTurn();
	}
	
	private void DoEnemyTurn()
	{
		foreach (var enemy in _actors.Where(enemy => enemy.Faction == Faction.Enemy))
		{
			enemy.GetAgentAction().Execute(enemy);
		}
		CleanupDead();
	}
	
	private void ActorDied(Actor actor)
	{
		_dead.Add(actor);
	}

	private void CleanupDead()
	{
		foreach (var d in _dead)
		{
			System.Console.WriteLine($"{d.ActorType.Name} has been slain!");
			_actors.Remove(d);
			World.RemoveObject(d);
		}
		
		_dead.Clear();
	}
	
	private void GameOver()
	{
		System.Console.WriteLine("Game Over");
		_inputHandler = new GameOverInputHandler();
	}
}