using System.Threading.Tasks;
using Pokemon.EVTracker.Models;
using Pokemon.EVTracker.PokemonService.Models;

namespace Pokemon.EVTracker.PokemonService.Repositories
{
    public interface IPokemonRepository
    {
        Task<ComputedPokemon> Get();
        Task<ComputedPokemon> UpdateEV(Stat stat, int change);
        Task<ComputedPokemon> UpdateIV(Stat stat, int change);
        Task<ComputedPokemon> UpdateNature(string nature);
        Task<ComputedPokemon> Defeat(int dexNumber);
    }
}