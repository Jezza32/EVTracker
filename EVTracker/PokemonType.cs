using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace EVTracker
{
	[DataContract]
	public class PokemonType
	{
		[DataMember]
		public int DexNumber { get; private set; }
		[DataMember]
		public string Name { get; private set; }
		[DataMember]
		public IDictionary<Stat, int> GivenEffortValues { get; private set; }
		[DataMember]
		public IDictionary<Stat, int> BaseStats { get; private set; }

		public PokemonType()
		{
			DexNumber = 0;
			Name = "MissingNo";
			GivenEffortValues = new Dictionary<Stat, int>();
			BaseStats = new Dictionary<Stat, int>();
			foreach (Stat s in Enum.GetValues(typeof(Stat)))
				BaseStats.Add(s, 80);
		}

		#region Serializable
		public static void Serialize(string location, List<PokemonType> pokemon)
		{
			var serializer = new DataContractSerializer(typeof(List<PokemonType>));
		    using (var stream = File.Create(location))
		    {
		        serializer.WriteObject(stream, pokemon);
		        stream.Close();
		    }
		}
		#endregion

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
	        return Equals((PokemonType) obj);
	    }

	    public override int GetHashCode()
	    {
	        unchecked
	        {
	            return (DexNumber*397) ^ (Name?.GetHashCode() ?? 0);
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