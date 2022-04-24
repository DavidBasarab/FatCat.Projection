using System.Linq.Expressions;
using FatCat.Fakes;
using FatCat.Toolkit.Console;

ConsoleLog.Write("Projection Test Console");

try
{
	var sourceItem = Faker.Create<PlayingObject>();

	new PlayingProjection<PlayingDestination, PlayingObject>()
		.ForProperty(i => i.Number, s => 17)
		.Project(sourceItem);
}
catch (Exception e) { ConsoleLog.WriteException(e); }

public class PlayingProjection<TDest, TSource>
{
	private Dictionary<string, Func<TSource, object>> overrides = new Dictionary<string, Func<TSource, object>>();

	public PlayingProjection<TDest, TSource> ForProperty<TMember>(Expression<Func<TDest, TMember>> selector, Func<TSource, TMember> memberOptions)
	{
		var propertyName = GetMemberName(selector.Body);

		var value = memberOptions(default!);
		
		ConsoleLog.WriteMagenta($"So the Property NAME is!!!!!! <{propertyName}> | Value is <{value}>");

		// overrides.Add(propertyName, memberOptions);
		
		return this;
	}

	public TDest Project(object source)
	{
		var destinationInstance = Activator.CreateInstance<TDest>();

		ConsoleLog.WriteMagenta($"This is where it would project {typeof(TDest).FullName} from <{source.GetType().FullName}>");

		return default!;
	}
	
	private static string GetMemberName(Expression expression)
	{
		if (expression == null)
		{
			throw new ArgumentException("expressionCannotBeNullMessage");
		}

		if (expression is MemberExpression)
		{
			// Reference type property or field
			var memberExpression = (MemberExpression) expression;
			return memberExpression.Member.Name;
		}

		if (expression is MethodCallExpression)
		{
			// Reference type method
			var methodCallExpression = (MethodCallExpression) expression;
			return methodCallExpression.Method.Name;
		}

		if (expression is UnaryExpression)
		{
			// Property, field of method returning value type
			var unaryExpression = (UnaryExpression) expression;
			return GetMemberName(unaryExpression);
		}

		throw new ArgumentException("invalidExpressionMessage");
	}

	private static string GetMemberName(UnaryExpression unaryExpression)
	{
		if (unaryExpression.Operand is MethodCallExpression)
		{
			var methodExpression = (MethodCallExpression) unaryExpression.Operand;
			return methodExpression.Method.Name;
		}

		return ((MemberExpression) unaryExpression.Operand).Member.Name;
	}
}