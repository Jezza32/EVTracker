using System.Collections.Generic;
using System.Threading.Tasks;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.GamesService.Models
{
    public interface IGamesRepository
    {
        Task<IEnumerable<Game>> GetAll();
    }
}