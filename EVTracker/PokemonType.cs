using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.IO;

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
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<PokemonType>));
			Stream s = File.Create(location);
			serializer.WriteObject(s, pokemon);
			s.Close();
		}
		#endregion

		public override string ToString()
		{
			return string.Format("#{0} - {1}", DexNumber.ToString().PadLeft(3, '0'), Name);
		}
	}
}