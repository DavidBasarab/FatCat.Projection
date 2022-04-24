using System.Linq.Expressions;
using FatCat.Fakes;
using FatCat.Toolkit.Console;

ConsoleLog.Write("Projection Test Console");

try
{
	var sourceItem = Faker.Create<PlayingObject>();

	new PlayingProjection<PlayingDestination, PlayingObject>()
		.ForProperty(i => i.Number, s => s.UseForDude)
		.Project(sourceItem);
}
catch (Exception e) { ConsoleLog.WriteException(e); }

public class PlayingStuff<TSource>
{
	private readonly Func<TSource, object> memberOptions;

	public string MemberName { get; }

	public PlayingStuff(string memberName, Func<TSource, object> memberOptions)
	{
		MemberName = memberName;
		this.memberOptions = memberOptions;
	}

	public object GetValue(TSource source) => memberOptions(source);
}

public class PlayingProjection<TDest, TSource>
{
	private PlayingStuff<TSource> dude;

	public PlayingProjection<TDest, TSource> ForProperty<TMember>(Expression<Func<TDest, TMember>> selector, Func<TSource, TMember> memberOptions)
	{
		var propertyName = GetMemberName(selector.Body);

		// overrides.Add(propertyName, memberOptions);
		dude = new PlayingStuff<TSource>(propertyName, s => memberOptions(s));

		return this;
	}

	public TDest Project(TSource source)
	{
		var destinationInstance = Activator.CreateInstance<TDest>();

		ConsoleLog.WriteMagenta($"This is where it would project {typeof(TDest).FullName} from <{source.GetType().FullName}>");

		var dudeValue = dude.GetValue(source);

		ConsoleLog.WriteCyan($"Dude value is {dudeValue} for {dude.MemberName}");

		return default!;
	}

	private static string GetMemberName(Expression expression)
	{
		if (expression == null) throw new ArgumentException("expressionCannotBeNullMessage");

		if (expression is MemberExpression)
		{
			// Reference type property or field
			var memberExpression = (MemberExpression)expression;
			return memberExpression.Member.Name;
		}

		if (expression is MethodCallExpression)
		{
			// Reference type method
			var methodCallExpression = (MethodCallExpression)expression;
			return methodCallExpression.Method.Name;
		}

		if (expression is UnaryExpression)
		{
			// Property, field of method returning value type
			var unaryExpression = (UnaryExpression)expression;
			return GetMemberName(unaryExpression);
		}

		throw new ArgumentException("invalidExpressionMessage");
	}

	private static string GetMemberName(UnaryExpression unaryExpression)
	{
		if (unaryExpression.Operand is MethodCallExpression)
		{
			var methodExpression = (MethodCallExpression)unaryExpression.Operand;
			return methodExpression.Method.Name;
		}

		return ((MemberExpression)unaryExpression.Operand).Member.Name;
	}
}