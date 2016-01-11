using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

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

            var games = new GamesLoader().Load().ToList();
            var pokemonSpecies = new SpeciesLoader().Load();
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
