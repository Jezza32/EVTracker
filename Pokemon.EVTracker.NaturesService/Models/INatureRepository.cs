using System.Collections.Generic;
using System.Threading.Tasks;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.NaturesService.Models
{
    public interface INatureRepository
    {
        Task<IEnumerable<Nature>> Get();
    }
}