using System.Linq.Expressions;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

internal class ProjectionOption<TSource>
{
	private readonly Func<TSource, object> optionValueFunction;

	public string DestinationMemberName { get; }

	public ProjectionOption(string destinationMemberName, Func<TSource, object> optionValueFunction)
	{
		DestinationMemberName = destinationMemberName;
		this.optionValueFunction = optionValueFunction;
	}

	public object GetOptionValue(TSource source) => throw new NotImplementedException();
}

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
		var processor = new ProjectionProcessor(typeof(TDestination), source, null, DoCustomPropertyValue);

		return (processor.DoProjection() as TDestination)!;
	}

	private object DoCustomPropertyValue(string propertyName, object sourceItem)
	{
		var propertyOptions = ProjectionOptions.FirstOrDefault(i => i.DestinationMemberName == propertyName);

		return propertyOptions?.GetOptionValue((sourceItem as TSource)!)!;
	}
}