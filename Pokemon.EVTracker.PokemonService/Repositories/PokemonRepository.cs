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
        private readonly EVTracker.Models.Pokemon _trackedPokemon;

        public PokemonRepository()
        {
            var ivs = Enum.GetValues(typeof(Stat)).OfType<Stat>().ToDictionary(s => s, _ => 0);
            var evs = Enum.GetValues(typeof(Stat)).OfType<Stat>().ToDictionary(s => s, _ => 0);
            _trackedPokemon = new EVTracker.Models.Pokemon(10, 100, new Nature("hardy", Stat.HP, Stat.HP), Item.None, false, ivs, evs);
        }

        public async Task<ComputedPokemon> Get()
        {
            using (var httpClient = new HttpClient())
            {
                var speciesResponse = await httpClient.GetAsync($"http://localhost:20640/api/v0/species/{_trackedPokemon.DexNumber}");
                var pokemonSpecies = await speciesResponse.Content.ReadAsAsync<PokemonType>();

                return new ComputedPokemon(_trackedPokemon, pokemonSpecies);
            }
        }

        public Task<ComputedPokemon> UpdateEV(Stat stat, int change)
        {
            _trackedPokemon.EffortValues[stat] += change;

            return Get();
        }

        public Task<ComputedPokemon> UpdateIV(Stat stat, int change)
        {
            _trackedPokemon.IndividualValues[stat] += change;

            return Get();
        }

        public async Task<ComputedPokemon> Defeat(int dexNumber)
        {
            using (var httpClient = new HttpClient())
            {
                var speciesResponse = await httpClient.GetAsync($"http://localhost:20640/api/v0/species/{dexNumber}");
                var pokemonSpecies = await speciesResponse.Content.ReadAsAsync<PokemonType>();

                var dict = pokemonSpecies.GivenEffortValues;

                foreach (var stat in Enum.GetValues(typeof(Stat)).OfType<Stat>())
                {
                    if (dict.ContainsKey(stat))
                    {
                        await UpdateEV(stat, dict[stat]);
                    }
                }

                return await Get();
            }
        }
    }
}