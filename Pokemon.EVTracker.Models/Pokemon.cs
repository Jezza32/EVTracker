using System;
using System.Collections.Generic;

namespace Pokemon.EVTracker.Models
{
	public class Pokemon
	{
	    public Pokemon(int dexNumber, int level, Nature nature, Item heldItem, bool hasPokerus, IDictionary<Stat, int> individualValues, IDictionary<Stat, int> effortValues)
	    {
	        DexNumber = dexNumber;
	        Level = level;
	        Nature = nature;
	        HeldItem = heldItem;
	        HasPokerus = hasPokerus;
	        IndividualValues = individualValues;
	        EffortValues = effortValues;
	    }

	    public int DexNumber { get; set; }
	    public int Level { get; set; }
        public Nature Nature { get; set; }
        public Item HeldItem { get; set; }
        public bool HasPokerus { get; set; }
	    public IDictionary<Stat, int> IndividualValues { get; }
	    public IDictionary<Stat, int> EffortValues { get; }

	    public override string ToString()
	    {
	        return DexNumber.ToString();
	    }
	}
}
