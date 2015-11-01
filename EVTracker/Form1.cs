using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using EVTracker.Properties;

namespace EVTracker
{
    public partial class Form1 : Form
	{
		Page _current;

		public Form1(IDictionary<string, Game> games, IDictionary<int, PokemonType> pokemonTypes, IDictionary<string, Nature> natures)
		{
		    _games = games;
		    _pokemonTypes = pokemonTypes;
		    _natures = natures;
		    InitializeComponent();

			LoadGames();
			tabControl1.Selected += tabControl1_Selected;

			tabControl1.TabPages.Add(new Page(_pokemonTypes, _natures).TabPage);
			_current = (Page)tabControl1.TabPages[0].Tag;

			if (File.Exists(_saveLocation))
				LoadFromFile();
		}

		void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage == null) return;
			_current = ((Page)e.TabPage.Tag);
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
			var dict = _pokemonTypes[i].GivenEffortValues;

            if (dict.ContainsKey(Stat.HP)) _current.UpdateStat(Stat.HP, dict[Stat.HP]);
			if (dict.ContainsKey(Stat.Attack)) _current.UpdateStat(Stat.Attack, dict[Stat.Attack]);
			if (dict.ContainsKey(Stat.Defence)) _current.UpdateStat(Stat.Defence, dict[Stat.Defence]);
			if (dict.ContainsKey(Stat.SpecialAttack)) _current.UpdateStat(Stat.SpecialAttack, dict[Stat.SpecialAttack]);
			if (dict.ContainsKey(Stat.SpecialDefence)) _current.UpdateStat(Stat.SpecialDefence, dict[Stat.SpecialDefence]);
			if (dict.ContainsKey(Stat.Speed)) _current.UpdateStat(Stat.Speed, dict[Stat.Speed]);

		    _current.ApplyItem(((Items) _current.HeldItem.SelectedItem));
		}

		private void addPokemonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var newTab = new Page(_pokemonTypes, _natures).TabPage;
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

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var saved = tabControl1.TabPages.Cast<TabPage>().Select(p => ((Page)p.Tag).Pokemon).ToList();
            Pokemon.Serialize(_saveLocation, saved);
		}

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!File.Exists(_saveLocation))
				MessageBox.Show($"Cannot find {_saveLocation} please ensure it exists before loading");
		}

        private void LoadFromFile(){
			var deserializer = new DataContractSerializer(typeof(List<Pokemon>));
            using (var stream = File.OpenRead(_saveLocation))
            {
                try
                {
                    var pok = (List<Pokemon>) deserializer.ReadObject(stream);
                    stream.Close();
                    tabControl1.TabPages.Clear();
                    pok.ForEach(p =>
                    {
                        var page = new Page(_pokemonTypes, _natures);
                        page.Load(p);
                        tabControl1.TabPages.Add(page.TabPage);
                    });
                    _current = ((Page) tabControl1.SelectedTab.Tag);
                }
                catch
                {
                    //This is when the  file is invalid
                    MessageBox.Show(Resources.LoadFailed);
                }
            }
        }

	}
}