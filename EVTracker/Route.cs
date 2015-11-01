using System.Collections.Generic;
using System.IO;
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
			var serializer = new DataContractSerializer(typeof(List<Game>));
		    using (var stream = File.Create(location))
		    {
		        serializer.WriteObject(stream, routes);
		        stream.Close();
		    }
		}
		#endregion
	}
}
