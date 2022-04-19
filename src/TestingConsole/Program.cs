using FatCat.Fakes;
using FatCat.Projections;
using FatCat.Toolkit.Console;

for (var i = 0; i < 4; i++) ConsoleLog.WriteEmptyLine();

ConsoleLog.Write("Projection Test Console");

try
{
	var sourceList = Faker.Create<List<PlayingObject>>();

	var copyList = ListCopy.Copy(sourceList);

	var referenceEquals = ReferenceEquals(sourceList, copyList);

	ConsoleLog.WriteCyan($"Does the list reference equal | <{referenceEquals}>");

	var strongList = copyList as List<PlayingObject>;
}
catch (Exception e) { ConsoleLog.WriteException(e); }