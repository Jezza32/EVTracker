using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Pokemon.EVTracker.Models;
using Pokemon.EVTracker.PokemonService.Models;

namespace Pokemon.EVTracker.PokemonService.Repositories
{
    public class PokemonRepository : IPokemonRepository
    {
        public async Task<ComputedPokemon> Get()
        {
            using (var httpClient = new HttpClient())
            {
                var ivs = Enum.GetValues(typeof(Stat)).OfType<Stat>().ToDictionary(s => s, _ => 0);
                var evs = Enum.GetValues(typeof(Stat)).OfType<Stat>().ToDictionary(s => s, _ => 0);
                var pokemon = new EVTracker.Models.Pokemon(10, 1, new Nature("hardy", Stat.HP, Stat.HP), Item.None, false, ivs, evs);

                var speciesResponse = await httpClient.GetAsync($"http://localhost:20640/api/v0/species/{pokemon.DexNumber}");
                var pokemonSpecies = await speciesResponse.Content.ReadAsAsync<PokemonType>();

                return new ComputedPokemon(pokemon, pokemonSpecies);
            }
        }
    }
}