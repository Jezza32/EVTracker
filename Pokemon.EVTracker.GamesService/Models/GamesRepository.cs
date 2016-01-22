using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.GamesService.Models
{
    class GamesRepository : IGamesRepository
    {
        private readonly ConcurrentBag<Game> _games;

        public GamesRepository([FromServices] IHostingEnvironment env)
        {
            var path = env.MapPath("games.json");
            var json = File.ReadAllText(path);
            var games = JsonConvert.DeserializeObject<IEnumerable<Game>>(json);
            
            _games = new ConcurrentBag<Game>(games);
        }

        public Task<IEnumerable<Game>> GetAll()
        {
            return Task.FromResult<IEnumerable<Game>>(_games);
        }
    }
}