using EasyWay.Internals.Clocks;

namespace EasyWay.Tests.Clocks
{
    public sealed class ClockTests
    {
        [Fact(DisplayName = $"{nameof(Clock)} should return date time UTC now")]
        public void ClockShouldReturnUtcNow()
        {
            // Arrange
            var expectedUtcNow = DateTime.UtcNow;
            TimeSpan precision = TimeSpan.FromMilliseconds(5);

            // Act
            var utcNow = Clock.UtcNow;

            // Assert
            Assert.Equal(DateTimeKind.Utc, utcNow.Kind);
            Assert.Equal(expectedUtcNow, utcNow, precision);
        }

        [Fact(DisplayName = $"{nameof(Clock)} should throw exception when set date time is not UTC")]
        public void ClockShouldThrowExceptionWhenSetDateTimeIsNotUtc()
        {
            // Arrange
            var unspecifiedDateTime = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

            // Assert
            Assert.Throws<CustomDateTimeMustBeUtcException>(() =>
            {
                // Act
                Clock.Set(unspecifiedDateTime);
            });
        }

        [Fact(DisplayName = $"{nameof(Clock)} should return the expected date time when {nameof(Clock.Set)}")]
        public void ClockShouldReturnExceptedDateTimeWhenSet()
        {
            // Arrange
            var expectedUtc = new DateTime(2024,1,1,0,0,0, DateTimeKind.Utc);
            TimeSpan precision = TimeSpan.FromMilliseconds(5);

            Clock.Set(expectedUtc);

            // Act
            var utcNow = Clock.UtcNow;

            // Assert
            Assert.Equal(DateTimeKind.Utc, utcNow.Kind);
            Assert.True(expectedUtc != utcNow);
            Assert.Equal(expectedUtc, utcNow, precision);

            Clock.Reset();
        }

        [Fact(DisplayName = $"{nameof(Clock)} should return the current UTC date time when {nameof(Clock.Set)} and then {nameof(Clock.Reset)}")]
        public void ClockShouldReturnExceptedDate()
        {
            // Arrange
            var expectedUtc = DateTime.UtcNow;

            TimeSpan precision = TimeSpan.FromMilliseconds(5);

            Clock.Set(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));

            // Act
            Clock.Reset();

            // Assert
            Assert.Equal(DateTimeKind.Utc, Clock.UtcNow.Kind);
            Assert.Equal(expectedUtc, Clock.UtcNow, precision);
        }
    }
}
