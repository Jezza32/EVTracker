using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using EVTracker.Annotations;

namespace EVTracker.Wpf
{
    public class Context : INotifyPropertyChanged
    {
        private readonly IDictionary<int, PokemonType> _species;
        private Game _currentGame;
        private Route _currentRoute;
        private Pokemon _currentPokemon;

        public Context(IList<Pokemon> pokemonList, IList<Game> games, IDictionary<int, PokemonType> species)
        {
            _species = species;
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

            DefeatPokemon = new RelayCommand(o =>
            {
                var speciesNumber = o as int?;
                if (!speciesNumber.HasValue) return;
                CurrentPokemon.Defeat(_species[speciesNumber.Value]);
            });

            PokemonList = pokemonList;
            Games = games;
            CurrentPokemon = pokemonList.FirstOrDefault();
            CurrentGame = games.FirstOrDefault();
            CurrentRoute = CurrentGame?.Routes.FirstOrDefault();

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "CurrentGame")
                {
                    CurrentRoute = CurrentGame?.Routes.FirstOrDefault();
                }
            };
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

        public ICommand DefeatPokemon { get; set; }

        public IList<Pokemon> PokemonList { get; }
        public IList<Game> Games { get; set; }

        public Pokemon CurrentPokemon
        {
            get { return _currentPokemon; }
            set
            {
                if (Equals(value, _currentPokemon)) return;
                _currentPokemon = value;
                OnPropertyChanged();
            }
        }

        public Game CurrentGame
        {
            get { return _currentGame; }
            set
            {
                if (Equals(value, _currentGame)) return;
                _currentGame = value;
                OnPropertyChanged();
            }
        }

        public Route CurrentRoute
        {
            get { return _currentRoute; }
            set
            {
                if (Equals(value, _currentRoute)) return;
                _currentRoute = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}