using System.Collections.Generic;
using System.Threading.Tasks;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.SpeciesService.Models
{
    public interface ISpeciesRepository
    {
        Task<IEnumerable<PokemonType>> GetAllAsync();
    }
}