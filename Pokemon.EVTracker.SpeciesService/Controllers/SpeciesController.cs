using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Pokemon.EVTracker.SpeciesService.Models;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.SpeciesService.Controllers
{
    [Route("api/v0/[controller]")]
    public class SpeciesController : Controller
    {
        [FromServices] public ISpeciesRepository SpeciesRepository { get; set; }

        // GET: api/v0/species
        [HttpGet]
        public Task<IEnumerable<PokemonType>> GetAll()
        {
            return SpeciesRepository.GetAllAsync();
        }

        // GET: api/v0/species/{dexNumber}
        [HttpGet("{dexNumber}")]
        public Task<PokemonType> GetSpecies(int dexNumber)
        {
            return SpeciesRepository.GetSpeciesAsync(dexNumber);
        }
    }
}
