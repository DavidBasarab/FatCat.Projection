using System;
using System.Linq;

namespace FatCat.Projections
{
	public static class Projection
	{
		public static TDestination ProjectTo<TDestination>(object source) where TDestination : class
		{
			var instance = Activator.CreateInstance<TDestination>();

			var destinationType = typeof(TDestination);
			var sourceType = source.GetType();

			var sourceProperties = sourceType.GetProperties();
			var destinationProperties = destinationType.GetProperties();

			foreach (var sourceProperty in sourceProperties)
			{
				var destinationProperty = destinationProperties.FirstOrDefault(i => i.Name == sourceProperty.Name);

				destinationProperty?.SetValue(instance, sourceProperty.GetValue(source));
			}

			return instance;
		}
	}
}