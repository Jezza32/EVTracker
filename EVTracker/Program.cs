using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                Debug.Assert(stream != null, "Failed to load species");
                types = (List<PokemonType>)deserializer.ReadObject(stream);
            }

		    types.ForEach(t => species.Add(t.DexNumber, t));

		    Dictionary<string, Game> games;
            deserializer = new DataContractSerializer(typeof(List<Game>));
		    using (var stream = assem.GetManifestResourceStream("EVTracker.Resources.Games.evt"))
            {
                Debug.Assert(stream != null, "Failed to load games");
                var game = (List<Game>) deserializer.ReadObject(stream);
                games = game.ToDictionary(g => g.Name);
		    }


            deserializer = new DataContractSerializer(typeof(List<Nature>));
            Dictionary<string, Nature> natures;

		    using (var stream = assem.GetManifestResourceStream("EVTracker.Resources.Natures.evt"))
		    {
		        Debug.Assert(stream != null, "Failed to load natures");
		        var nature = (List<Nature>) deserializer.ReadObject(stream);
		        natures = nature.ToDictionary(n => n.Name);
		    }

		    Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1(games, species, natures));
		}
	}
}
