using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace EVTracker
{
	public class Manager
	{
		public Manager()
		{
			_species = new Dictionary<int, PokemonType>();
			List<PokemonType> types;
			var assem = Assembly.GetExecutingAssembly();

			var deserializer = new DataContractSerializer(typeof(List<PokemonType>));
			using (var stream = assem.GetManifestResourceStream("EVTracker.Resources.Species.evt")) {
				types = (List<PokemonType>)deserializer.ReadObject(stream);
			}

			types.ForEach(t => _species.Add(t.DexNumber, t));

		    deserializer = new DataContractSerializer(typeof(List<Game>));
			var game = (List<Game>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Games.evt"));

			_games = new Dictionary<string, Game>();
			game.ForEach(g => _games.Add(g.Name, g));

		    deserializer = new DataContractSerializer(typeof(List<Nature>));
			var nature = (List<Nature>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Natures.evt"));

			_natures = new Dictionary<string, Nature>();
			nature.ForEach(g => _natures.Add(g.Name, g));
		}
		private readonly IDictionary<int, PokemonType> _species;
		private readonly IDictionary<string, Game> _games;
		private readonly IDictionary<string, Nature> _natures;

		public PokemonType GetPokemonType(int i)
		{
			return _species[i];
		}
		public IList<PokemonType> GetPokemonTypes()
		{
			return _species.Values.ToList();
		}
		public Game GetGame(string s)
		{
			return _games[s];
		}
		public IList<Game> GetGames() { return _games.Values.ToList(); }
		public Nature GetNature(string s)
		{
			return _natures[s];
		}
		public IList<Nature> GetNatures() { return _natures.Values.ToList(); }
	}
}
