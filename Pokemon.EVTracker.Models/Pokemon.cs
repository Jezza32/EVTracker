using System;
using System.Collections.Generic;

namespace Pokemon.EVTracker.Models
{
	public class Pokemon
	{
	    public Pokemon(PokemonType species, int level, Nature nature, Items heldItem, bool hasPokerus, IDictionary<Stat, int> individualValues, IDictionary<Stat, int> effortValues)
	    {
	        Species = species;
	        Level = level;
	        Nature = nature;
	        HeldItem = heldItem;
	        HasPokerus = hasPokerus;
	        IndividualValues = individualValues;
	        EffortValues = effortValues;
	    }

	    public PokemonType Species { get; set; }
	    public int Level { get; set; }
        public Nature Nature { get; set; }
        public Items HeldItem { get; set; }
        public bool HasPokerus { get; set; }
	    public IDictionary<Stat, int> IndividualValues { get; }
	    public IDictionary<Stat, int> EffortValues { get; }

	    public override string ToString()
	    {
	        return Species.ToString();
	    }
	}
}
