using System.Collections.Generic;

namespace Pokemon.EVTracker.Models
{
    public class Route
    {
        public Route(string name, IList<int> pokemon)
        {
            Name = name;
            Pokemon = pokemon;
        }

        public string Name { get; }

        public IList<int> Pokemon { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}