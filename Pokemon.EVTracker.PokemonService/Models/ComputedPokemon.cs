using System;
using System.Collections.Generic;
using Pokemon.EVTracker.Models;

namespace Pokemon.EVTracker.PokemonService.Models
{
    public class ComputedPokemon : EVTracker.Models.Pokemon
    {
        private readonly IDictionary<Stat, int> _baseStats;

        public ComputedPokemon(EVTracker.Models.Pokemon pokemon, PokemonType species)
            : base(pokemon.DexNumber, pokemon.Level, pokemon.Nature, pokemon.HeldItem, pokemon.HasPokerus, pokemon.IndividualValues, pokemon.EffortValues)
        {
            if (pokemon.DexNumber != species.DexNumber) throw new Exception("Wrong Pokemon");

            _baseStats = species.BaseStats;
        }

        //public int HP
        //{
        //    get
        //    {
        //        int value = (2 * _baseStats[Stat.HP]) + IndividualValues[Stat.HP] + (EffortValues[Stat.HP] / 4);
        //        value *= Level;
        //        value /= 100;
        //        value += Level + 10;
        //        return value;
        //    }
        //    set { }
        //}
        //public int Attack { get { return GetStat(Stat.Attack); } set {} }
        //public int Defence { get { return GetStat(Stat.Defence); } set {} }
        //public int SpecialAttack { get { return GetStat(Stat.SpecialAttack); } set {} }
        //public int SpecialDefence { get { return GetStat(Stat.SpecialDefence); } set {} }
        public int Speed { get { return GetStat(Stat.Speed); } set {} }

        private int GetStat(Stat s)
        {
            var value = (2 * _baseStats[s]) + IndividualValues[s] + (EffortValues[s] / 4);
            value *= Level;
            value /= 100;
            value += 5;
            value = (int)Math.Floor(value * Nature.GetModifier(s));
            return value;
        }
    }
}