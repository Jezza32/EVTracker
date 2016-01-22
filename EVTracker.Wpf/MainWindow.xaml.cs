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
                var response = httpClient.GetAsync("http://localhost:20640/api/v0/species").Result;
                var games = new GamesLoader().Load().ToList();
                var pokemonSpecies = response.Content.ReadAsAsync<IEnumerable<PokemonType>>().Result;
                var natures = new NaturesLoader().Load();
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
