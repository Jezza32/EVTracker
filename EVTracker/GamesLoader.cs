using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using EVTracker.Properties;

namespace EVTracker
{
    public class GamesLoader
    {
        public IEnumerable<Game> Load()
        {
            var deserializer = new DataContractSerializer(typeof(List<Game>));
            using (var stream = new MemoryStream(Resources.Games))
            {
                return (List<Game>)deserializer.ReadObject(stream);
            }
        }
    }
}