using System.Collections.Generic;

namespace Pokemon.EVTracker.Models
{
    public class Game
    {
        public Game(string name, IList<Route> routes)
        {
            Name = name;
            Routes = routes;
        }

        public string Name { get; }
        public IList<Route> Routes { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}