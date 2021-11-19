using System;
using System.Linq;

namespace FatCat.Projections
{
	public static class Projection
	{
		public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
		{
			var destinationType = typeof(TDestination);

			return (ProjectTo(destinationType, source) as TDestination)!;
		}

		private static object ProjectTo(Type destinationType, object source)
		{
			var instance = Activator.CreateInstance(destinationType);

			var sourceType = source.GetType();

			var sourceProperties = sourceType.GetProperties();
			var destinationProperties = destinationType.GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var destinationProperty = destinationProperties.FirstOrDefault(i => i.Name == sourceProperty.Name);

				if (destinationProperty == null) continue;

				destinationProperty?.SetValue(instance, sourceProperty.GetValue(source));
			}

			return instance!;
		}
	}
}