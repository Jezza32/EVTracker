using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EVTracker.Properties;

namespace EVTracker
{
    public partial class Form1 : Form
	{
		public Form1(IDictionary<string, Game> games, IDictionary<int, PokemonType> pokemonTypes, IDictionary<string, Nature> natures, ISaveLoader saveLoader)
		{
		    _games = games;
		    _pokemonTypes = pokemonTypes;
		    _natures = natures;
		    _saveLoader = saveLoader;
		    InitializeComponent();

			LoadGames();

			tabControl1.TabPages.Add(new Page(Pokemon.MissingNo, _pokemonTypes, _natures));

			if (File.Exists(_saveLocation))
				LoadFromFile();
		}

		public void LoadGames()
		{
			cmbGame.Items.AddRange(_games.Values.ToArray<object>());
			cmbGame.SelectedIndex = 0;
			cmbGame.SelectedIndexChanged += cmbGame_SelectedIndexChanged;
			cmbRoute.Items.AddRange(((Game)cmbGame.SelectedItem).Routes.ToArray<object>());
			cmbRoute.SelectedIndex = 0;
			cmbRoute.SelectedIndexChanged += cmbRoute_SelectedIndexChanged;
			cmbRoute_SelectedIndexChanged(null, null);
		}

		void cmbGame_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmbRoute.Items.Clear();
			cmbRoute.Items.AddRange(((Game)cmbGame.SelectedItem).Routes.ToArray<object>());
			cmbRoute.SelectedIndex = 0;
			cmbRoute_SelectedIndexChanged(null, null);
		}

		void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
		{
			splitContainer3.Panel2.Controls.Clear();
			var count = 0;
			foreach (var i in ((Route)cmbRoute.SelectedItem).Pokemon)
			{
			    var b = new Button
			    {
			        Text = "",
			        AutoSize = true
			    };
			    var location = "_" + i.ToString().PadLeft(3, '0');
				b.Tag = i;

				b.Image = (Image)Resources.ResourceManager.GetObject(location);


				b.Location = new Point((count / 3) * 106, (count % 3) * 106);
				splitContainer3.Panel2.Controls.Add(b);

				b.Click += b_Click;
			    count++;
			}
		}

		void b_Click(object sender, EventArgs e)
		{
			var i = (int)((Button)sender).Tag;

		    ((Page)tabControl1.SelectedTab).Defeat(_pokemonTypes[i]);
		}

		private void addPokemonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newTab = new Page(Pokemon.MissingNo, _pokemonTypes, _natures);
			tabControl1.TabPages.Add(newTab);
		}

        private void deletePokemonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tabControl1.TabPages.Count < 1) return;
			int page = tabControl1.SelectedIndex;
			tabControl1.TabPages.RemoveAt(page);
			tabControl1.SelectedIndex = Math.Min(tabControl1.TabPages.Count - 1, page);
		}

		private readonly string _saveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EVTrackerSave.evt";
        private readonly IDictionary<string, Game> _games;
        private readonly IDictionary<int, PokemonType> _pokemonTypes;
        private readonly IDictionary<string, Nature> _natures;
        private readonly ISaveLoader _saveLoader;

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saved = tabControl1.TabPages.Cast<TabPage>().Select(p => ((Page)p).Pokemon).ToList();
            Pokemon.Serialize(_saveLocation, saved);
		}

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!File.Exists(_saveLocation))
				MessageBox.Show($"Cannot find {_saveLocation} please ensure it exists before loading");
            LoadFromFile();
		}

        private void LoadFromFile(){
            try
            {
                var savedPokemon = _saveLoader.LoadSaveFile(_saveLocation);
                tabControl1.TabPages.Clear();
                foreach (var pokemon in savedPokemon)
                {
                     var page = new Page(pokemon, _pokemonTypes, _natures);
                     tabControl1.TabPages.Add(page);
                }
            }
            catch
            {
                //This is when the  file is invalid
                MessageBox.Show(Resources.LoadFailed);
            }
        }
    }
}