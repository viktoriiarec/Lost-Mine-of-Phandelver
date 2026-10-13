using System;

namespace HeartOfDepths.Core
{
    [Serializable]
    public readonly struct CharacterStats
    {
        public int Might { get; }
        public int Agility { get; }
        public int Intellect { get; }
        public int Will { get; }
        public int Influence { get; }

        public CharacterStats(int might, int agility, int intellect, int will, int influence)
        {
            Might = Validate(might, nameof(might));
            Agility = Validate(agility, nameof(agility));
            Intellect = Validate(intellect, nameof(intellect));
            Will = Validate(will, nameof(will));
            Influence = Validate(influence, nameof(influence));
        }

        private static int Validate(int value, string name)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(name, "A stat cannot be negative.");

            return value;
        }
    }
}
