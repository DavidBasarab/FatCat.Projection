using System.Linq.Expressions;
using FatCat.Projections.Extensions;

namespace FatCat.Projections;

internal class ProjectionOption<TSource>
{
	private readonly string destinationMemberName;
	private readonly Func<TSource, object> optionValueFunction;

	public ProjectionOption(string destinationMemberName, Func<TSource, object> optionValueFunction)
	{
		this.destinationMemberName = destinationMemberName;
		this.optionValueFunction = optionValueFunction;
	}

	public object GetOptionValue(TSource source) => throw new NotImplementedException();
}

public class FluentProjection<TDestination, TSource> where TDestination : class
{
	private List<ProjectionOption<TSource>> ProjectionOptions { get; } = new();

	public FluentProjection<TDestination, TSource> ForProperty<TMember>(Expression<Func<TDestination, TMember>> selector, Func<TSource, TMember> optionValueFunction)
	{
		ProjectionOptions.Add(new ProjectionOption<TSource>(selector.Body.GetMemberName(), s => optionValueFunction(s)));

		return this;
	}

	public TDestination Project(TSource source)
	{
		var processor = new ProjectionProcessor(typeof(TDestination), typeof(TSource));

		return (processor.DoProjection() as TDestination)!;
	}
}