using System.Threading.Tasks;
using Pokemon.EVTracker.PokemonService.Models;

namespace Pokemon.EVTracker.PokemonService.Repositories
{
    public interface IPokemonRepository
    {
        Task<ComputedPokemon> Get();
    }
}