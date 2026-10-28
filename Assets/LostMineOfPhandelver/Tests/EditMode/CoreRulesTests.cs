using LostMineOfPhandelver.Core;
using NUnit.Framework;

namespace LostMineOfPhandelver.Tests
{
    public sealed class CoreRulesTests
    {
        [TestCase(1, -5)]
        [TestCase(8, -1)]
        [TestCase(9, -1)]
        [TestCase(10, 0)]
        [TestCase(11, 0)]
        [TestCase(12, 1)]
        [TestCase(20, 5)]
        public void Modifier_UsesD20Formula(int score, int expected)
        {
            Assert.That(CharacterStats.Modifier(score), Is.EqualTo(expected));
        }

        [TestCase(12, 1, 2, 15, true)]
        [TestCase(11, 1, 2, 15, false)]
        public void Resolve_ComparesTotalWithDifficultyClass(int roll, int wisdomModifier, int proficiency, int dc, bool expected)
        {
            int wisdomScore = 10 + wisdomModifier * 2;
            var result = new AbilityCheckResolver(new FixedRoller(roll)).Resolve(wisdomScore, proficiency, dc);
            Assert.That(result.IsSuccess, Is.EqualTo(expected));
        }

        [Test]
        public void SeededD20_IsReproducible()
        {
            var first = new RandomD20Roller(42);
            var second = new RandomD20Roller(42);

            for (int i = 0; i < 10; i++)
                Assert.That(first.Roll(), Is.EqualTo(second.Roll()));
        }

        private sealed class FixedRoller : ID20Roller
        {
            private readonly int _roll;
            public FixedRoller(int roll) => _roll = roll;
            public int Roll() => _roll;
        }
    }
}
