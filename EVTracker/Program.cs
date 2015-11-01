using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Xml.Serialization;
using EVTracker.Properties;

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
            Dictionary<int, PokemonType> species;
		    Dictionary<string, Game> games;
            Dictionary<string, Nature> natures;

            var deserializer = new DataContractSerializer(typeof(List<PokemonType>));
            using (var stream = new MemoryStream(Resources.Species))
            {
                var types = (List<PokemonType>)deserializer.ReadObject(stream);
                species = types.ToDictionary(t => t.DexNumber);
            }

            deserializer = new DataContractSerializer(typeof(List<Game>));
		    using (var stream = new MemoryStream(Resources.Games))
            {
                var game = (List<Game>) deserializer.ReadObject(stream);
                games = game.ToDictionary(g => g.Name);
		    }


            deserializer = new DataContractSerializer(typeof(List<Nature>));
		    using (var stream = new MemoryStream(Resources.Natures))
		    {
		        var nature = (List<Nature>) deserializer.ReadObject(stream);
		        natures = nature.ToDictionary(n => n.Name);
		    }

		    Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1(games, species, natures));
		}
	}
}
