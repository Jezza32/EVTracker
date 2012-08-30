using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Resources;
using System.Globalization;
using System.Collections;
using System.Runtime.Serialization;
using System.Reflection;
using System.IO;

namespace EVTracker
{
	public static class Manager
	{
		static Manager()
		{
			species = new Dictionary<int, PokemonType>();
			List<PokemonType> types;
			Assembly assem = Assembly.GetExecutingAssembly();

			DataContractSerializer deserializer = new DataContractSerializer(typeof(List<PokemonType>));
			using (Stream stream = assem.GetManifestResourceStream("EVTracker.Resources.Species.evt")) {
				types = (List<PokemonType>)deserializer.ReadObject(stream);
			}

			types.ForEach(t => species.Add(t.DexNumber, t));

			List<Game> game = new List<Game>();
			deserializer = new DataContractSerializer(typeof(List<Game>));
			game = (List<Game>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Games.evt"));

			games = new Dictionary<string, Game>();
			game.ForEach(g => games.Add(g.Name, g));

			List<Nature> nature = new List<Nature>();
			deserializer = new DataContractSerializer(typeof(List<Nature>));
			nature = (List<Nature>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Natures.evt"));

			natures = new Dictionary<string, Nature>();
			nature.ForEach(g => natures.Add(g.Name, g));
		}
		private static IDictionary<int, PokemonType> species;
		private static IDictionary<string, Game> games;
		private static IDictionary<string, Nature> natures;

		public static PokemonType GetPokemonType(int i)
		{
			return species[i];
		}
		public static IList<PokemonType> GetPokemonTypes()
		{
			return species.Values.ToList();
		}
		public static Game GetGame(string s)
		{
			return games[s];
		}
		public static IList<Game> GetGames() { return games.Values.ToList(); }
		public static Nature GetNature(string s)
		{
			return natures[s];
		}
		public static IList<Nature> GetNatures() { return natures.Values.ToList(); }
	}
}
