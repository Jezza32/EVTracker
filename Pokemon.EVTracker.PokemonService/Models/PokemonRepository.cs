using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.PokemonService.Models
{
    public class PokemonRepository
    {
        public async Task<EVTracker.Models.Pokemon> Get()
        {
            using (var httpClient = new HttpClient())
            {
                var speciesResponse = await httpClient.GetAsync("http://localhost:20640/api/v0/species");
                var pokemonSpecies = await speciesResponse.Content.ReadAsAsync<IEnumerable<PokemonType>>();
                return new EVTracker.Models.Pokemon(pokemonSpecies.First(), 1, new Nature("hardy", Stat.HP, Stat.HP), Items.None, false, null, null);
            }
        }
    }
}