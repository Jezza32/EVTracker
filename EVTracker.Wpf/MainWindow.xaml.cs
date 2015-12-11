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

            DataContext = new
            {
                PokemonList = new List<Pokemon>
                {
                    Pokemon.MissingNo,
                    Pokemon.MissingNo
                }
            };
        }
    }
}
