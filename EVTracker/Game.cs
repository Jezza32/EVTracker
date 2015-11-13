using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace EVTracker
{
	[DataContract]
	public class Game
	{
		public Game()
		{
			Routes = new List<Route>();
		}

		[DataMember]
		public string Name { get; set; }
		[DataMember]
		public IList<Route> Routes { get; set; }

		public override string ToString()
		{
			return Name;
		}


		#region Serializable
		public static void Serialize(string location, List<Game> games)
		{
			var serializer = new DataContractSerializer(typeof(List<Game>));
		    using (var stream = File.Create(location))
		    {
		        serializer.WriteObject(stream, games);
		        stream.Close();
		    }
		}
		#endregion
	}
}
