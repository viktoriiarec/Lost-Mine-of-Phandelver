using System;

namespace LostMineOfPhandelver.Core
{
    [Serializable]
    public readonly struct CharacterStats
    {
        public int Strength { get; }
        public int Dexterity { get; }
        public int Constitution { get; }
        public int Intelligence { get; }
        public int Wisdom { get; }
        public int Charisma { get; }

        public CharacterStats(int strength, int dexterity, int constitution, int intelligence, int wisdom, int charisma)
        {
            Strength = Validate(strength, nameof(strength));
            Dexterity = Validate(dexterity, nameof(dexterity));
            Constitution = Validate(constitution, nameof(constitution));
            Intelligence = Validate(intelligence, nameof(intelligence));
            Wisdom = Validate(wisdom, nameof(wisdom));
            Charisma = Validate(charisma, nameof(charisma));
        }

        public static int Modifier(int score)
        {
            Validate(score, nameof(score));
            return (int)Math.Floor((score - 10) / 2d);
        }

        private static int Validate(int value, string name)
        {
            if (value < 1 || value > 30)
                throw new ArgumentOutOfRangeException(name, "An ability score must be between 1 and 30.");

            return value;
        }
    }
}
