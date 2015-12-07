using System;
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
            
            var species = new SpeciesLoader().Load();
            SpeciesComboBox.ItemsSource = species;

            var natures = new NaturesLoader().Load();
            NatureComboBox.ItemsSource = natures;

            HeldItemComboBox.ItemsSource = Enum.GetValues(typeof (Items));

            DataContext = Pokemon.MissingNo;
        }
    }
}
