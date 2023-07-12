namespace Spelunker;

public abstract class BaseLoader<T>
{
	protected abstract int ExpectedColumns { get; }

	private IEnumerable<string[]> GetEntries(string filepath)
	{
		var lines = File.ReadAllLines(filepath);
		var data = lines.Where(line => !line.StartsWith("//") && line.Length > 0)
			.Select(line => line.Split(';')
				.Select(elem => elem.Trim())
				.ToArray())
			.ToArray();

		var numRecords = data.Length;
		var numColumns = ExpectedColumns;

		// Check that all records have the proper number of columns
		for (var i = 0; i < numRecords; i++)
		{
			if (data[i].Length != numColumns)
			{
				throw new InvalidOperationException(
					$"Input record does not have the proper amount of columns: {data[i]}");
			}
		}

		return data;
	}

	protected abstract T Translate(string[] entry);

	public T[] Load(string filepath)
	{
		return GetEntries(filepath).Select(Translate).ToArray();
	}
}