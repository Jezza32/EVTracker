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

	    public int HP
		{
			get
			{
				int value = (2*Species.BaseStats[Stat.HP]) + IndividualValues[Stat.HP] + (EffortValues[Stat.HP] / 4);
				value *= Level;
				value /= 100;
				value += Level + 10;
				return value;
			}
		}
		public int Attack => GetStat(Stat.Attack);
	    public int Defence => GetStat(Stat.Defence);
	    public int SpecialAttack => GetStat(Stat.SpecialAttack);
	    public int SpecialDefence => GetStat(Stat.SpecialDefence);
	    public int Speed => GetStat(Stat.Speed);

	    private int GetStat(Stat s)
		{
			var value = (2 * Species.BaseStats[s]) + IndividualValues[s] + (EffortValues[s] / 4);
			value *= Level;
			value /= 100;
			value += 5;
			value = (int)Math.Floor(value * Nature.GetModifier(s));
			return value;
		}

	    public override string ToString()
	    {
	        return Species.ToString();
	    }
	}
}
