using System.Collections;
using System.Reflection;

namespace FatCat.Projections.Tests.Assertions;

/// <summary>
/// FatCat.Testing's BeEquivalentTo requires both sides to be the same type. Projections map between
/// different types, so this compares structurally by matching public properties by name: every readable
/// property on <c>expected</c> must exist on <c>subject</c> with an equivalent value. Collections are
/// compared element by element, in order.
/// </summary>
public static class CrossTypeEquivalency
{
    public static void ShouldBeEquivalentTo(this object subject, object expected)
    {
        var difference = FindDifference(subject, expected, "root");

        if (difference != null)
        {
            Assert.Fail(difference);
        }
    }

    private static string FindDifference(object subject, object expected, string path)
    {
        if (subject == null && expected == null)
        {
            return null;
        }

        if (subject == null || expected == null)
        {
            return $"{path}: expected {Describe(expected)} but found {Describe(subject)}";
        }

        var expectedType = expected.GetType();

        if (IsValueLike(expectedType))
        {
            return Equals(subject, expected)
                ? null
                : $"{path}: expected {Describe(expected)} but found {Describe(subject)}";
        }

        if (expected is IEnumerable expectedItems)
        {
            if (subject is not IEnumerable subjectItems)
            {
                return $"{path}: expected a collection but found {subject.GetType().Name}";
            }

            return FindCollectionDifference(subjectItems, expectedItems, path);
        }

        foreach (var expectedProperty in ReadableProperties(expectedType))
        {
            var subjectProperty = ReadableProperties(subject.GetType())
                .FirstOrDefault(property => property.Name == expectedProperty.Name);

            if (subjectProperty == null)
            {
                return $"{path}.{expectedProperty.Name}: missing on {subject.GetType().Name}";
            }

            var difference = FindDifference(
                subjectProperty.GetValue(subject),
                expectedProperty.GetValue(expected),
                $"{path}.{expectedProperty.Name}"
            );

            if (difference != null)
            {
                return difference;
            }
        }

        return null;
    }

    private static string FindCollectionDifference(IEnumerable subject, IEnumerable expected, string path)
    {
        var subjectList = subject.Cast<object>().ToList();
        var expectedList = expected.Cast<object>().ToList();

        if (subjectList.Count != expectedList.Count)
        {
            return $"{path}: expected {expectedList.Count} items but found {subjectList.Count}";
        }

        for (var index = 0; index < expectedList.Count; index++)
        {
            var difference = FindDifference(subjectList[index], expectedList[index], $"{path}[{index}]");

            if (difference != null)
            {
                return difference;
            }
        }

        return null;
    }

    private static bool IsValueLike(Type type)
    {
        return type.IsPrimitive
            || type.IsEnum
            || type == typeof(string)
            || type == typeof(decimal)
            || type == typeof(DateTime)
            || type == typeof(DateTimeOffset)
            || type == typeof(TimeSpan)
            || type == typeof(Guid);
    }

    private static IEnumerable<PropertyInfo> ReadableProperties(Type type)
    {
        return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(property => property.CanRead && property.GetIndexParameters().Length == 0);
    }

    private static string Describe(object value)
    {
        return value switch
        {
            null => "null",
            string text => $"\"{text}\"",
            _ => value.ToString(),
        };
    }
}
