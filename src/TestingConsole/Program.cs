using FatCat.Fakes;
using FatCat.Projections.Tests.Objects.OneLevelComplexItems;

namespace FatCat.Projections.TestingConsole;

public class Program
{
	public static void Main(params string[] args)
	{
		Console.WriteLine("Projection Test Console");

		try
		{
			var source = Faker.Create<OneLevelSource>();

			var result = Projection.ProjectTo<OneLevelDestination>(source);
		}
		catch (Exception e) { Console.WriteLine(e); }
	}
}