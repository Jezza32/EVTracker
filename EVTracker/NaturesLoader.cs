using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using EVTracker.Properties;

namespace EVTracker
{
    public class NaturesLoader
    {
        public IEnumerable<Nature> Load()
        {
            var deserializer = new DataContractSerializer(typeof(List<Nature>));
            using (var stream = new MemoryStream(Resources.Natures))
            {
                return (List<Nature>)deserializer.ReadObject(stream);
            }
        }
    }
}