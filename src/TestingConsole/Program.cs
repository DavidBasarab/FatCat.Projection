using FatCat.Fakes;
using FatCat.Projections.Tests.Objects;
using Newtonsoft.Json;

namespace FatCat.Projections.TestingConsole;

public class Program
{
	public static void Main(params string[] args)
	{
		Console.WriteLine("Projection Test Console");

		try
		{
			var source = Faker.Create<MultiLevelObjectSource>();

			var result = Projection.ProjectTo<MultiLevelObjectDestination>(source);

			Console.WriteLine(string.Empty);
			Console.WriteLine($"{new string('-', 100)}");
			Console.WriteLine(string.Empty);

			Console.WriteLine($"{JsonConvert.SerializeObject(source, Formatting.Indented)}");

			Console.WriteLine(string.Empty);
			Console.WriteLine($"{new string('-', 100)}");
			Console.WriteLine(string.Empty);

			Console.WriteLine($"{JsonConvert.SerializeObject(result, Formatting.Indented)}");
		}
		catch (Exception e) { Console.WriteLine(e); }
	}
}