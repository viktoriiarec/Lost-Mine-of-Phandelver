using System;

namespace HeartOfDepths.Core
{
    [Serializable]
    public sealed class Integrity
    {
        public const int Min = 0;
        public const int Max = 100;

        public int Value { get; private set; }

        public Integrity(int initialValue = Max)
        {
            Value = Clamp(initialValue);
        }

        public int Spend(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            int spent = Math.Min(Value, amount);
            Value -= spent;
            return spent;
        }

        public int Restore(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));

            int before = Value;
            Value = Clamp(Value + amount);
            return Value - before;
        }

        private static int Clamp(int value) => Math.Max(Min, Math.Min(Max, value));
    }
}
