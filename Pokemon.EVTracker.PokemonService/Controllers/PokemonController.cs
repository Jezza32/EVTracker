using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Pokemon.EVTracker.Models;
using Pokemon.EVTracker.PokemonService.Models;
using Pokemon.EVTracker.PokemonService.Repositories;

namespace Pokemon.EVTracker.PokemonService.Controllers
{
    [Route("api/v0/[controller]")]
    public class PokemonController : Controller
    {
        private readonly IPokemonRepository _pokemonRepository;

        public PokemonController([FromServices] IPokemonRepository pokemonRepository)
        {
            _pokemonRepository = pokemonRepository;
        }

        [HttpGet]
        public Task<ComputedPokemon> Get()
        {
            return _pokemonRepository.Get();
        }
        
        [HttpGet("update/{type}/{stat}/{change}")]
        public Task<ComputedPokemon> UpdateValue(string type, Stat stat, int change)
        {
            if (string.Equals("effort", type, StringComparison.CurrentCultureIgnoreCase))
            {
                return _pokemonRepository.UpdateEV(stat, change);
            }

            if (string.Equals("individual", type, StringComparison.CurrentCultureIgnoreCase))
            {
                return _pokemonRepository.UpdateIV(stat, change);
            }

            return Get();
        }
        
        [HttpGet("update/nature/{nature}")]
        public Task<ComputedPokemon> UpdateNature(string nature)
        {
            return _pokemonRepository.UpdateNature(nature);
        }
        
        [HttpGet("defeat/{dexNumber}")]
        public Task<ComputedPokemon> Defeat(int dexNumber)
        {
            return _pokemonRepository.Defeat(dexNumber);
        }
    }
}
