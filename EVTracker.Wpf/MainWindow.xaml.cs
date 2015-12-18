using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

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

            DataContext = new Context(new List<Pokemon>
                {
                    Pokemon.MissingNo,
                    Pokemon.MissingNo
                });
        }
    }

    public class Context
    {
        public Context(IList<Pokemon> pokemonList)
        {
            HpUpCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBoost(Stat.HP));
            PomegBerryCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBerry(Stat.HP));
            ProteinCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBoost(Stat.Attack));
            KelpsyBerryCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBerry(Stat.Attack));
            IronCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBoost(Stat.Defence));
            QualotBerryCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBerry(Stat.Defence));
            CalciumCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBoost(Stat.SpecialAttack));
            HondewBerryCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBerry(Stat.SpecialAttack));
            ZincCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBoost(Stat.SpecialDefence));
            GrepaBerryCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBerry(Stat.SpecialDefence));
            CarbosCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBoost(Stat.Speed));
            TamatoBerryCommand = new RelayCommand(o => CurrentPokemon.ApplyStatBerry(Stat.Speed));

            PokemonList = pokemonList;
            CurrentPokemon = pokemonList.FirstOrDefault();
        }

        public ICommand HpUpCommand { get; }
        public ICommand ProteinCommand { get; set; }
        public ICommand IronCommand { get; set; }
        public ICommand CalciumCommand { get; set; }
        public ICommand ZincCommand { get; set; }
        public ICommand CarbosCommand { get; set; }

        public ICommand PomegBerryCommand { get; set; }
        public ICommand KelpsyBerryCommand { get; set; }
        public ICommand QualotBerryCommand { get; set; }
        public ICommand HondewBerryCommand { get; }
        public ICommand GrepaBerryCommand { get; set; }
        public ICommand TamatoBerryCommand { get; set; }

        public IList<Pokemon> PokemonList { get; }
        public Pokemon CurrentPokemon { get; set; }
    }
}
