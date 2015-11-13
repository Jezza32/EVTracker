using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using EVTracker.Properties;
using static System.Double;

namespace EVTracker
{
	public class Page
	{
	    public Page(Pokemon pokemon, IDictionary<int, PokemonType> pokemonTypes, IDictionary<string, Nature> natures)
	    {
	        Pokemon = pokemon;
            StatLabels = new Dictionary<Stat, Label>();

	        TabPage = new TabPage
	        {
	            Tag = this,
	            BackColor = Color.White,
	            Text = Pokemon.Species.Name
	        };

	        #region Pokemon Details
            Species = new ComboBox();
            Species.Items.AddRange(pokemonTypes.Values.ToArray<object>());
            Species.SelectedIndex = 0;
            Species.DropDownStyle = ComboBoxStyle.DropDownList;
            Species.SelectedIndexChanged += cmbPok_SelectedIndexChanged;
            Species.Location = new Point(15, 23);
            Species.Size = new Size(146, 21);
            TabPage.Controls.Add(Species);

            //Nature
            Nature = new ComboBox();
            Nature.Items.AddRange(natures.Values.ToArray<object>());
            Nature.SelectedIndex = 0;
            Nature.DropDownStyle = ComboBoxStyle.DropDownList;
            Nature.Location = new Point(Species.Right + 20, Species.Top);
            Nature.SelectedIndexChanged += (o, args) => { Pokemon.Nature = (Nature) Nature.SelectedItem; UpdateForm(); };
            TabPage.Controls.Add(Nature);

            //Pokerus
	        Pokerus = new CheckBox
	        {
	            Text = Resources.Pokerus,
	            Location = new Point(Nature.Right + 20, Species.Top)
	        };
	        Pokerus.CheckedChanged += (o, args) => { Pokemon.HasPokerus = Pokerus.Checked; UpdateForm(); };
            TabPage.Controls.Add(Pokerus);

            //HeldItem
	        HeldItem = new ComboBox {FormattingEnabled = true};
	        HeldItem.Format += (sender, args) =>
            {
                var val = args.Value as Items?;
                args.Value = val.HasValue ? val.Value.GetName() : "";
            };
            HeldItem.Location = new Point(Pokerus.Left, Pokerus.Bottom + 20);
            foreach (Items i in Enum.GetValues(typeof(Items))) HeldItem.Items.Add(i);
            HeldItem.DropDownStyle = ComboBoxStyle.DropDownList;
            HeldItem.SelectedIndex = 0;
            HeldItem.SelectedIndexChanged += (o, args) => { Pokemon.HeldItem = (Items) HeldItem.SelectedIndex; UpdateForm(); };
            TabPage.Controls.Add(HeldItem);

	        var level = new Label
	        {
	            Text = Resources.Level,
	            Location = new Point(11, 64),
	            Size = new Size(33, 13)
	        };
	        TabPage.Controls.Add(level);

	        Level = new NumericUpDown
	        {
	            Minimum = 1,
	            Value = 1,
	            Maximum = 100,
	            Location = new Point(50, 62),
	            Size = new Size(61, 20)
	        };
	        Level.ValueChanged += (o, args) => { Pokemon.Level = (int) Level.Value; UpdateForm(); };
            TabPage.Controls.Add(Level);

	        Image = new PictureBox
	        {
	            Location = new Point(449, 11),
	            Size = new Size(80, 80),
	            SizeMode = PictureBoxSizeMode.AutoSize
	        };
            TabPage.Controls.Add(Image);

	        var hp = new Label
	        {
	            Text = Resources.HP,
	            Location = new Point(29, 116),
	            Size = new Size(22, 13)
	        };
	        TabPage.Controls.Add(hp); StatLabels[Stat.HP] = hp;
	        var att = new Label
	        {
	            Text = Resources.Attack,
	            Location = new Point(110, 116),
	            Size = new Size(38, 13)
	        };
	        TabPage.Controls.Add(att); StatLabels[Stat.Attack] = att;
	        var def = new Label
	        {
	            Text = Resources.Defence,
	            Location = new Point(203, 116),
	            Size = new Size(50, 13)
	        };
	        TabPage.Controls.Add(def); StatLabels[Stat.Defence] = def;
	        var satt = new Label
	        {
	            Text = Resources.SpAttack,
	            Location = new Point(296, 116),
	            Size = new Size(57, 13)
	        };
	        TabPage.Controls.Add(satt); StatLabels[Stat.SpecialAttack] = satt;
	        var sdef = new Label
	        {
	            Text = Resources.SpDefence,
	            Location = new Point(389, 116),
	            Size = new Size(70, 13)
	        };
	        TabPage.Controls.Add(sdef); StatLabels[Stat.SpecialDefence] = sdef;
	        var spe = new Label
	        {
	            Text = Resources.Speed,
	            Location = new Point(482, 116),
	            Size = new Size(38, 13)
	        };
	        TabPage.Controls.Add(spe); StatLabels[Stat.Speed] = spe;

            #region BaseStats

	        var groupStats = new GroupBox
	        {
	            Text = Resources.BaseStats,
	            Location = new Point(14, 132),
	            Size = new Size(529, 46)
	        };
	        TabPage.Controls.Add(groupStats);

	        BaseStatHP = new Label
	        {
	            Location = new Point(15, 21),
	            Size = new Size(35, 13)
	        };
	        groupStats.Controls.Add(BaseStatHP);
	        BaseStatAttack = new Label
	        {
	            Location = new Point(96, 21),
	            Size = new Size(35, 13)
	        };
	        groupStats.Controls.Add(BaseStatAttack);
	        BaseStatDefence = new Label
	        {
	            Location = new Point(189, 21),
	            Size = new Size(35, 13)
	        };
	        groupStats.Controls.Add(BaseStatDefence);
	        BaseStatSpecialAttack = new Label
	        {
	            Location = new Point(282, 21),
	            Size = new Size(35, 13)
	        };
	        groupStats.Controls.Add(BaseStatSpecialAttack);
	        BaseStatSpecialDefence = new Label
	        {
	            Location = new Point(375, 21),
	            Size = new Size(35, 13)
	        };
	        groupStats.Controls.Add(BaseStatSpecialDefence);
	        BaseStatSpeed = new Label
	        {
	            Location = new Point(468, 21),
	            Size = new Size(35, 13)
	        };
	        groupStats.Controls.Add(BaseStatSpeed);
            #endregion

            #region IVs

	        GroupBox groupIVs = new GroupBox
	        {
	            Text = @"IVs",
	            Location = new Point(14, 193),
	            Size = new Size(529, 46)
	        };
	        TabPage.Controls.Add(groupIVs);

	        IVHP = new NumericUpDown
	        {
	            Location = new Point(15, 21),
	            Size = new Size(56, 20)
	        };
	        groupIVs.Controls.Add(IVHP); IVHP.ValueChanged += (o, args) => { Pokemon.IV[Stat.HP] = (int) IVHP.Value; UpdateForm(); }; IVHP.Maximum = 31;
	        IVAttack = new NumericUpDown
	        {
	            Location = new Point(96, 21),
	            Size = new Size(56, 20)
	        };
	        groupIVs.Controls.Add(IVAttack); IVAttack.ValueChanged += (o, args) => { Pokemon.IV[Stat.Attack] = (int)IVAttack.Value; UpdateForm(); }; IVAttack.Maximum = 31;
	        IVDefence = new NumericUpDown
	        {
	            Location = new Point(189, 21),
	            Size = new Size(56, 20)
	        };
	        groupIVs.Controls.Add(IVDefence); IVDefence.ValueChanged += (o, args) => { Pokemon.IV[Stat.Defence] = (int)IVDefence.Value; UpdateForm(); }; IVDefence.Maximum = 31;
	        IVSpecialAttack = new NumericUpDown
	        {
	            Location = new Point(282, 21),
	            Size = new Size(56, 20)
	        };
	        groupIVs.Controls.Add(IVSpecialAttack); IVSpecialAttack.ValueChanged += (o, args) => { Pokemon.IV[Stat.SpecialAttack] = (int)IVSpecialAttack.Value; UpdateForm();}; IVSpecialAttack.Maximum = 31;
	        IVSpecialDefence = new NumericUpDown
	        {
	            Location = new Point(375, 21),
	            Size = new Size(56, 20)
	        };
	        groupIVs.Controls.Add(IVSpecialDefence); IVSpecialDefence.ValueChanged += (o, args) => { Pokemon.IV[Stat.SpecialDefence] = (int)IVSpecialDefence.Value; UpdateForm();}; IVSpecialDefence.Maximum = 31;
	        IVSpeed = new NumericUpDown
	        {
	            Location = new Point(468, 21),
	            Size = new Size(56, 20)
	        };
	        groupIVs.Controls.Add(IVSpeed); IVSpeed.ValueChanged += (o, args) => { Pokemon.IV[Stat.Speed] = (int)IVSpeed.Value; UpdateForm();}; IVSpeed.Maximum = 31;
            #endregion


            #region Actual Stats

	        var groupActuals = new GroupBox
	        {
	            Text = Resources.ActualStats,
	            Location = new Point(14, 262),
	            Size = new Size(529, 46)
	        };
	        TabPage.Controls.Add(groupActuals);

	        ActualStatHP = new Label
	        {
	            Location = new Point(15, 21),
	            Size = new Size(35, 13)
	        };
	        groupActuals.Controls.Add(ActualStatHP);
	        ActualStatAttack = new Label
	        {
	            Location = new Point(96, 21),
	            Size = new Size(35, 13)
	        };
	        groupActuals.Controls.Add(ActualStatAttack);
	        ActualStatDefence = new Label
	        {
	            Location = new Point(189, 21),
	            Size = new Size(35, 13)
	        };
	        groupActuals.Controls.Add(ActualStatDefence);
	        ActualStatSpecialAttack = new Label
	        {
	            Location = new Point(282, 21),
	            Size = new Size(35, 13)
	        };
	        groupActuals.Controls.Add(ActualStatSpecialAttack);
	        ActualStatSpecialDefence = new Label
	        {
	            Location = new Point(375, 21),
	            Size = new Size(35, 13)
	        };
	        groupActuals.Controls.Add(ActualStatSpecialDefence);
	        ActualStatSpeed = new Label
	        {
	            Location = new Point(468, 21),
	            Size = new Size(35, 13)
	        };
	        groupActuals.Controls.Add(ActualStatSpeed);
            #endregion
            #endregion

            #region EVs

	        var eVs = new GroupBox
	        {
	            Location = new Point(550, 0),
	            Height = 300,
	            Width = 420,
	            Text = @"EVs"
	        };
	        TabPage.Controls.Add(eVs);

            #region HP

	        var evHP = new Label
	        {
	            Location = new Point(6, 43),
	            Size = new Size(22, 13),
	            Text = Resources.HP
	        };
	        eVs.Controls.Add(evHP);

	        EVHP = new NumericUpDown
	        {
	            Maximum = 255,
	            Location = new Point(82, 41),
	            Size = new Size(120, 20)
	        };
	        EVHP.ValueChanged += (o, args) => { Pokemon.EV[Stat.HP] = (int) EVHP.Value; UpdateForm(); };
            eVs.Controls.Add(EVHP);

	        var hpUp = new Button
	        {
	            Text = Resources.HPUp,
	            Location = new Point(208, 38),
	            Size = new Size(75, 23)
	        };
	        hpUp.Click += btnHPUp_Click;
            eVs.Controls.Add(hpUp);

	        var pomeg = new Button
	        {
	            Text = Resources.PomegBerry,
	            Location = new Point(289, 38),
	            Size = new Size(84, 23)
	        };
	        pomeg.Click += btnPomeg_Click;
            eVs.Controls.Add(pomeg);
            #endregion

            #region Attack

	        var evAttack = new Label
	        {
	            Location = new Point(6, 69),
	            Size = new Size(50, 13),
	            Text = Resources.Attack
	        };
	        eVs.Controls.Add(evAttack);

            EVAttack = new NumericUpDown
	        {
	            Maximum = 255,
	            Location = new Point(82, 67),
	            Size = new Size(120, 20)
	        };
            EVAttack.ValueChanged += (o, args) => { Pokemon.EV[Stat.Attack] = (int) EVAttack.Value; UpdateForm(); };
            eVs.Controls.Add(EVAttack);

	        var protein = new Button
	        {
	            Text = Resources.Protein,
	            Location = new Point(208, 64),
	            Size = new Size(75, 23)
	        };
	        protein.Click += btnProtein_Click;
            eVs.Controls.Add(protein);

	        var kelpsy = new Button
	        {
	            Text = Resources.KelpsyBerry,
	            Location = new Point(289, 64),
	            Size = new Size(84, 23)
	        };
	        kelpsy.Click += btnKelpsy_Click;
            eVs.Controls.Add(kelpsy);
            #endregion

            #region Defence

	        var evDefence = new Label
	        {
	            Location = new Point(6, 95),
	            Size = new Size(50, 13),
	            Text = Resources.Defence
	        };
	        eVs.Controls.Add(evDefence);

            EVDefence = new NumericUpDown
	        {
	            Maximum = 255,
	            Location = new Point(82, 93),
	            Size = new Size(120, 20)
	        };
            EVDefence.ValueChanged += (o, args) => { Pokemon.EV[Stat.Defence] = (int) EVDefence.Value; UpdateForm(); };
            eVs.Controls.Add(EVDefence);

	        var iron = new Button
	        {
	            Text = Resources.Iron,
	            Location = new Point(208, 90),
	            Size = new Size(75, 23)
	        };
	        iron.Click += btnIron_Click;
            eVs.Controls.Add(iron);

	        var qualot = new Button
	        {
	            Text = Resources.QualotBerry,
	            Location = new Point(289, 90),
	            Size = new Size(84, 23)
	        };
	        qualot.Click += btnQualot_Click;
            eVs.Controls.Add(qualot);
            #endregion

            #region SpecialAttack

	        var evSpecialAttack = new Label
	        {
	            Location = new Point(6, 121),
	            Size = new Size(60, 13),
	            Text = Resources.SpAttack
	        };
	        eVs.Controls.Add(evSpecialAttack);

            EVSpecialAttack = new NumericUpDown
	        {
	            Maximum = 255,
	            Location = new Point(82, 119),
	            Size = new Size(120, 20)
	        };
            EVSpecialAttack.ValueChanged += (o, args) =>
            {
                Pokemon.EV[Stat.SpecialAttack] = (int) EVSpecialAttack.Value; UpdateForm();
            };
            eVs.Controls.Add(EVSpecialAttack);

	        var calcium = new Button
	        {
	            Text = Resources.Calcium,
	            Location = new Point(208, 116),
	            Size = new Size(75, 23)
	        };
	        calcium.Click += btnCalcium_Click;
            eVs.Controls.Add(calcium);

	        var hondew = new Button
	        {
	            Text = Resources.HondewBerry,
	            Location = new Point(289, 116),
	            Size = new Size(84, 23)
	        };
	        hondew.Click += btnHondew_Click;
            eVs.Controls.Add(hondew);
            #endregion

            #region SpecialDefence

	        var evSpecialDefence = new Label
	        {
	            Location = new Point(6, 147),
	            Size = new Size(70, 13),
	            Text = Resources.SpDefence
	        };
	        eVs.Controls.Add(evSpecialDefence);

            EVSpecialDefence = new NumericUpDown
	        {
	            Maximum = 255,
	            Location = new Point(82, 145),
	            Size = new Size(120, 20)
	        };
            EVSpecialDefence.ValueChanged += (o, args) =>
            {
                Pokemon.EV[Stat.SpecialDefence] = (int) EVSpecialDefence.Value; UpdateForm();
            };
            eVs.Controls.Add(EVSpecialDefence);

	        var zinc = new Button
	        {
	            Text = Resources.Zinc,
	            Location = new Point(208, 142),
	            Size = new Size(75, 23)
	        };
	        zinc.Click += btnZinc_Click;
            eVs.Controls.Add(zinc);

	        var grepa = new Button
	        {
	            Text = Resources.GrepaBerry,
	            Location = new Point(289, 142),
	            Size = new Size(84, 23)
	        };
	        grepa.Click += btnGrepa_Click;
            eVs.Controls.Add(grepa);
            #endregion

            #region Speed

	        var evSpeed = new Label
	        {
	            Location = new Point(6, 173),
	            Size = new Size(50, 13),
	            Text = Resources.Speed
	        };
	        eVs.Controls.Add(evSpeed);

            EVSpeed = new NumericUpDown
	        {
	            Maximum = 255,
	            Location = new Point(82, 171),
	            Size = new Size(120, 20)
	        };
            EVSpeed.ValueChanged += (o, args) => { Pokemon.EV[Stat.Speed] = (int) EVSpeed.Value; UpdateForm(); };
            eVs.Controls.Add(EVSpeed);

	        var carbos = new Button
	        {
	            Text = Resources.Carbos,
	            Location = new Point(208, 168),
	            Size = new Size(75, 23)
	        };
	        carbos.Click += btnCarbos_Click;
            eVs.Controls.Add(carbos);

	        var tamato = new Button
	        {
	            Text = Resources.TamatoBerry,
	            Location = new Point(289, 168),
	            Size = new Size(84, 23)
	        };
	        tamato.Click += btnTamato_Click;
            eVs.Controls.Add(tamato);
            #endregion

            Warning = new Label
	        {
	            Location = new Point(9, 212),
	            Width = 200
	        };
	        eVs.Controls.Add(Warning);
            #endregion

            UpdateForm();
        }
		public Pokemon Pokemon { get; set; }

		public IDictionary<Stat, Label> StatLabels { get; set; }

		public TabPage TabPage { get; set; }
		public PictureBox Image { get; set; }
		public NumericUpDown Level { get; set; }
		public CheckBox Pokerus { get; set; }
		public ComboBox HeldItem { get; set; }
		public ComboBox Nature { get; set; }

		public Label BaseStatHP { get; set; }
		public Label BaseStatAttack { get; set; }
		public Label BaseStatDefence { get; set; }
		public Label BaseStatSpecialAttack { get; set; }
		public Label BaseStatSpecialDefence { get; set; }
		public Label BaseStatSpeed { get; set; }

		public NumericUpDown IVHP { get; set; }
		public NumericUpDown IVAttack { get; set; }
		public NumericUpDown IVDefence { get; set; }
		public NumericUpDown IVSpecialAttack { get; set; }
		public NumericUpDown IVSpecialDefence { get; set; }
		public NumericUpDown IVSpeed { get; set; }

		public Label ActualStatHP { get; set; }
		public Label ActualStatAttack { get; set; }
		public Label ActualStatDefence { get; set; }
		public Label ActualStatSpecialAttack { get; set; }
		public Label ActualStatSpecialDefence { get; set; }
		public Label ActualStatSpeed { get; set; }

		public NumericUpDown EVHP { get; set; }
		public NumericUpDown EVAttack { get; set; }
		public NumericUpDown EVDefence { get; set; }
		public NumericUpDown EVSpecialAttack { get; set; }
		public NumericUpDown EVSpecialDefence { get; set; }
		public NumericUpDown EVSpeed { get; set; }

		public Label Warning { get; set; }

		public ComboBox Species { get; set; }

	    public void UpdateStat(Stat stat, int statIncrease)
	    {
	        var items = HeldItem.SelectedItem as Items?;
	        Pokemon.EV[stat] = Math.Min(statIncrease * (items.HasValue && items.Value == Items.MachoBrace ? 2 : 1) * (Pokerus.Checked ? 2 : 1) + Pokemon.EV[stat], 255);
	        UpdateForm();
	    }

	    public void ApplyItem(Items item)
	    {
	        switch (item)
	        {
				case Items.PowerWeight:
					Pokemon.EV[Stat.HP] = Math.Min(255, Pokemon.EV[Stat.HP] + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerBracer:
					Pokemon.EV[Stat.Attack] = Math.Min(255, Pokemon.EV[Stat.Attack] + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerBelt:
					Pokemon.EV[Stat.Defence] = Math.Min(255, Pokemon.EV[Stat.Defence] + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerLens:
					Pokemon.EV[Stat.SpecialAttack] = Math.Min(255, Pokemon.EV[Stat.SpecialAttack] + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerBand:
					Pokemon.EV[Stat.SpecialDefence] = Math.Min(255, Pokemon.EV[Stat.SpecialDefence] + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerAnklet:
					Pokemon.EV[Stat.Speed] = Math.Min(255, Pokemon.EV[Stat.Speed] + (Pokerus.Checked ? 8 : 4));
                    break;
            }
            UpdateForm();
	    }


	    private void btnHPUp_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.HP];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            Pokemon.EV[Stat.HP] = value;
            UpdateForm();
        }

        private void btnProtein_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.Attack];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            Pokemon.EV[Stat.Attack] = value;
            UpdateForm();
        }

        private void btnIron_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.Defence];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            Pokemon.EV[Stat.Defence] = value;
            UpdateForm();
        }

        private void btnCalcium_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.SpecialAttack];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            Pokemon.EV[Stat.SpecialAttack] = value;
            UpdateForm();
        }

        private void btnZinc_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.SpecialDefence];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            Pokemon.EV[Stat.SpecialDefence] = value;
            UpdateForm();
        }

        private void btnCarbos_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.Speed];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            Pokemon.EV[Stat.Speed] = value;
            UpdateForm();
        }

        private void btnPomeg_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.HP];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            Pokemon.EV[Stat.HP] = value;
            UpdateForm();
        }

        private void btnKelpsy_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.Attack];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            Pokemon.EV[Stat.Attack] = value;
            UpdateForm();
        }

        private void btnQualot_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.Defence];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            Pokemon.EV[Stat.Defence] = value;
            UpdateForm();
        }

        private void btnHondew_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.SpecialAttack];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            Pokemon.EV[Stat.SpecialAttack] = value;
            UpdateForm();
        }

        private void btnGrepa_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.SpecialDefence];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            Pokemon.EV[Stat.SpecialDefence] = value;
            UpdateForm();
        }

        private void btnTamato_Click(object sender, EventArgs e)
        {
            var value = Pokemon.EV[Stat.Speed];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            Pokemon.EV[Stat.Speed] = value;
            UpdateForm();
        }

        private void cmbPok_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (PokemonType)Species.SelectedItem;
            if (selectedItem == Pokemon.Species) return;

            Pokemon.Species = selectedItem;
            UpdateForm();
        }

        private void UpdateForm()
        {
            foreach (Stat s in Enum.GetValues(typeof(Stat)))
            {
                var modifier = Pokemon.Nature.GetModifier(s);
                if (modifier < 1) StatLabels[s].ForeColor = Color.Red;
                else if (Math.Abs(modifier - 1) < Epsilon) StatLabels[s].ForeColor = Color.Black;
                else StatLabels[s].ForeColor = Color.Green;
            }

            var selectedSpecies = Species.Items.Cast<PokemonType>().Where(s => s.Name == Pokemon.Species.Name).ToList();
            if (selectedSpecies.Any())
            {
                Species.SelectedItem = selectedSpecies.First();
            }
            Level.Value = Pokemon.Level;
            var selectedNature = Nature.Items.Cast<Nature>().Where(n => n.Name == Pokemon.Nature.Name).ToList();
            if (selectedNature.Any())
            {
                Nature.SelectedItem = selectedNature.First();
            }
            Pokerus.Checked = Pokemon.HasPokerus;
            var selectedItem = HeldItem.Items.Cast<Items>().Where(i => i == Pokemon.HeldItem).ToList();
            if (selectedItem.Any())
            {
                HeldItem.SelectedItem = selectedItem.First();
            }

            EVAttack.Value = Pokemon.EV[Stat.Attack];
            EVDefence.Value = Pokemon.EV[Stat.Defence];
            EVHP.Value = Pokemon.EV[Stat.HP];
            EVSpecialAttack.Value = Pokemon.EV[Stat.SpecialAttack];
            EVSpecialDefence.Value = Pokemon.EV[Stat.SpecialDefence];
            EVSpeed.Value = Pokemon.EV[Stat.Speed];
            IVAttack.Value = Pokemon.IV[Stat.Attack];
            IVDefence.Value = Pokemon.IV[Stat.Defence];
            IVHP.Value = Pokemon.IV[Stat.HP];
            IVSpecialAttack.Value = Pokemon.IV[Stat.SpecialAttack];
            IVSpecialDefence.Value = Pokemon.IV[Stat.SpecialDefence];
            IVSpeed.Value = Pokemon.IV[Stat.Speed];
            BaseStatHP.Text = Pokemon.Species.BaseStats[Stat.HP].ToString();
            BaseStatAttack.Text = Pokemon.Species.BaseStats[Stat.Attack].ToString();
            BaseStatDefence.Text = Pokemon.Species.BaseStats[Stat.Defence].ToString();
            BaseStatSpecialAttack.Text = Pokemon.Species.BaseStats[Stat.SpecialAttack].ToString();
            BaseStatSpecialDefence.Text = Pokemon.Species.BaseStats[Stat.SpecialDefence].ToString();
            BaseStatSpeed.Text = Pokemon.Species.BaseStats[Stat.Speed].ToString();
            ActualStatHP.Text = Pokemon.HP.ToString();
            ActualStatAttack.Text = Pokemon.Attack.ToString();
            ActualStatDefence.Text = Pokemon.Defence.ToString();
            ActualStatSpecialAttack.Text = Pokemon.SpecialAttack.ToString();
            ActualStatSpecialDefence.Text = Pokemon.SpecialDefence.ToString();
            ActualStatSpeed.Text = Pokemon.Speed.ToString();

            TabPage.Text = Pokemon.Species.Name;

            var num = Pokemon.Species.DexNumber;
            var location = "_" + num.ToString().PadLeft(3, '0');

            Image.Image = (Image)Resources.ResourceManager.GetObject(location);

            var ev = 0;
            ev += (int)EVHP.Value;
            ev += (int)EVAttack.Value;
            ev += (int)EVDefence.Value;
            ev += (int)EVSpecialAttack.Value;
            ev += (int)EVSpecialDefence.Value;
            ev += (int)EVSpeed.Value;

            int remaining = 510 - ev;
            Warning.ForeColor = (remaining >= 0 ? Color.Black : Color.Red);
            Warning.Text = (remaining >= 0 ? "You have " + remaining + " EVs left" : "You are " + (remaining * -1) + " EVs over");
        }
	}
}
