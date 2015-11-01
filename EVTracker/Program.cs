using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace EVTracker
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            var species = new Dictionary<int, PokemonType>();
            List<PokemonType> types;
            var assem = Assembly.GetExecutingAssembly();

            var deserializer = new DataContractSerializer(typeof(List<PokemonType>));
            using (var stream = assem.GetManifestResourceStream("EVTracker.Resources.Species.evt"))
            {
                types = (List<PokemonType>)deserializer.ReadObject(stream);
            }

            types.ForEach(t => species.Add(t.DexNumber, t));

            deserializer = new DataContractSerializer(typeof(List<Game>));
            var game = (List<Game>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Games.evt"));

            var games = new Dictionary<string, Game>();
            game.ForEach(g => games.Add(g.Name, g));

            deserializer = new DataContractSerializer(typeof(List<Nature>));
            var nature = (List<Nature>)deserializer.ReadObject(assem.GetManifestResourceStream("EVTracker.Resources.Natures.evt"));

            var natures = new Dictionary<string, Nature>();
            nature.ForEach(g => natures.Add(g.Name, g));

            Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1(games, species, natures));
		}
	}
}
