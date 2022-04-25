using System.Linq.Expressions;

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

public class FluentProjection<TDestination, TSource>
{
	public FluentProjection<TDestination, TSource> ForProperty<TMember>(Expression<Func<TDestination, TMember>> selector, Func<TSource, TMember> optionValueFunction) => throw new NotImplementedException();

	public TDestination Project(TSource source) => throw new NotImplementedException();
}