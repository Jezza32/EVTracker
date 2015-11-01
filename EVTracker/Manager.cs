using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace EVTracker
{
	public static class Manager
	{
		static Manager()
		{
			Species = new Dictionary<int, PokemonType>();
			List<PokemonType> types;
			var assem = Assembly.GetExecutingAssembly();

			var deserializer = new DataContractSerializer(typeof(List<PokemonType>));
			using (var stream = assem.GetManifestResourceStream("EVTracker.Resources.Species.evt")) {
				types = (List<PokemonType>)deserializer.ReadObject(stream);
			}

			types.ForEach(t => Species.Add(t.DexNumber, t));

		    deserializer = new DataContractSerializer(typeof(List<Game>));
			var game = (List<Game>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Games.evt"));

			Games = new Dictionary<string, Game>();
			game.ForEach(g => Games.Add(g.Name, g));

		    deserializer = new DataContractSerializer(typeof(List<Nature>));
			var nature = (List<Nature>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Natures.evt"));

			Natures = new Dictionary<string, Nature>();
			nature.ForEach(g => Natures.Add(g.Name, g));
		}
		private static readonly IDictionary<int, PokemonType> Species;
		private static readonly IDictionary<string, Game> Games;
		private static readonly IDictionary<string, Nature> Natures;

		public static PokemonType GetPokemonType(int i)
		{
			return Species[i];
		}
		public static IList<PokemonType> GetPokemonTypes()
		{
			return Species.Values.ToList();
		}
		public static Game GetGame(string s)
		{
			return Games[s];
		}
		public static IList<Game> GetGames() { return Games.Values.ToList(); }
		public static Nature GetNature(string s)
		{
			return Natures[s];
		}
		public static IList<Nature> GetNatures() { return Natures.Values.ToList(); }
	}
}
