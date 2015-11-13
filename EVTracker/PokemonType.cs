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
		public int DexNumber { get; set; }
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public IDictionary<Stat, int> GivenEffortValues { get; set; }
		[DataMember]
		public IDictionary<Stat, int> BaseStats { get; set; }

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
	}
}