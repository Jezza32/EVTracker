using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace EVTracker
{
	[DataContract]
	public class Route
	{
		public Route()
		{
			Pokemon = new List<int>();
		}

		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public IList<int> Pokemon { get; set; }

		public override string ToString()
		{
			return Name;
		}


		#region Serializable
		public static void Serialize(string location, List<Route> routes)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<Game>));
			Stream s = File.Create(location);
			serializer.WriteObject(s, routes);
			s.Close();
		}
		#endregion
	}
}
