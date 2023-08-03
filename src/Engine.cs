using SadConsole.Input;

namespace Spelunker;

public class Engine
{
	public event System.Action? RequestHistoryPane;
	
	public World World;

	private readonly Stack<InputHandler> _inputStack = new();
	private InputHandler InputHandler => _inputStack.Peek();

	public Actor Player;
	private List<Actor> _actors = new();

	// List of dead actors, used to avoid concurrent modification
	private readonly List<Actor> _dead = new();

	public Engine()
	{
		PushInputHandler(new DefaultInputHandler());
	}

	public void LoadWorld(World world)
	{
		World = world;
		Player = world.Player;
		
		// Fetch all actors from the World and assign the basic death event
		_actors = world.Actors;
		_actors.ForEach(a => a.OnDeath += () => ActorDied(a));
		
		Player.OnDeath += GameOver;
	}

	public bool ReceiveInput(Keyboard keyboard)
	{
		return InputHandler.HandleInput(this, keyboard);
	}

	public void DoPlayerTurn(Action playerAction)
	{
		if (!playerAction.Execute(Player))
		{
			return;
		}
		World.UpdateVisibility();
		foreach (var ally in _actors.Where(ally => ally.Faction == Faction.Player && ally != Player))
		{
			ally.GetAgentAction().Execute(ally);
		}
		CleanupDead();
		
		DoEnemyTurn();
		
		GameTime.NextTurn();
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
			// It's a weird edge case, but the enemy might have been revived.
			if (d.Alive) continue;
			
			Logger.Log($"{d.ActorType.Name} has been slain!");
			_actors.Remove(d);
			World.RemoveActor(d);
		}
		
		_dead.Clear();
	}
	
	private void GameOver()
	{
		Logger.Log("Game Over");
		// Force input stack to just be the game over handler
		_inputStack.Clear();
		_inputStack.Push(new GameOverInputHandler());
	}

	public void PushInputHandler(InputHandler handler)
	{
		_inputStack.Push(handler);
	}

	public InputHandler PopInputHandler()
	{
		return _inputStack.Pop();
	}

	public void OpenHistory()
	{
		RequestHistoryPane?.Invoke();
	}

	public void StartPlayerUseItem()
	{
		var item = Player.Inventory.HeldItem;
		if (item == null) return;
		
		switch (item.ItemType.TargetingMode)
		{
			case TargetingMode.Self:
				DoPlayerTurn(new UseItemAction(Player.Position, item));
				break;
			case TargetingMode.Tile:
				var gridSelector = new GridSelector(Player.Position, item.ItemType.Range, World);
				PushInputHandler(new UseItemInputHandler(gridSelector, item));
				World.StartSelection(gridSelector);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}
	}

	public void FinishPlayerUseItem()
	{
		// Pop use item input handler
		PopInputHandler();
		
		// Disable visuals
		World.FinishSelection();
	}
}