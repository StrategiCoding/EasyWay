using EasyWay.Internals.GuidGenerators;
using EasyWay.Internals;
using EasyWay.Internals.Commands.ConcurrencyConflict;

namespace EasyWay.Tests.Internals.Commands
{
    public sealed class ConcurrencyConflictValidatorCommandWithResultTests
    {
        private readonly ConcurrencyConflictValidator _validator = new ConcurrencyConflictValidator();

        internal sealed class TestAggragate : AggregateRoot;

        internal sealed class CommandResult : OperationResult;

        internal sealed class TestClassWithResult : Command<CommandResult>, IWithConcurrencyToken
        {
            public short ConcurrencyToken { get; }

            public TestClassWithResult(short concurrencyToken)
            {
                ConcurrencyToken = concurrencyToken;
            }
        }

        internal sealed class TestClassWithoutConcurrencyToken : Command<CommandResult>;

        [Fact(DisplayName = $"When concurrency tokens are different validator should throw {nameof(ConcurrencyException)}")]
        public void WhenConcurrencyTokensAreDifferent()
        {
            // Arrange
            var id = new Guid("56bc4845-b8c4-4c33-a20d-67e14a486f90");

            GuidGenerator.Set(id);

            var aggregateRoot = new TestAggragate();

            var commandWithToken = new TestClassWithResult(128);

            var expectedMessage = $"{typeof(TestAggragate).Name} with id {id} has different concurrency token ({aggregateRoot.ConcurrencyToken}) that command ({commandWithToken.ConcurrencyToken})";

            // Assert
            var ex = Assert.Throws<ConcurrencyException>(() =>
            {
                // Act
                _validator.Validate(aggregateRoot, commandWithToken);
            });

            Assert.Equal(expectedMessage, ex.Message);

            GuidGenerator.Reset();
        }

        [Fact(DisplayName = $"When concurrency tokens are the same validator should not throw exception")]
        public void WhenConcurrencyTokensAreTheSame()
        {
            // Arrange
            var aggregateRoot = new TestAggragate();

            var commandWithToken = new TestClassWithResult(0);

            // Act & Assert
            _validator.Validate(aggregateRoot, commandWithToken);
        }

        [Fact(DisplayName = $"When command with result don't implement {nameof(IWithConcurrencyToken)} interface")]
        public void WhenCommandIsWithoutConcurrencyToken()
        {
            // Arrange
            var aggregateRoot = new TestAggragate();

            var commandWithToken = new TestClassWithoutConcurrencyToken();

            // Act & Assert

            // Assert
            var ex = Assert.Throws<CommandWihtoutConcurrencyTokenException>(() =>
            {
                // Act
                _validator.Validate(aggregateRoot, commandWithToken);
            });

            string expectedMessage = $"Add {nameof(IWithConcurrencyToken)} interface to {nameof(TestClassWithoutConcurrencyToken)}";

            Assert.Equal(expectedMessage, ex.Message);
        }
    }
}
