using System.Collections.Generic;
using HeartOfDepths.Core;
using NUnit.Framework;

namespace HeartOfDepths.Tests
{
    public sealed class CoreRulesTests
    {
        [TestCase(6, 6, 0, CheckOutcome.FullSuccess)]
        [TestCase(4, 5, 0, CheckOutcome.SuccessWithCost)]
        [TestCase(3, 3, 0, CheckOutcome.FailureWithProgress)]
        [TestCase(1, 4, 0, CheckOutcome.CriticalFailure)]
        public void Resolve_UsesDocumentedThresholds(int first, int second, int modifier, CheckOutcome expected)
        {
            var dice = new SequenceDice(first, second);
            var result = new CheckResolver(dice).Resolve(modifier, 0, 0);
            Assert.That(result.Outcome, Is.EqualTo(expected));
        }

        [Test]
        public void Integrity_StaysInsideZeroToOneHundred()
        {
            var integrity = new Integrity(100);
            Assert.That(integrity.Spend(150), Is.EqualTo(100));
            Assert.That(integrity.Value, Is.Zero);
            Assert.That(integrity.Restore(150), Is.EqualTo(100));
            Assert.That(integrity.Value, Is.EqualTo(100));
        }

        [Test]
        public void SeededRoller_IsReproducible()
        {
            var first = new RandomDiceRoller(42);
            var second = new RandomDiceRoller(42);

            for (int i = 0; i < 10; i++)
                Assert.That(first.RollD6(), Is.EqualTo(second.RollD6()));
        }

        private sealed class SequenceDice : IDiceRoller
        {
            private readonly Queue<int> _values;

            public SequenceDice(params int[] values)
            {
                _values = new Queue<int>(values);
            }

            public int RollD6() => _values.Dequeue();
        }
    }
}
