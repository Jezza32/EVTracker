using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace EVTracker
{
	[DataContract]
	public class Nature
	{
	    protected bool Equals(Nature other)
	    {
	        return string.Equals(Name, other.Name) && Bonus == other.Bonus && Penalty == other.Penalty;
	    }

	    public override bool Equals(object obj)
	    {
	        if (ReferenceEquals(null, obj)) return false;
	        if (ReferenceEquals(this, obj)) return true;
	        if (obj.GetType() != this.GetType()) return false;
	        return Equals((Nature) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            var hashCode = Name?.GetHashCode() ?? 0;
	            hashCode = (hashCode*397) ^ (int) Bonus;
	            hashCode = (hashCode*397) ^ (int) Penalty;
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

	    [DataMember]
		public string Name { get; private set; }
		[DataMember]
		public Stat Bonus { get; private  set; }
		[DataMember]
		public Stat Penalty { get; private  set; }
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

	    public static Nature Hardy => new Nature {Bonus = Stat.Attack, Penalty = Stat.Attack, Name = "Hardy"};

	    #region Serializable
		public static void Serialize(string location, List<Nature> natures)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<Nature>));
			Stream s= File.Create(location);
			serializer.WriteObject(s, natures);
			s.Close();
		}
		#endregion
	}
}
