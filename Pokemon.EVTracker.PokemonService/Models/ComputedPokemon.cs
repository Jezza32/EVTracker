using System;
using System.Collections.Generic;
using System.Linq;
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
            Stats = Enum.GetValues(typeof(Stat)).OfType<Stat>().ToDictionary(s => s, s => s == Stat.HP ? CalcHP() : CalcStat(s));
        }

        public IDictionary<Stat, int> Stats { get; set; }

        private int CalcHP()
        {
            int value = (2 * _baseStats[Stat.HP]) + IndividualValues[Stat.HP] + (EffortValues[Stat.HP] / 4);
            value *= Level;
            value /= 100;
            value += Level + 10;
            return value;
        }

        private int CalcStat(Stat s)
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