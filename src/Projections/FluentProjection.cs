using System.Linq.Expressions;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

public class FluentProjection<TDestination, TSource> where TDestination : class where TSource : class
{
	private List<ProjectionOption<TSource>> ProjectionOptions { get; } = new();

	public FluentProjection<TDestination, TSource> ForProperty<TMember>(Expression<Func<TDestination, TMember>> selector, Func<TSource, TMember> optionValueFunction)
	{
		ProjectionOptions.Add(new ProjectionOption<TSource>(selector.Body.GetMemberName(), s => optionValueFunction(s)));

		return this;
	}

	public TDestination Project(TSource source)
	{
		var processor = new ProjectionProcessor(typeof(TDestination), source, Activator.CreateInstance<TDestination>(), DoCustomPropertyValue);

		return (processor.DoProjection() as TDestination)!;
	}

	private OverridePropertyValueResult DoCustomPropertyValue(string propertyName, object sourceItem)
	{
		var propertyOptions = ProjectionOptions.FirstOrDefault(i => i.DestinationMemberName == propertyName);

		return propertyOptions != null ? new OverridePropertyValueResult(true, propertyOptions.GetOptionValue((sourceItem as TSource)!)) : new OverridePropertyValueResult(false);
	}
}