using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Pokemon.EVTracker.Models;
using Pokemon.EVTracker.NaturesService.Models;

namespace Pokemon.EVTracker.NaturesService.Controllers
{
    [Route("api/v0/[controller]")]
    public class NaturesController : Controller
    {
        private readonly INatureRepository _natureRepository;

        public NaturesController([FromServices] INatureRepository natureRepository)
        {
            _natureRepository = natureRepository;
        }

        [HttpGet]
        public Task<IEnumerable<Nature>> Get()
        {
            return _natureRepository.Get();
        }

        [HttpGet("{nature}")]
        public Task<Nature> Get(string nature)
        {
            return _natureRepository.Get(nature);
        }
    }
}
