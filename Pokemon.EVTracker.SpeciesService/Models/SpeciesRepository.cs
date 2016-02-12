using System.Collections.Concurrent;
using Pokemon.EVTracker.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;

namespace Pokemon.EVTracker.SpeciesService.Models
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly ConcurrentDictionary<int, PokemonType> _species;

        public SpeciesRepository([FromServices] IHostingEnvironment hostedEnvironment)
        {
            _species = new ConcurrentDictionary<int, PokemonType>();
            
            var path = hostedEnvironment.MapPath("species.json");
            var json = File.ReadAllText(path);
            var species = JsonConvert.DeserializeObject<IEnumerable<PokemonType>>(json);

            foreach (var pokemonType in species)
            {
                _species.TryAdd(pokemonType.DexNumber, pokemonType);
            }
        }

        public Task<IEnumerable<PokemonType>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<PokemonType>>(_species.Values);
        }

        public Task<PokemonType> GetSpeciesAsync(int dexNumber)
        {
            return Task.FromResult(_species[dexNumber]);
        }
    }
}