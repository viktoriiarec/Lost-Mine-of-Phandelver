using System;

namespace HeartOfDepths.Core
{
    public enum CheckOutcome
    {
        CriticalFailure,
        FailureWithProgress,
        SuccessWithCost,
        FullSuccess
    }

    public readonly struct CheckResult
    {
        public int FirstDie { get; }
        public int SecondDie { get; }
        public int Modifier { get; }
        public int Total => FirstDie + SecondDie + Modifier;
        public CheckOutcome Outcome { get; }

        public CheckResult(int firstDie, int secondDie, int modifier, CheckOutcome outcome)
        {
            FirstDie = firstDie;
            SecondDie = secondDie;
            Modifier = modifier;
            Outcome = outcome;
        }
    }

    public interface IDiceRoller
    {
        int RollD6();
    }

    public sealed class RandomDiceRoller : IDiceRoller
    {
        private readonly Random _random;

        public RandomDiceRoller(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public int RollD6() => _random.Next(1, 7);
    }

    public sealed class CheckResolver
    {
        private readonly IDiceRoller _dice;

        public CheckResolver(IDiceRoller dice)
        {
            _dice = dice ?? throw new ArgumentNullException(nameof(dice));
        }

        public CheckResult Resolve(int stat, int skill, int difficulty)
        {
            if (stat < 0) throw new ArgumentOutOfRangeException(nameof(stat));
            if (skill < 0) throw new ArgumentOutOfRangeException(nameof(skill));

            int first = _dice.RollD6();
            int second = _dice.RollD6();
            int modifier = stat + skill - difficulty;
            int total = first + second + modifier;

            CheckOutcome outcome = total switch
            {
                >= 12 => CheckOutcome.FullSuccess,
                >= 9 => CheckOutcome.SuccessWithCost,
                >= 6 => CheckOutcome.FailureWithProgress,
                _ => CheckOutcome.CriticalFailure
            };

            return new CheckResult(first, second, modifier, outcome);
        }
    }
}
