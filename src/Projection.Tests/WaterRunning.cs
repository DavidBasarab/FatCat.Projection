using FluentAssertions;
using Xunit;

namespace FatCat.Projection.Tests
{
	public class WaterRunning
	{
		[Fact]
		public void SumWillSumTheNumbers()
		{
			var water = new JustToGetTheWaterRunning();

			water.Sum(1, 2)
				.Should()
				.Be(3);
		}
	}
}