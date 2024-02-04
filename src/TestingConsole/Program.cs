using System.Linq.Expressions;
using FatCat.Fakes;
using FatCat.Projections.Extensions;
using FatCat.Projections.TestingConsole;
using FatCat.Toolkit.Console;

ConsoleLog.Write("Projection Test Console");

try
{
    var sourceItem = Faker.Create<PlayingObject>();

    new PlayingProjection<PlayingDestination, PlayingObject>()
        .ForProperty(i => i.Number, s => s.UseForDude)
        .Project(sourceItem);
}
catch (Exception e)
{
    ConsoleLog.WriteException(e);
}

namespace FatCat.Projections.TestingConsole
{
    public class PlayingStuff<TSource>
    {
        private readonly Func<TSource, object> memberOptions;

        public string MemberName { get; }

        public PlayingStuff(string memberName, Func<TSource, object> memberOptions)
        {
            MemberName = memberName;
            this.memberOptions = memberOptions;
        }

        public object GetValue(TSource source)
        {
            return memberOptions(source);
        }
    }

    public class PlayingProjection<TDest, TSource>
    {
        private PlayingStuff<TSource> dude = null!;

        public PlayingProjection<TDest, TSource> ForProperty<TMember>(
            Expression<Func<TDest, TMember>> selector,
            Func<TSource, TMember> memberOptions
        )
        {
            var propertyName = selector.Body.GetMemberName();

            // overrides.Add(propertyName, memberOptions);
            dude = new PlayingStuff<TSource>(propertyName, s => memberOptions(s)!);

            return this;
        }

        public TDest Project(TSource source)
        {
            var destinationInstance = Activator.CreateInstance<TDest>();

            ConsoleLog.WriteMagenta(
                $"This is where it would project {typeof(TDest).FullName} from <{source!.GetType().FullName}>"
            );

            var dudeValue = dude.GetValue(source);

            ConsoleLog.WriteCyan($"Dude value is {dudeValue} for {dude.MemberName}");

            return default!;
        }
    }
}
