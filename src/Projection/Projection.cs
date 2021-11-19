using System;

namespace FatCat
{
	public static class Projection
	{
		public static TDestination? ProjectTo<TDestination>(object source) where TDestination : class
		{
			var instance = Activator.CreateInstance<TDestination>();

			return instance;
		}
	}
}