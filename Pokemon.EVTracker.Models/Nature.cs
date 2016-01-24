namespace Pokemon.EVTracker.Models
{
    public class Nature
    {
        public Nature(string name, Stat bonus, Stat penalty)
        {
            Name = name;
            Bonus = bonus;
            Penalty = penalty;
        }

        protected bool Equals(Nature other)
        {
            return string.Equals(Name, other.Name) && Bonus == other.Bonus && Penalty == other.Penalty;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Nature)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Name?.GetHashCode() ?? 0;
                hashCode = (hashCode * 397) ^ (int)Bonus;
                hashCode = (hashCode * 397) ^ (int)Penalty;
                return hashCode;
            }
        }

        public static bool operator ==(Nature left, Nature right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Nature left, Nature right)
        {
            return !Equals(left, right);
        }
        
        public string Name { get; }
        public Stat Bonus { get; }
        public Stat Penalty { get; }

        public double GetModifier(Stat s)
        {
            if (Bonus == Penalty) return 1;
            if (s == Bonus) return 1.1;
            if (s == Penalty) return 0.9;
            return 1;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}