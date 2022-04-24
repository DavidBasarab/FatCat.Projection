using FatCat.Fakes;
using FatCat.Projections;
using FatCat.Toolkit.Console;
using Newtonsoft.Json;

for (var i = 0; i < 4; i++) ConsoleLog.WriteEmptyLine();

ConsoleLog.Write("Projection Test Console");

try
{
	var sourceList = Faker.Create<List<PlayingObject>>();

	var copyList = ListCopy.Copy(sourceList);

	var referenceEquals = ReferenceEquals(sourceList, copyList);

	ConsoleLog.WriteCyan($"Does the list reference equal | <{referenceEquals}>");

	var json = JsonConvert.SerializeObject(copyList, Formatting.Indented);

	ConsoleLog.WriteMagenta(json);

	var strongList = copyList as List<PlayingObject>;
}
catch (Exception e) { ConsoleLog.WriteException(e); }