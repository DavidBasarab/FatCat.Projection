using System.Linq.Expressions;

namespace FatCat.Projections.Extensions;

internal static class ExpressionExtensions
{
	internal static string GetMemberName(this Expression expression)
	{
		if (expression == null) throw new ArgumentException("expressionCannotBeNullMessage");

		if (expression is MemberExpression memberExpression)
		{
			// Reference type property or field
			return memberExpression.Member.Name;
		}

		if (expression is MethodCallExpression methodCallExpression)
		{
			// Reference type method
			return methodCallExpression.Method.Name;
		}

		if (expression is UnaryExpression unaryExpression)
		{
			// Property, field of method returning value type
			return GetMemberName(unaryExpression);
		}

		throw new ArgumentException("invalidExpressionMessage");
	}

	private static string GetMemberName(UnaryExpression unaryExpression)
	{
		if (unaryExpression.Operand is MethodCallExpression methodExpression) return methodExpression.Method.Name;

		return ((MemberExpression)unaryExpression.Operand).Member.Name;
	}
}