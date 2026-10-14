using System;

namespace LostMineOfPhandelver.Core
{
    public readonly struct CheckResult
    {
        public int Roll { get; }
        public int AbilityModifier { get; }
        public int ProficiencyBonus { get; }
        public int DifficultyClass { get; }
        public int Total => Roll + AbilityModifier + ProficiencyBonus;
        public bool IsSuccess => Total >= DifficultyClass;
        public bool IsNaturalOne => Roll == 1;
        public bool IsNaturalTwenty => Roll == 20;

        public CheckResult(int roll, int abilityModifier, int proficiencyBonus, int difficultyClass)
        {
            Roll = roll;
            AbilityModifier = abilityModifier;
            ProficiencyBonus = proficiencyBonus;
            DifficultyClass = difficultyClass;
        }
    }

    public interface ID20Roller
    {
        int Roll();
    }

    public sealed class RandomD20Roller : ID20Roller
    {
        private readonly Random _random;

        public RandomD20Roller(int? seed = null)
        {
            _random = seed.HasValue ? new Random(seed.Value) : new Random();
        }

        public int Roll() => _random.Next(1, 21);
    }

    public sealed class AbilityCheckResolver
    {
        private readonly ID20Roller _roller;

        public AbilityCheckResolver(ID20Roller roller)
        {
            _roller = roller ?? throw new ArgumentNullException(nameof(roller));
        }

        public CheckResult Resolve(int abilityScore, int proficiencyBonus, int difficultyClass)
        {
            if (proficiencyBonus < 0) throw new ArgumentOutOfRangeException(nameof(proficiencyBonus));
            if (difficultyClass < 0) throw new ArgumentOutOfRangeException(nameof(difficultyClass));

            return new CheckResult(
                _roller.Roll(),
                CharacterStats.Modifier(abilityScore),
                proficiencyBonus,
                difficultyClass);
        }
    }
}
