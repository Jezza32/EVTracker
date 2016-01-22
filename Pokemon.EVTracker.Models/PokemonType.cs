using System.Collections.Generic;

namespace Pokemon.EVTracker.Models
{
    public class PokemonType
    {
        public int DexNumber { get; }
        public string Name { get; }
        public IDictionary<Stat, int> GivenEffortValues { get; }
        public IDictionary<Stat, int> BaseStats { get; }

        public PokemonType(int dexNumber, string name, IDictionary<Stat, int> baseStats, IDictionary<Stat, int> givenEffortValues)
        {
            DexNumber = dexNumber;
            Name = name;
            GivenEffortValues = givenEffortValues;
            BaseStats = baseStats;
        }

        public override string ToString()
        {
            return $"#{DexNumber.ToString().PadLeft(3, '0')} - {Name}";
        }

        protected bool Equals(PokemonType other)
        {
            return DexNumber == other.DexNumber && string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PokemonType)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (DexNumber * 397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(PokemonType left, PokemonType right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PokemonType left, PokemonType right)
        {
            return !Equals(left, right);
        }
    }
}