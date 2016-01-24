using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using Newtonsoft.Json;

namespace EVTracker.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var httpClient = new HttpClient())
            {
                var speciesResponse = httpClient.GetAsync("http://localhost:20640/api/v0/species").Result;
                var gamesResponse = httpClient.GetAsync("http://localhost:39775/api/v0/games").Result;
                var natureResponse = httpClient.GetAsync("http://localhost:25072/api/v0/natures").Result;
                var games = gamesResponse.Content.ReadAsAsync<IList<Game>>().Result;
                var pokemonSpecies = speciesResponse.Content.ReadAsAsync<IEnumerable<PokemonType>>().Result;
                var natures = natureResponse.Content.ReadAsAsync<IEnumerable<Nature>>().Result;
                var items = Enum.GetValues(typeof(Items));

                DataContext = new Context(new List<Pokemon>
                    {
                        Pokemon.MissingNo,
                        Pokemon.MissingNo
                    }, games, pokemonSpecies, natures, items.OfType<Items>());
            }

        }
    }
}
