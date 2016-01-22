using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Pokemon.EVTracker.GamesService.Models;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.GamesService.Controllers
{
    [Route("api/v0/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGamesRepository _gamesRepository;

        public GamesController([FromServices] IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }
        
        [HttpGet]
        public Task<IEnumerable<Game>> Get()
        {
            return _gamesRepository.GetAll();
        }
    }
}
