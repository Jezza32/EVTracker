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
            var pokemonSpecies = new SpeciesLoader().Load().ToDictionary(s => s.DexNumber);

            DataContext = new Context(new List<Pokemon>
                {
                    Pokemon.MissingNo,
                    Pokemon.MissingNo
                }, games, pokemonSpecies);
        }
    }
}
