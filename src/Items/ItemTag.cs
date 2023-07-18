namespace Spelunker;

public abstract class ItemTag
{
	private static readonly Dictionary<Type, ItemTag> Flyweights = new();
	public static T GetTag<T>() where T : ItemTag, new()
	{
		if (!Flyweights.ContainsKey(typeof(T)))
		{
			Flyweights.Add(typeof(T), new T());
		}

		return (T)Flyweights[typeof(T)];
	}
	
	public virtual void OnAttack(Actor instigator, Item item, Actor target) {}
	public virtual void OnHitWhileHolding(Actor holder, Item item, Actor instigator) {}
	public virtual void OnUse(Actor holder, Item item) {}
	public virtual void OnPickup(Actor holder, Item item) {}
}