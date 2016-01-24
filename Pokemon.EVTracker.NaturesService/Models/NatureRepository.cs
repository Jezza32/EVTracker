using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.NaturesService.Models
{
    public class NatureRepository : INatureRepository
    {
        private readonly ConcurrentBag<Nature> _natures;

        public NatureRepository([FromServices] IHostingEnvironment env)
        {
            var path = env.MapPath("natures.json");
            var json = File.ReadAllText(path);
            var natures = JsonConvert.DeserializeObject<IEnumerable<Nature>>(json);

            _natures = new ConcurrentBag<Nature>(natures);
        }

        public Task<IEnumerable<Nature>> Get()
        {
            return Task.FromResult<IEnumerable<Nature>>(_natures);
        }
    }
}