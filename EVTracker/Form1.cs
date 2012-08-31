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
	public enum Items
	{
		None,
		[Description("Macho Brace")]
		MachoBrace,
		[Description("Power Weight")]
		PowerWeight,
		[Description("Power Bracer")]
		PowerBracer,
		[Description("Power Belt")]
		PowerBelt,
		[Description("Power Lens")]
		PowerLens,
		[Description("Power Band")]
		PowerBand,
		[Description("Power Anklet")]
		PowerAnklet
	}
	public partial class Form1 : Form
	{
		Page current;

		public Form1()
		{
			InitializeComponent();

			LoadGames();
			tabControl1.Selected += new TabControlEventHandler(tabControl1_Selected);

			tabControl1.TabPages.Add(CreateTabPage());
			current = (Page)tabControl1.TabPages[0].Tag;

			if (File.Exists(saveLocation))
				load();

			UpdateForm();
		}

		void tabControl1_Selected(object sender, TabControlEventArgs e)
		{
			if (e.TabPage == null) return;
			current = ((Page)e.TabPage.Tag);
			recalculate(null, null);
		}

		public void LoadGames()
		{
			cmbGame.Items.AddRange(Manager.GetGames().ToArray());
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
			int i = (int)((Button)sender).Tag;
			IDictionary<Stat, int> dict = Manager.GetPokemonType(i).GivenEffortValues;
			foreach (Stat s in dict.Keys)
				switch (s)
				{
					case Stat.HP: current.EVHP.Value = Math.Min(dict[s] * (current.HeldItem.SelectedItem.ToString() == GetEnumDescription(Items.MachoBrace) ? 2 : 1) * (current.Pokerus.Checked ? 2 : 1) + current.EVHP.Value, 255);
						break;
					case Stat.Attack: current.EVAttack.Value = Math.Min(dict[s] * (current.HeldItem.SelectedItem.ToString() == GetEnumDescription(Items.MachoBrace) ? 2 : 1) * (current.Pokerus.Checked ? 2 : 1) + current.EVAttack.Value, 255);
						break;
					case Stat.Defence: current.EVDefence.Value = Math.Min(dict[s] * (current.HeldItem.SelectedItem.ToString() == GetEnumDescription(Items.MachoBrace) ? 2 : 1) * (current.Pokerus.Checked ? 2 : 1) + current.EVDefence.Value, 255);
						break;
					case Stat.SpecialAttack: current.EVSpecialAttack.Value = Math.Min(dict[s] * (current.HeldItem.SelectedItem.ToString() == GetEnumDescription(Items.MachoBrace) ? 2 : 1) * (current.Pokerus.Checked ? 2 : 1) + current.EVSpecialAttack.Value, 255);
						break;
					case Stat.SpecialDefence: current.EVSpecialDefence.Value = Math.Min(dict[s] * (current.HeldItem.SelectedItem.ToString() == GetEnumDescription(Items.MachoBrace) ? 2 : 1) * (current.Pokerus.Checked ? 2 : 1) + current.EVSpecialDefence.Value, 255);
						break;
					case Stat.Speed: current.EVSpeed.Value = Math.Min(dict[s] * (current.HeldItem.SelectedItem.ToString() == GetEnumDescription(Items.MachoBrace) ? 2 : 1) * (current.Pokerus.Checked ? 2 : 1) + current.EVSpeed.Value, 255);
						break;
					default:
						break;
				}
			switch ((Items)Enum.Parse(typeof(Items), current.HeldItem.SelectedItem.ToString().Replace(" ", "")))
			{
				case Items.PowerWeight:
					current.EVHP.Value = Math.Min(255, current.EVHP.Value + (current.Pokerus.Checked ? 8 : 4));
					break;
				case Items.PowerBracer:
					current.EVAttack.Value = Math.Min(255, current.EVAttack.Value + (current.Pokerus.Checked ? 8 : 4));
					break;
				case Items.PowerBelt:
					current.EVDefence.Value = Math.Min(255, current.EVDefence.Value + (current.Pokerus.Checked ? 8 : 4));
					break;
				case Items.PowerLens:
					current.EVSpecialAttack.Value = Math.Min(255, current.EVSpecialAttack.Value + (current.Pokerus.Checked ? 8 : 4));
					break;
				case Items.PowerBand:
					current.EVSpecialDefence.Value = Math.Min(255, current.EVSpecialDefence.Value + (current.Pokerus.Checked ? 8 : 4));
					break;
				case Items.PowerAnklet:
					current.EVSpeed.Value = Math.Min(255, current.EVSpeed.Value + (current.Pokerus.Checked ? 8 : 4));
					break;
			}

			recalculate(sender, e);
		}
		
		private void UpdateForm()
		{
			current.Pokemon.Species = (PokemonType)current.Species.SelectedItem;

			foreach (Stat s in Enum.GetValues(typeof(Stat)))
			{
				double modifier = current.Pokemon.Nature.GetModifier(s);
				if (modifier < 1) current.StatLabels[s].ForeColor = Color.Red;
				else if (modifier == 1) current.StatLabels[s].ForeColor = Color.Black;
				else current.StatLabels[s].ForeColor = Color.Green;
			}

			current.BaseStatHP.Text = current.Pokemon.Species.BaseStats[Stat.HP].ToString();
			current.BaseStatAttack.Text = current.Pokemon.Species.BaseStats[Stat.Attack].ToString();
			current.BaseStatDefence.Text = current.Pokemon.Species.BaseStats[Stat.Defence].ToString();
			current.BaseStatSpecialAttack.Text = current.Pokemon.Species.BaseStats[Stat.SpecialAttack].ToString();
			current.BaseStatSpecialDefence.Text = current.Pokemon.Species.BaseStats[Stat.SpecialDefence].ToString();
			current.BaseStatSpeed.Text = current.Pokemon.Species.BaseStats[Stat.Speed].ToString();

			current.ActualStatHP.Text = current.Pokemon.HP.ToString();
			current.ActualStatAttack.Text = current.Pokemon.Attack.ToString();
			current.ActualStatDefence.Text = current.Pokemon.Defence.ToString();
			current.ActualStatSpecialAttack.Text = current.Pokemon.SpecialAttack.ToString();
			current.ActualStatSpecialDefence.Text = current.Pokemon.SpecialDefence.ToString();
			current.ActualStatSpeed.Text = current.Pokemon.Speed.ToString();

			current.TabPage.Text = current.Pokemon.Species.Name;

			int num = current.Pokemon.Species.DexNumber;
			string location = "_" + num.ToString().PadLeft(3, '0');

			current.Image.Image = (Image)Properties.Resources.ResourceManager.GetObject(location);

			int ev = 0;
			ev += (int)current.EVHP.Value;
			ev += (int)current.EVAttack.Value;
			ev += (int)current.EVDefence.Value;
			ev += (int)current.EVSpecialAttack.Value;
			ev += (int)current.EVSpecialDefence.Value;
			ev += (int)current.EVSpeed.Value;

			int remaining = 510 - ev;
			current.Warning.ForeColor = (remaining >=0 ? Color.Black : Color.Red);
			current.Warning.Text = (remaining >= 0? "You have " + remaining + " EVs left" : "You are " + (remaining * -1) + " EVs over");
		}

		private void recalculate(object sender, EventArgs e)
		{
			current.Pokemon.Level = (int)current.Level.Value;
			current.Pokemon.HasPokerus = current.Pokerus.Checked;
			current.Pokemon.HeldItem = (Items)current.HeldItem.SelectedIndex;
			current.Pokemon.Nature = (Nature)current.Nature.SelectedItem;

			current.Pokemon.IV[Stat.HP] = (int)current.IVHP.Value;
			current.Pokemon.IV[Stat.Attack] = (int)current.IVAttack.Value;
			current.Pokemon.IV[Stat.Defence] = (int)current.IVDefence.Value;
			current.Pokemon.IV[Stat.SpecialAttack] = (int)current.IVSpecialAttack.Value;
			current.Pokemon.IV[Stat.SpecialDefence] = (int)current.IVSpecialDefence.Value;
			current.Pokemon.IV[Stat.Speed] = (int)current.IVSpeed.Value;

			current.Pokemon.EV[Stat.HP] = (int)current.EVHP.Value;
			current.Pokemon.EV[Stat.Attack] = (int)current.EVAttack.Value;
			current.Pokemon.EV[Stat.Defence] = (int)current.EVDefence.Value;
			current.Pokemon.EV[Stat.SpecialAttack] = (int)current.EVSpecialAttack.Value;
			current.Pokemon.EV[Stat.SpecialDefence] = (int)current.EVSpecialDefence.Value;
			current.Pokemon.EV[Stat.Speed] = (int)current.EVSpeed.Value;

			UpdateForm();
		}


		private void btnHPUp_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVHP.Value;
			if (value >= 100) return;
			value = Math.Min(100, value + 10);
			current.EVHP.Value = value;
			recalculate(sender, e);
		}

		private void btnProtein_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVAttack.Value;
			if (value >= 100) return;
			value = Math.Min(100, value + 10);
			current.EVAttack.Value = value;
			recalculate(sender, e);
		}

		private void btnIron_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVDefence.Value;
			if (value >= 100) return;
			value = Math.Min(100, value + 10);
			current.EVDefence.Value = value;
			recalculate(sender, e);
		}

		private void btnCalcium_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVSpecialAttack.Value;
			if (value >= 100) return;
			value = Math.Min(100, value + 10);
			current.EVSpecialAttack.Value = value;
			recalculate(sender, e);
		}

		private void btnZinc_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVSpecialDefence.Value;
			if (value >= 100) return;
			value = Math.Min(100, value + 10);
			current.EVSpecialDefence.Value = value;
			recalculate(sender, e);
		}

		private void btnCarbos_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVSpeed.Value;
			if (value >= 100) return;
			value = Math.Min(100, value + 10);
			current.EVSpeed.Value = value;
			recalculate(sender, e);
		}

		private void btnPomeg_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVHP.Value;
			if (value <= 100)
				value = Math.Max(0, value - 10);
			else
				value = 100;

			current.EVHP.Value = value;
			recalculate(sender, e);
		}

		private void btnKelpsy_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVAttack.Value;
			if (value <= 100)
				value = Math.Max(0, value - 10);
			else
				value = 100;

			current.EVAttack.Value = value;
			recalculate(sender, e);
		}

		private void btnQualot_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVDefence.Value;
			if (value <= 100)
				value = Math.Max(0, value - 10);
			else
				value = 100;

			current.EVDefence.Value = value;
			recalculate(sender, e);
		}

		private void btnHondew_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVSpecialAttack.Value;
			if (value <= 100)
				value = Math.Max(0, value - 10);
			else
				value = 100;

			current.EVSpecialAttack.Value = value;
			recalculate(sender, e);
		}

		private void btnGrepa_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVSpecialDefence.Value;
			if (value <= 100)
				value = Math.Max(0, value - 10);
			else
				value = 100;

			current.EVSpecialDefence.Value = value;
			recalculate(sender, e);
		}

		private void btnTamato_Click(object sender, EventArgs e)
		{
			int value = (int)current.EVSpeed.Value;
			if (value <= 100)
				value = Math.Max(0, value - 10);
			else
				value = 100;

			current.EVSpeed.Value = value;
			recalculate(sender, e);
		}

		private void cmbPok_SelectedIndexChanged(object sender, EventArgs e)
		{
			current.Pokemon.Species = (PokemonType)current.Species.SelectedItem;
			UpdateForm();
		}

		private void addPokemonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			TabPage newTab = CreateTabPage();
			tabControl1.TabPages.Add(newTab);
		}

		public TabPage CreateTabPage()
		{
			Page newPage = new Page();
			TabPage newTab = new TabPage();
			newTab.Tag = newPage;
			newPage.TabPage = newTab;
			newTab.BackColor = Color.White;
			newTab.Text = newPage.Pokemon.Species.Name;

			#region Pokemon Details
			ComboBox pok = new ComboBox();
			pok.Items.AddRange(Manager.GetPokemonTypes().ToArray());
			pok.SelectedIndex = 0;
			pok.DropDownStyle = ComboBoxStyle.DropDownList;
			pok.SelectedIndexChanged +=new EventHandler(cmbPok_SelectedIndexChanged);
			pok.Location = new Point(15,23);
			pok.Size = new Size(146, 21);
			newTab.Controls.Add(pok);
			newPage.Species = pok;

			//Nature
			ComboBox nat = new ComboBox();
			nat.Items.AddRange(Manager.GetNatures().ToArray());
			nat.SelectedIndex = 0;
			nat.DropDownStyle = ComboBoxStyle.DropDownList;
			nat.Location = new Point(pok.Right + 20, pok.Top);
			nat.SelectedIndexChanged += new EventHandler(recalculate);
			newTab.Controls.Add(nat);
			newPage.Nature = nat;

			//Pokerus
			CheckBox rus = new CheckBox();
			rus.Text = "Pokerus";
			rus.Location = new Point(nat.Right + 20, pok.Top);
			rus.CheckedChanged +=new EventHandler(recalculate);
			newTab.Controls.Add(rus);
			newPage.Pokerus = rus;

			//HeldItem
			ComboBox held = new ComboBox();
			held.Location = new Point(rus.Left, rus.Bottom + 20);
			newTab.Controls.Add(held);
			foreach (Items i in Enum.GetValues(typeof(Items))) held.Items.Add(GetEnumDescription(i));
			newPage.HeldItem = held;
			held.DropDownStyle = ComboBoxStyle.DropDownList;
			held.SelectedIndex = 0;
			held.SelectedIndexChanged +=new EventHandler(recalculate);

			Label level = new Label();
			level.Text = "Level";
			level.Location = new Point(11, 64);
			level.Size = new Size(33, 13);
			newTab.Controls.Add(level);

			NumericUpDown nudLevel = new NumericUpDown();
			nudLevel.Minimum = 1;
			nudLevel.Value = 1;
			nudLevel.Maximum = 100;
			nudLevel.Location = new Point(50, 62);
			nudLevel.Size = new Size(61, 20);
			nudLevel.ValueChanged +=new EventHandler(recalculate);
			newPage.Level = nudLevel;
			newTab.Controls.Add(nudLevel);

			PictureBox pic = new PictureBox();
			pic.Location = new Point(449,11);
			pic.Size = new Size(80,80);
			pic.SizeMode = PictureBoxSizeMode.AutoSize;
			newPage.Image = pic;
			newTab.Controls.Add(pic);

			Label hp = new Label(); hp.Text = "HP"; hp.Location = new Point(29, 116); hp.Size = new Size(22, 13); newTab.Controls.Add(hp); newPage.StatLabels[Stat.HP] = hp;
			Label att = new Label(); att.Text = "Attack"; att.Location = new Point(110, 116); att.Size = new Size(38, 13); newTab.Controls.Add(att); newPage.StatLabels[Stat.Attack] = att;
			Label def = new Label(); def.Text = "Defence"; def.Location = new Point(203, 116); def.Size = new Size(50, 13); newTab.Controls.Add(def); newPage.StatLabels[Stat.Defence] = def;
			Label satt = new Label(); satt.Text = "Sp. Attack"; satt.Location = new Point(296, 116); satt.Size = new Size(57, 13); newTab.Controls.Add(satt); newPage.StatLabels[Stat.SpecialAttack] = satt;
			Label sdef = new Label(); sdef.Text = "Sp. Defence"; sdef.Location = new Point(389, 116); sdef.Size = new Size(70, 13); newTab.Controls.Add(sdef); newPage.StatLabels[Stat.SpecialDefence] = sdef;
			Label spe = new Label(); spe.Text = "Speed"; spe.Location = new Point(482, 116); spe.Size = new Size(38, 13); newTab.Controls.Add(spe); newPage.StatLabels[Stat.Speed] = spe;

			#region BaseStats
			GroupBox groupStats = new GroupBox();
			groupStats.Text = "Base Stats";
			groupStats.Location = new Point(14, 132);
			groupStats.Size = new Size(529, 46);
			newTab.Controls.Add(groupStats);

			Label baseHP = new Label(); baseHP.Location = new Point(15, 21); baseHP.Size = new Size(35, 13); groupStats.Controls.Add(baseHP); newPage.BaseStatHP = baseHP;
			Label baseAtt = new Label(); baseAtt.Location = new Point(96, 21); baseAtt.Size = new Size(35, 13); groupStats.Controls.Add(baseAtt); newPage.BaseStatAttack = baseAtt;
			Label baseDef = new Label(); baseDef.Location = new Point(189, 21); baseDef.Size = new Size(35, 13); groupStats.Controls.Add(baseDef); newPage.BaseStatDefence = baseDef;
			Label baseSAtt = new Label(); baseSAtt.Location = new Point(282, 21); baseSAtt.Size = new Size(35, 13); groupStats.Controls.Add(baseSAtt); newPage.BaseStatSpecialAttack = baseSAtt;
			Label baseSDef = new Label(); baseSDef.Location = new Point(375, 21); baseSDef.Size = new Size(35, 13); groupStats.Controls.Add(baseSDef); newPage.BaseStatSpecialDefence = baseSDef;
			Label baseSpe = new Label(); baseSpe.Location = new Point(468, 21); baseSpe.Size = new Size(35, 13); groupStats.Controls.Add(baseSpe); newPage.BaseStatSpeed = baseSpe;
			#endregion

			#region IVs
			GroupBox groupIVs = new GroupBox();
			groupIVs.Text = "IVs";
			groupIVs.Location = new Point(14, 193);
			groupIVs.Size = new Size(529, 46);
			newTab.Controls.Add(groupIVs);

			NumericUpDown IVHP = new NumericUpDown(); IVHP.Location = new Point(15, 21); IVHP.Size = new Size(56, 20); groupIVs.Controls.Add(IVHP); newPage.IVHP = IVHP; IVHP.ValueChanged += new EventHandler(recalculate); IVHP.Maximum = 31;
			NumericUpDown IVAtt = new NumericUpDown(); IVAtt.Location = new Point(96, 21); IVAtt.Size = new Size(56, 20); groupIVs.Controls.Add(IVAtt); newPage.IVAttack = IVAtt; IVAtt.ValueChanged += new EventHandler(recalculate); IVAtt.Maximum = 31;
			NumericUpDown IVDef = new NumericUpDown(); IVDef.Location = new Point(189, 21); IVDef.Size = new Size(56, 20); groupIVs.Controls.Add(IVDef); newPage.IVDefence = IVDef; IVDef.ValueChanged += new EventHandler(recalculate); IVDef.Maximum = 31;
			NumericUpDown IVSAtt = new NumericUpDown(); IVSAtt.Location = new Point(282, 21); IVSAtt.Size = new Size(56, 20); groupIVs.Controls.Add(IVSAtt); newPage.IVSpecialAttack = IVSAtt; IVSAtt.ValueChanged += new EventHandler(recalculate); IVSAtt.Maximum = 31;
			NumericUpDown IVSDef = new NumericUpDown(); IVSDef.Location = new Point(375, 21); IVSDef.Size = new Size(56, 20); groupIVs.Controls.Add(IVSDef); newPage.IVSpecialDefence = IVSDef; IVSDef.ValueChanged += new EventHandler(recalculate); IVSDef.Maximum = 31;
			NumericUpDown IVSpe = new NumericUpDown(); IVSpe.Location = new Point(468, 21); IVSpe.Size = new Size(56, 20); groupIVs.Controls.Add(IVSpe); newPage.IVSpeed = IVSpe; IVSpe.ValueChanged += new EventHandler(recalculate); IVSpe.Maximum = 31;
			#endregion


			#region Actual Stats
			GroupBox groupActuals = new GroupBox();
			groupActuals.Text = "Actual Stats";
			groupActuals.Location = new Point(14, 262);
			groupActuals.Size = new Size(529, 46);
			newTab.Controls.Add(groupActuals);

			Label actualHP = new Label(); actualHP.Location = new Point(15, 21); actualHP.Size = new Size(35, 13); groupActuals.Controls.Add(actualHP); newPage.ActualStatHP = actualHP;
			Label actualAtt = new Label(); actualAtt.Location = new Point(96, 21); actualAtt.Size = new Size(35, 13); groupActuals.Controls.Add(actualAtt); newPage.ActualStatAttack = actualAtt;
			Label actualDef = new Label(); actualDef.Location = new Point(189, 21); actualDef.Size = new Size(35, 13); groupActuals.Controls.Add(actualDef); newPage.ActualStatDefence = actualDef;
			Label actualSAtt = new Label(); actualSAtt.Location = new Point(282, 21); actualSAtt.Size = new Size(35, 13); groupActuals.Controls.Add(actualSAtt); newPage.ActualStatSpecialAttack = actualSAtt;
			Label actualSDef = new Label(); actualSDef.Location = new Point(375, 21); actualSDef.Size = new Size(35, 13); groupActuals.Controls.Add(actualSDef); newPage.ActualStatSpecialDefence = actualSDef;
			Label actualSpe = new Label(); actualSpe.Location = new Point(468, 21); actualSpe.Size = new Size(35, 13); groupActuals.Controls.Add(actualSpe); newPage.ActualStatSpeed = actualSpe;
			#endregion
			#endregion

			#region EVs
			GroupBox EVs = new GroupBox();
			EVs.Location = new Point(550, 0);
			EVs.Height = 300;
			EVs.Width = this.Width - 580;
			EVs.Text = "EVs";
			newTab.Controls.Add(EVs);

			#region HP
			Label evHP = new Label();
			evHP.Location = new Point(6, 43);
			evHP.Size = new Size(22, 13);
			evHP.Text = "HP";
			EVs.Controls.Add(evHP);

			NumericUpDown nudEVHP = new NumericUpDown();
			nudEVHP.Maximum = 255;
			nudEVHP.Location = new Point(82, 41);
			nudEVHP.Size = new Size(120, 20);
			nudEVHP.ValueChanged += new EventHandler(recalculate);
			newPage.EVHP = nudEVHP;
			EVs.Controls.Add(nudEVHP);

			Button HPUp = new Button();
			HPUp.Text = "HP Up";
			HPUp.Location = new Point(208, 38);
			HPUp.Size = new Size(75, 23);
			HPUp.Click +=new EventHandler(btnHPUp_Click);
			EVs.Controls.Add(HPUp);

			Button Pomeg = new Button();
			Pomeg.Text = "Pomeg Berry";
			Pomeg.Location = new Point(289, 38);
			Pomeg.Size = new Size(84, 23);
			Pomeg.Click += new EventHandler(btnPomeg_Click);
			EVs.Controls.Add(Pomeg);
			#endregion

			#region Attack
			Label evAttack = new Label();
			evAttack.Location = new Point(6, 69);
			evAttack.Size = new Size(50, 13);
			evAttack.Text = "Attack";
			EVs.Controls.Add(evAttack);

			NumericUpDown nudEVAttack = new NumericUpDown();
			nudEVAttack.Maximum = 255;
			nudEVAttack.Location = new Point(82, 67);
			nudEVAttack.Size = new Size(120, 20);
			nudEVAttack.ValueChanged += new EventHandler(recalculate);
			newPage.EVAttack = nudEVAttack;
			EVs.Controls.Add(nudEVAttack);

			Button Protein = new Button();
			Protein.Text = "Protein";
			Protein.Location = new Point(208, 64);
			Protein.Size = new Size(75, 23);
			Protein.Click += new EventHandler(btnProtein_Click);
			EVs.Controls.Add(Protein);

			Button Kelpsy = new Button();
			Kelpsy.Text = "Kelpsy Berry";
			Kelpsy.Location = new Point(289, 64);
			Kelpsy.Size = new Size(84, 23);
			Kelpsy.Click += new EventHandler(btnKelpsy_Click);
			EVs.Controls.Add(Kelpsy);
			#endregion

			#region Defence
			Label evDefence = new Label();
			evDefence.Location = new Point(6, 95);
			evDefence.Size = new Size(50, 13);
			evDefence.Text = "Defence";
			EVs.Controls.Add(evDefence);

			NumericUpDown nudEVDefence = new NumericUpDown();
			nudEVDefence.Maximum = 255;
			nudEVDefence.Location = new Point(82, 93);
			nudEVDefence.Size = new Size(120, 20);
			nudEVDefence.ValueChanged += new EventHandler(recalculate);
			newPage.EVDefence = nudEVDefence;
			EVs.Controls.Add(nudEVDefence);

			Button Iron = new Button();
			Iron.Text = "Iron";
			Iron.Location = new Point(208, 90);
			Iron.Size = new Size(75, 23);
			Iron.Click += new EventHandler(btnIron_Click);
			EVs.Controls.Add(Iron);

			Button Qualot = new Button();
			Qualot.Text = "Qualot Berry";
			Qualot.Location = new Point(289, 90);
			Qualot.Size = new Size(84, 23);
			Qualot.Click += new EventHandler(btnQualot_Click);
			EVs.Controls.Add(Qualot);
			#endregion

			#region SpecialAttack
			Label evSpecialAttack = new Label();
			evSpecialAttack.Location = new Point(6, 121);
			evSpecialAttack.Size = new Size(50, 13);
			evSpecialAttack.Text = "SpecialAttack";
			EVs.Controls.Add(evSpecialAttack);

			NumericUpDown nudEVSpecialAttack = new NumericUpDown();
			nudEVSpecialAttack.Maximum = 255;
			nudEVSpecialAttack.Location = new Point(82, 119);
			nudEVSpecialAttack.Size = new Size(120, 20);
			nudEVSpecialAttack.ValueChanged += new EventHandler(recalculate);
			newPage.EVSpecialAttack = nudEVSpecialAttack;
			EVs.Controls.Add(nudEVSpecialAttack);

			Button Calcium = new Button();
			Calcium.Text = "Calcium";
			Calcium.Location = new Point(208, 116);
			Calcium.Size = new Size(75, 23);
			Calcium.Click += new EventHandler(btnCalcium_Click);
			EVs.Controls.Add(Calcium);

			Button Hondew = new Button();
			Hondew.Text = "Hondew Berry";
			Hondew.Location = new Point(289, 116);
			Hondew.Size = new Size(84, 23);
			Hondew.Click += new EventHandler(btnHondew_Click);
			EVs.Controls.Add(Hondew);
			#endregion

			#region SpecialDefence
			Label evSpecialDefence = new Label();
			evSpecialDefence.Location = new Point(6, 147);
			evSpecialDefence.Size = new Size(50, 13);
			evSpecialDefence.Text = "SpecialDefence";
			EVs.Controls.Add(evSpecialDefence);

			NumericUpDown nudEVSpecialDefence = new NumericUpDown();
			nudEVSpecialDefence.Maximum = 255;
			nudEVSpecialDefence.Location = new Point(82, 145);
			nudEVSpecialDefence.Size = new Size(120, 20);
			nudEVSpecialDefence.ValueChanged += new EventHandler(recalculate);
			newPage.EVSpecialDefence = nudEVSpecialDefence;
			EVs.Controls.Add(nudEVSpecialDefence);

			Button Zinc = new Button();
			Zinc.Text = "Zinc";
			Zinc.Location = new Point(208, 142);
			Zinc.Size = new Size(75, 23);
			Zinc.Click += new EventHandler(btnZinc_Click);
			EVs.Controls.Add(Zinc);

			Button Grepa = new Button();
			Grepa.Text = "Grepa Berry";
			Grepa.Location = new Point(289, 142);
			Grepa.Size = new Size(84, 23);
			Grepa.Click += new EventHandler(btnGrepa_Click);
			EVs.Controls.Add(Grepa);
			#endregion

			#region Speed
			Label evSpeed = new Label();
			evSpeed.Location = new Point(6, 173);
			evSpeed.Size = new Size(50, 13);
			evSpeed.Text = "Speed";
			EVs.Controls.Add(evSpeed);

			NumericUpDown nudEVSpeed = new NumericUpDown();
			nudEVSpeed.Maximum = 255;
			nudEVSpeed.Location = new Point(82, 171);
			nudEVSpeed.Size = new Size(120, 20);
			nudEVSpeed.ValueChanged += new EventHandler(recalculate);
			newPage.EVSpeed = nudEVSpeed;
			EVs.Controls.Add(nudEVSpeed);

			Button Carbos = new Button();
			Carbos.Text = "Carbos";
			Carbos.Location = new Point(208, 168);
			Carbos.Size = new Size(75, 23);
			Carbos.Click += new EventHandler(btnCarbos_Click);
			EVs.Controls.Add(Carbos);

			Button Tamato = new Button();
			Tamato.Text = "Tamato Berry";
			Tamato.Location = new Point(289, 168);
			Tamato.Size = new Size(84, 23);
			Tamato.Click += new EventHandler(btnTamato_Click);
			EVs.Controls.Add(Tamato);
			#endregion

			Label warning = new Label();
			warning.Location = new Point(9, 212);
			warning.Width = 200;
			EVs.Controls.Add(warning);
			newPage.Warning = warning;
			#endregion

			return newTab;
		}

		public static string GetEnumDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
				return attributes[0].Description;
			else
				return value.ToString();
		}

		private void deletePokemonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (tabControl1.TabPages.Count < 1) return;
			int page = tabControl1.SelectedIndex;
			tabControl1.TabPages.RemoveAt(page);
			tabControl1.SelectedIndex = Math.Min(tabControl1.TabPages.Count - 1, page);
			recalculate(null, null);
		}

		private string saveLocation = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EVTrackerSave.evt";
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
					p.Species = Manager.GetPokemonType(p.Species.DexNumber);
					p.Nature = Manager.GetNature(p.Nature.Name);
					TabPage newTab = CreateTabPage();
					Page page = ((Page)newTab.Tag);
					page.Pokemon = p;
					page.Species.SelectedIndex = page.Species.Items.IndexOf(p.Species);
					page.Level.Value = p.Level;
					page.Nature.SelectedIndex = page.Nature.Items.IndexOf(p.Nature);
					page.Pokerus.Checked = p.HasPokerus;
					page.HeldItem.SelectedIndex = page.HeldItem.Items.IndexOf(GetEnumDescription(p.HeldItem));

					page.EVAttack.Value = p.EV[Stat.Attack];
					page.EVDefence.Value = p.EV[Stat.Defence];
					page.EVHP.Value = p.EV[Stat.HP];
					page.EVSpecialAttack.Value = p.EV[Stat.SpecialAttack];
					page.EVSpecialDefence.Value = p.EV[Stat.SpecialDefence];
					page.EVSpeed.Value = p.EV[Stat.Speed];
					page.IVAttack.Value = p.IV[Stat.Attack];
					page.IVDefence.Value = p.IV[Stat.Defence];
					page.IVHP.Value = p.IV[Stat.HP];
					page.IVSpecialAttack.Value = p.IV[Stat.SpecialAttack];
					page.IVSpecialDefence.Value = p.IV[Stat.SpecialDefence];
					page.IVSpeed.Value = p.IV[Stat.Speed];

					tabControl1.TabPages.Add(newTab);
				});
				current = ((Page)tabControl1.SelectedTab.Tag);
				recalculate(null, null);
			}
			catch
			{
				//This is when the  file is invalid
				MessageBox.Show("The Save File is Invalid, load failed");
			}
		}

	}
}