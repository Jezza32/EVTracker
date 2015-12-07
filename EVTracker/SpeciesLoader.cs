using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using EVTracker.Properties;

namespace EVTracker
{
    public class SpeciesLoader
    {
        public IEnumerable<PokemonType> Load()
        {
            var deserializer = new DataContractSerializer(typeof(List<PokemonType>));
            using (var stream = new MemoryStream(Resources.Species))
            {
                return (List<PokemonType>)deserializer.ReadObject(stream);
            }
        }
    }
}