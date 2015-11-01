using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace EVTracker
{
	[DataContract]
	public class Nature
	{
		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public Stat Bonus { get; set; }
		[DataMember]
		public Stat Penalty { get; set; }
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
