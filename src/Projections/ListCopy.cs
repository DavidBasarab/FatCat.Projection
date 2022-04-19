using System.Collections;
using FatCat.Toolkit.Console;

namespace FatCat.Projections;

internal class ListCopy
{
	public static IEnumerable? Copy(IEnumerable sourceList)
	{
		var sourceType = sourceList.GetType();

		var destinationType = sourceType.GetGenericArguments()[0];

		return Copy(sourceList, destinationType);
	}

	public static IEnumerable? Copy(IEnumerable sourceList, Type destinationType)
	{
		ConsoleLog.WriteGreen($"Source DestinationType := {destinationType.FullName}");

		var genericListType = typeof(List<>);

		var destinationCombinedType = genericListType.MakeGenericType(destinationType);

		var destinationList = Activator.CreateInstance(destinationCombinedType);

		var addMethod = destinationList?.GetType().GetMethod("Add");

		foreach (var item in sourceList)
		{
			var copyOfItem = Projection.ProjectTo(destinationType, item);

			addMethod?.Invoke(destinationList, new[] { copyOfItem });
		}

		return destinationList as IEnumerable;
	}
}