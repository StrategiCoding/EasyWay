﻿using EasyWay.Internals;
using EasyWay.Internals.GuidGenerators;

namespace EasyWay.Tests.Internals.Commands
{
    public sealed class ConcurrencyConflictValidatorCommandTests
    {
        private readonly ConcurrencyConflictValidator _validator = new ConcurrencyConflictValidator();

        internal sealed class TestAggragate : AggregateRoot;

        internal sealed class TestClass : Command, IWithConcurrencyToken
        {
            public short ConcurrencyToken { get; }

            public TestClass(short concurrencyToken)
            {
                ConcurrencyToken = concurrencyToken;
            }
        }

        [Fact(DisplayName = $"When concurrency tokens are different validator should throw {nameof(ConcurrencyException)}")]
        public void WhenConcurrencyTokensAreDifferent()
        {
            // Arrange
            var id = new Guid("56bc4845-b8c4-4c33-a20d-67e14a486f90");

            GuidGenerator.Set(id);

            var aggregateRoot = new TestAggragate();

            var commandWithToken = new TestClass(128);

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

            var commandWithToken = new TestClass(0);

            // Act & Assert
            _validator.Validate(aggregateRoot, commandWithToken);
        }
    }
}
