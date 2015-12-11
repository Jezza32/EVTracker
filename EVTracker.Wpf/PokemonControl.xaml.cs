using System;
using System.Windows.Controls;

namespace EVTracker.Wpf
{
    /// <summary>
    /// Interaction logic for PokemonControl.xaml
    /// </summary>
    public partial class PokemonControl : UserControl
    {
        public PokemonControl()
        {
            InitializeComponent();
            
            var species = new SpeciesLoader().Load();
            SpeciesComboBox.ItemsSource = species;

            var natures = new NaturesLoader().Load();
            NatureComboBox.ItemsSource = natures;

            HeldItemComboBox.ItemsSource = Enum.GetValues(typeof(Items));
        }
    }
}
