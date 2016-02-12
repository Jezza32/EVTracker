using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.ItemsService.Controllers
{
    [Route("api/v0/[controller]")]
    public class ItemsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enum.GetValues(typeof(Item)).OfType<Item>().Select(i => i.ToString());
        }
    }
}
