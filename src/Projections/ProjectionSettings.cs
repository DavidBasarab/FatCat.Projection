namespace FatCat.Projections;

[Flags]
public enum ProjectionSettings
{
    None = 0,
    DoNotProjectNull = 1,
    SkipCustomProjector = 2
}
