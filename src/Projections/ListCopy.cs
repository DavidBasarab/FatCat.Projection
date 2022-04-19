using System.Collections;

namespace FatCat.Projections;

internal class ListCopy
{
	public static IEnumerable Copy(IEnumerable sourceList)
	{
		var sourceType = sourceList.GetType();

		var destinationType = sourceType.GetGenericArguments()[0];


		return null;
	}
}