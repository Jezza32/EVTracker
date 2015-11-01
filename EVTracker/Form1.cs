using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;

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
			tabControl1.Selected += new TabControlEventHandler(tabControl1_Selected);

			tabControl1.TabPages.Add(new Page(_pokemonTypes, _natures).TabPage);
			_current = (Page)tabControl1.TabPages[0].Tag;

			if (File.Exists(saveLocation))
				load();
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
			cmbGame.SelectedIndexChanged += new EventHandler(cmbGame_SelectedIndexChanged);
			cmbRoute.Items.AddRange(((Game)cmbGame.SelectedItem).Routes.ToArray());
			cmbRoute.SelectedIndex = 0;
			cmbRoute.SelectedIndexChanged += new EventHandler(cmbRoute_SelectedIndexChanged);
			cmbRoute_SelectedIndexChanged(null, null);
		}

		void cmbGame_SelectedIndexChanged(object sender, EventArgs e)
		{
			cmbRoute.Items.Clear();
			cmbRoute.Items.AddRange(((Game)cmbGame.SelectedItem).Routes.ToArray());
			cmbRoute.SelectedIndex = 0;
			cmbRoute_SelectedIndexChanged(null, null);
		}

		void cmbRoute_SelectedIndexChanged(object sender, EventArgs e)
		{
			splitContainer3.Panel2.Controls.Clear();
			int x = 0;
			int y = 0;
			int vert = 0;
			foreach (int i in ((Route)cmbRoute.SelectedItem).Pokemon)
			{
				Button b = new Button();
				b.Text = "";
				b.AutoSize = true;
				string location = "_" + i.ToString().PadLeft(3, '0');
				b.Tag = i;

				b.Image = (Image)Properties.Resources.ResourceManager.GetObject(location);


				b.Location = new Point(x, y);
				vert = (vert + 1) % 3;
				y = 106 * vert;
				if (vert == 0) x += 106;
				splitContainer3.Panel2.Controls.Add(b);

				b.Click += new EventHandler(b_Click);
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

		private string saveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EVTrackerSave.evt";
        private readonly IDictionary<string, Game> _games;
        private readonly IDictionary<int, PokemonType> _pokemonTypes;
        private readonly IDictionary<string, Nature> _natures;

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Pokemon> Saved = new List<Pokemon>();
			foreach (TabPage tab in tabControl1.TabPages)
			{
				Page p = (Page)tab.Tag;
				Saved.Add(p.Pokemon);
			}
			Pokemon.Serialize(saveLocation, Saved);
		}

		private void loadToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!File.Exists(saveLocation))
				MessageBox.Show("Cannot find " + saveLocation + " please ensure it exists before loading");
		}

		private void load(){
			DataContractSerializer deserializer = new DataContractSerializer(typeof(List<Pokemon>));
			Stream s = File.OpenRead(saveLocation);
			try
			{
				List<Pokemon> pok = (List<Pokemon>)deserializer.ReadObject(s);
				s.Close();
				tabControl1.TabPages.Clear();
				pok.ForEach(p =>
				{
					var page = new Page(_pokemonTypes, _natures);
                    page.Load(p);
					tabControl1.TabPages.Add(page.TabPage);
				});
				_current = ((Page)tabControl1.SelectedTab.Tag);
			}
			catch
			{
				//This is when the  file is invalid
				MessageBox.Show("The Save File is Invalid, load failed");
			}
		}

	}
}