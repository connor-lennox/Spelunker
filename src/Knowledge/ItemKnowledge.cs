namespace Spelunker;

[Flags]
public enum KnowledgeType
{
	Description = 1,
	MeleeDamage = 2,
	Value = 4,
	Effects = 8,
	Tags = 16
}

public class ItemKnowledge
{
	private KnowledgeType _knowledge;
	
	public ItemKnowledge() : this(0) {}

	public ItemKnowledge(int state)
	{
		_knowledge = (KnowledgeType)state;
	}

	public bool HasKnowledge(KnowledgeType type)
	{
		return (_knowledge & type) > 0;
	}

	public void AddKnowledge(KnowledgeType type)
	{
		_knowledge |= type;
	}

	public int ToInt() => (int)_knowledge;
}