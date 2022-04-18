using FatCat.Fakes;
using FatCat.Projections;
using FatCat.Projections.Tests.Objects.SimpleItems.ItemsWithLists;
using FatCat.Toolkit.Console;
using Newtonsoft.Json;

ConsoleLog.Write("Projection Test Console");

try
{
	var source = Faker.Create<SourceItemWithList>();

	var result = Projection.ProjectTo<DestinationItemWithList>(source);

	ConsoleLog.Write(string.Empty);

	ConsoleLog.Write(string.Empty);
	ConsoleLog.Write($"{new string('-', 100)}");
	ConsoleLog.Write(string.Empty);

	ConsoleLog.Write($"{JsonConvert.SerializeObject(source, Formatting.Indented)}");

	ConsoleLog.Write(string.Empty);
	ConsoleLog.Write($"{new string('-', 100)}");
	ConsoleLog.Write(string.Empty);

	ConsoleLog.Write($"{JsonConvert.SerializeObject(result, Formatting.Indented)}");
}
catch (Exception e) { ConsoleLog.WriteException(e); }