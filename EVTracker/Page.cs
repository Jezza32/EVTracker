using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EVTracker
{
	public class Page
	{
	    public Page(IDictionary<int, PokemonType> pokemonTypes, IDictionary<string, Nature> natures)
	    {
	        Pokemon = new Pokemon();
            StatLabels = new Dictionary<Stat, Label>();
            
            TabPage = new TabPage();
	        TabPage.Tag = this;
            TabPage.BackColor = Color.White;
            TabPage.Text = Pokemon.Species.Name;

            #region Pokemon Details
            ComboBox pok = new ComboBox();
            pok.Items.AddRange(pokemonTypes.Values.ToArray<object>());
            pok.SelectedIndex = 0;
            pok.DropDownStyle = ComboBoxStyle.DropDownList;
            pok.SelectedIndexChanged += new EventHandler(cmbPok_SelectedIndexChanged);
            pok.Location = new Point(15, 23);
            pok.Size = new Size(146, 21);
            TabPage.Controls.Add(pok);
            Species = pok;

            //Nature
            ComboBox nat = new ComboBox();
            nat.Items.AddRange(natures.Values.ToArray<object>());
            nat.SelectedIndex = 0;
            nat.DropDownStyle = ComboBoxStyle.DropDownList;
            nat.Location = new Point(pok.Right + 20, pok.Top);
            nat.SelectedIndexChanged += (o, args) => recalculate();
            TabPage.Controls.Add(nat);
            Nature = nat;

            //Pokerus
            CheckBox rus = new CheckBox();
            rus.Text = "Pokerus";
            rus.Location = new Point(nat.Right + 20, pok.Top);
            rus.CheckedChanged += (o, args) => recalculate();
            TabPage.Controls.Add(rus);
            Pokerus = rus;

            //HeldItem
            ComboBox held = new ComboBox();
            held.FormattingEnabled = true;
            held.Format += (sender, args) =>
            {
                var val = args.Value as Items?;
                args.Value = val.HasValue ? val.Value.GetName() : "";
            };
            held.Location = new Point(rus.Left, rus.Bottom + 20);
            TabPage.Controls.Add(held);
            foreach (Items i in Enum.GetValues(typeof(Items))) held.Items.Add(i);
            HeldItem = held;
            held.DropDownStyle = ComboBoxStyle.DropDownList;
            held.SelectedIndex = 0;
            held.SelectedIndexChanged += (o, args) => recalculate();

            Label level = new Label();
            level.Text = "Level";
            level.Location = new Point(11, 64);
            level.Size = new Size(33, 13);
            TabPage.Controls.Add(level);

            NumericUpDown nudLevel = new NumericUpDown();
            nudLevel.Minimum = 1;
            nudLevel.Value = 1;
            nudLevel.Maximum = 100;
            nudLevel.Location = new Point(50, 62);
            nudLevel.Size = new Size(61, 20);
            nudLevel.ValueChanged += (o, args) => recalculate();
            Level = nudLevel;
            TabPage.Controls.Add(nudLevel);

            PictureBox pic = new PictureBox();
            pic.Location = new Point(449, 11);
            pic.Size = new Size(80, 80);
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            Image = pic;
            TabPage.Controls.Add(pic);

            Label hp = new Label(); hp.Text = "HP"; hp.Location = new Point(29, 116); hp.Size = new Size(22, 13); TabPage.Controls.Add(hp); StatLabels[Stat.HP] = hp;
            Label att = new Label(); att.Text = "Attack"; att.Location = new Point(110, 116); att.Size = new Size(38, 13); TabPage.Controls.Add(att); StatLabels[Stat.Attack] = att;
            Label def = new Label(); def.Text = "Defence"; def.Location = new Point(203, 116); def.Size = new Size(50, 13); TabPage.Controls.Add(def); StatLabels[Stat.Defence] = def;
            Label satt = new Label(); satt.Text = "Sp. Attack"; satt.Location = new Point(296, 116); satt.Size = new Size(57, 13); TabPage.Controls.Add(satt); StatLabels[Stat.SpecialAttack] = satt;
            Label sdef = new Label(); sdef.Text = "Sp. Defence"; sdef.Location = new Point(389, 116); sdef.Size = new Size(70, 13); TabPage.Controls.Add(sdef); StatLabels[Stat.SpecialDefence] = sdef;
            Label spe = new Label(); spe.Text = "Speed"; spe.Location = new Point(482, 116); spe.Size = new Size(38, 13); TabPage.Controls.Add(spe); StatLabels[Stat.Speed] = spe;

            #region BaseStats
            GroupBox groupStats = new GroupBox();
            groupStats.Text = "Base Stats";
            groupStats.Location = new Point(14, 132);
            groupStats.Size = new Size(529, 46);
            TabPage.Controls.Add(groupStats);

            Label baseHP = new Label(); baseHP.Location = new Point(15, 21); baseHP.Size = new Size(35, 13); groupStats.Controls.Add(baseHP); BaseStatHP = baseHP;
            Label baseAtt = new Label(); baseAtt.Location = new Point(96, 21); baseAtt.Size = new Size(35, 13); groupStats.Controls.Add(baseAtt); BaseStatAttack = baseAtt;
            Label baseDef = new Label(); baseDef.Location = new Point(189, 21); baseDef.Size = new Size(35, 13); groupStats.Controls.Add(baseDef); BaseStatDefence = baseDef;
            Label baseSAtt = new Label(); baseSAtt.Location = new Point(282, 21); baseSAtt.Size = new Size(35, 13); groupStats.Controls.Add(baseSAtt); BaseStatSpecialAttack = baseSAtt;
            Label baseSDef = new Label(); baseSDef.Location = new Point(375, 21); baseSDef.Size = new Size(35, 13); groupStats.Controls.Add(baseSDef); BaseStatSpecialDefence = baseSDef;
            Label baseSpe = new Label(); baseSpe.Location = new Point(468, 21); baseSpe.Size = new Size(35, 13); groupStats.Controls.Add(baseSpe); BaseStatSpeed = baseSpe;
            #endregion

            #region IVs
            GroupBox groupIVs = new GroupBox();
            groupIVs.Text = "IVs";
            groupIVs.Location = new Point(14, 193);
            groupIVs.Size = new Size(529, 46);
            TabPage.Controls.Add(groupIVs);

            IVHP = new NumericUpDown(); IVHP.Location = new Point(15, 21); IVHP.Size = new Size(56, 20); groupIVs.Controls.Add(IVHP); IVHP.ValueChanged += (o, args) => recalculate(); IVHP.Maximum = 31;
            IVAttack = new NumericUpDown(); IVAttack.Location = new Point(96, 21); IVAttack.Size = new Size(56, 20); groupIVs.Controls.Add(IVAttack); IVAttack.ValueChanged += (o, args) => recalculate(); IVAttack.Maximum = 31;
            IVDefence = new NumericUpDown(); IVDefence.Location = new Point(189, 21); IVDefence.Size = new Size(56, 20); groupIVs.Controls.Add(IVDefence); IVDefence.ValueChanged += (o, args) => recalculate(); IVDefence.Maximum = 31;
            IVSpecialAttack = new NumericUpDown(); IVSpecialAttack.Location = new Point(282, 21); IVSpecialAttack.Size = new Size(56, 20); groupIVs.Controls.Add(IVSpecialAttack); IVSpecialAttack.ValueChanged += (o, args) => recalculate(); IVSpecialAttack.Maximum = 31;
            IVSpecialDefence = new NumericUpDown(); IVSpecialDefence.Location = new Point(375, 21); IVSpecialDefence.Size = new Size(56, 20); groupIVs.Controls.Add(IVSpecialDefence); IVSpecialDefence.ValueChanged += (o, args) => recalculate(); IVSpecialDefence.Maximum = 31;
            IVSpeed = new NumericUpDown(); IVSpeed.Location = new Point(468, 21); IVSpeed.Size = new Size(56, 20); groupIVs.Controls.Add(IVSpeed); IVSpeed.ValueChanged += (o, args) => recalculate(); IVSpeed.Maximum = 31;
            #endregion


            #region Actual Stats
            GroupBox groupActuals = new GroupBox();
            groupActuals.Text = "Actual Stats";
            groupActuals.Location = new Point(14, 262);
            groupActuals.Size = new Size(529, 46);
            TabPage.Controls.Add(groupActuals);

            Label actualHP = new Label(); actualHP.Location = new Point(15, 21); actualHP.Size = new Size(35, 13); groupActuals.Controls.Add(actualHP); ActualStatHP = actualHP;
            Label actualAtt = new Label(); actualAtt.Location = new Point(96, 21); actualAtt.Size = new Size(35, 13); groupActuals.Controls.Add(actualAtt); ActualStatAttack = actualAtt;
            Label actualDef = new Label(); actualDef.Location = new Point(189, 21); actualDef.Size = new Size(35, 13); groupActuals.Controls.Add(actualDef); ActualStatDefence = actualDef;
            Label actualSAtt = new Label(); actualSAtt.Location = new Point(282, 21); actualSAtt.Size = new Size(35, 13); groupActuals.Controls.Add(actualSAtt); ActualStatSpecialAttack = actualSAtt;
            Label actualSDef = new Label(); actualSDef.Location = new Point(375, 21); actualSDef.Size = new Size(35, 13); groupActuals.Controls.Add(actualSDef); ActualStatSpecialDefence = actualSDef;
            Label actualSpe = new Label(); actualSpe.Location = new Point(468, 21); actualSpe.Size = new Size(35, 13); groupActuals.Controls.Add(actualSpe); ActualStatSpeed = actualSpe;
            #endregion
            #endregion

            #region EVs
            GroupBox EVs = new GroupBox();
            EVs.Location = new Point(550, 0);
            EVs.Height = 300;
            EVs.Width = 420;
            EVs.Text = "EVs";
            TabPage.Controls.Add(EVs);

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
            nudEVHP.ValueChanged += (o, args) => recalculate();
            EVHP = nudEVHP;
            EVs.Controls.Add(nudEVHP);

            Button HPUp = new Button();
            HPUp.Text = "HP Up";
            HPUp.Location = new Point(208, 38);
            HPUp.Size = new Size(75, 23);
            HPUp.Click += new EventHandler(btnHPUp_Click);
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
            nudEVAttack.ValueChanged += (o, args) => recalculate();
            EVAttack = nudEVAttack;
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
            nudEVDefence.ValueChanged += (o, args) => recalculate();
            EVDefence = nudEVDefence;
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
            nudEVSpecialAttack.ValueChanged += (o, args) => recalculate();
            EVSpecialAttack = nudEVSpecialAttack;
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
            nudEVSpecialDefence.ValueChanged += (o, args) => recalculate();
            EVSpecialDefence = nudEVSpecialDefence;
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
            nudEVSpeed.ValueChanged += (o, args) => recalculate();
            EVSpeed = nudEVSpeed;
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
            Warning = warning;
            #endregion

            _statNumericUpDowns = new Dictionary<Stat, NumericUpDown>
            {
                { Stat.HP, EVHP},
                { Stat.Attack, EVAttack},
                { Stat.Defence, EVDefence},
                { Stat.SpecialAttack, EVSpecialAttack},
                { Stat.SpecialDefence, EVSpecialDefence},
                { Stat.Speed, EVSpeed}
            };

            recalculate();
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

	    private readonly IDictionary<Stat, NumericUpDown> _statNumericUpDowns;

	    public void UpdateStat(Stat stat, int statIncrease)
	    {
	        UpdateStat(statIncrease, _statNumericUpDowns[stat]);
            recalculate();
	    }

	    public void ApplyItem(Items item)
	    {
	        switch (item)
	        {
				case Items.PowerWeight:
					EVHP.Value = Math.Min(255, EVHP.Value + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerBracer:
					EVAttack.Value = Math.Min(255, EVAttack.Value + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerBelt:
					EVDefence.Value = Math.Min(255, EVDefence.Value + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerLens:
					EVSpecialAttack.Value = Math.Min(255, EVSpecialAttack.Value + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerBand:
					EVSpecialDefence.Value = Math.Min(255, EVSpecialDefence.Value + (Pokerus.Checked ? 8 : 4));
                    break;
				case Items.PowerAnklet:
					EVSpeed.Value = Math.Min(255, EVSpeed.Value + (Pokerus.Checked ? 8 : 4));
                    break;
                }
            recalculate();
	    }

	    private void UpdateStat(int statIncrease, NumericUpDown numericUpDown)
	    {
	        var items = HeldItem.SelectedItem as Items?;
	        numericUpDown.Value = Math.Min(statIncrease * (items.HasValue && items.Value == Items.MachoBrace ? 2 : 1) * (Pokerus.Checked ? 2 : 1) + numericUpDown.Value, 255);
	    }

        private void recalculate()
        {
            Pokemon.Level = (int)Level.Value;
            Pokemon.HasPokerus = Pokerus.Checked;
            Pokemon.HeldItem = (Items)HeldItem.SelectedIndex;
            Pokemon.Nature = (Nature)Nature.SelectedItem;

            Pokemon.IV[Stat.HP] = (int)IVHP.Value;
            Pokemon.IV[Stat.Attack] = (int)IVAttack.Value;
            Pokemon.IV[Stat.Defence] = (int)IVDefence.Value;
            Pokemon.IV[Stat.SpecialAttack] = (int)IVSpecialAttack.Value;
            Pokemon.IV[Stat.SpecialDefence] = (int)IVSpecialDefence.Value;
            Pokemon.IV[Stat.Speed] = (int)IVSpeed.Value;

            Pokemon.EV[Stat.HP] = (int)EVHP.Value;
            Pokemon.EV[Stat.Attack] = (int)EVAttack.Value;
            Pokemon.EV[Stat.Defence] = (int)EVDefence.Value;
            Pokemon.EV[Stat.SpecialAttack] = (int)EVSpecialAttack.Value;
            Pokemon.EV[Stat.SpecialDefence] = (int)EVSpecialDefence.Value;
            Pokemon.EV[Stat.Speed] = (int)EVSpeed.Value;

            UpdateForm();
        }


        private void btnHPUp_Click(object sender, EventArgs e)
        {
            int value = (int)EVHP.Value;
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EVHP.Value = value;
            recalculate();
        }

        private void btnProtein_Click(object sender, EventArgs e)
        {
            int value = (int)EVAttack.Value;
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EVAttack.Value = value;
            recalculate();
        }

        private void btnIron_Click(object sender, EventArgs e)
        {
            int value = (int)EVDefence.Value;
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EVDefence.Value = value;
            recalculate();
        }

        private void btnCalcium_Click(object sender, EventArgs e)
        {
            int value = (int)EVSpecialAttack.Value;
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EVSpecialAttack.Value = value;
            recalculate();
        }

        private void btnZinc_Click(object sender, EventArgs e)
        {
            int value = (int)EVSpecialDefence.Value;
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EVSpecialDefence.Value = value;
            recalculate();
        }

        private void btnCarbos_Click(object sender, EventArgs e)
        {
            int value = (int)EVSpeed.Value;
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EVSpeed.Value = value;
            recalculate();
        }

        private void btnPomeg_Click(object sender, EventArgs e)
        {
            int value = (int)EVHP.Value;
            if (value <= 100)
                value = Math.Max(0, value - 10);
            else
                value = 100;

            EVHP.Value = value;
            recalculate();
        }

        private void btnKelpsy_Click(object sender, EventArgs e)
        {
            int value = (int)EVAttack.Value;
            if (value <= 100)
                value = Math.Max(0, value - 10);
            else
                value = 100;

            EVAttack.Value = value;
            recalculate();
        }

        private void btnQualot_Click(object sender, EventArgs e)
        {
            int value = (int)EVDefence.Value;
            if (value <= 100)
                value = Math.Max(0, value - 10);
            else
                value = 100;

            EVDefence.Value = value;
            recalculate();
        }

        private void btnHondew_Click(object sender, EventArgs e)
        {
            int value = (int)EVSpecialAttack.Value;
            if (value <= 100)
                value = Math.Max(0, value - 10);
            else
                value = 100;

            EVSpecialAttack.Value = value;
            recalculate();
        }

        private void btnGrepa_Click(object sender, EventArgs e)
        {
            int value = (int)EVSpecialDefence.Value;
            if (value <= 100)
                value = Math.Max(0, value - 10);
            else
                value = 100;

            EVSpecialDefence.Value = value;
            recalculate();
        }

        private void btnTamato_Click(object sender, EventArgs e)
        {
            int value = (int)EVSpeed.Value;
            if (value <= 100)
                value = Math.Max(0, value - 10);
            else
                value = 100;

            EVSpeed.Value = value;
            recalculate();
        }

        private void cmbPok_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pokemon.Species = (PokemonType)Species.SelectedItem;
            UpdateForm();
        }

        private void UpdateForm()
        {
            Pokemon.Species = (PokemonType)Species.SelectedItem;

            foreach (Stat s in Enum.GetValues(typeof(Stat)))
            {
                double modifier = Pokemon.Nature.GetModifier(s);
                if (modifier < 1) StatLabels[s].ForeColor = Color.Red;
                else if (modifier == 1) StatLabels[s].ForeColor = Color.Black;
                else StatLabels[s].ForeColor = Color.Green;
            }

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

            int num = Pokemon.Species.DexNumber;
            string location = "_" + num.ToString().PadLeft(3, '0');

            Image.Image = (Image)Properties.Resources.ResourceManager.GetObject(location);

            int ev = 0;
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

	    public void Load(Pokemon pokemon)
	    {
            Pokemon = pokemon;
            Species.SelectedIndex = Species.Items.IndexOf(pokemon.Species);
            Level.Value = pokemon.Level;
            Nature.SelectedIndex = Nature.Items.IndexOf(pokemon.Nature);
            Pokerus.Checked = pokemon.HasPokerus;
            HeldItem.SelectedIndex = HeldItem.Items.IndexOf(pokemon.HeldItem.GetName());

            EVAttack.Value = pokemon.EV[Stat.Attack];
            EVDefence.Value = pokemon.EV[Stat.Defence];
            EVHP.Value = pokemon.EV[Stat.HP];
            EVSpecialAttack.Value = pokemon.EV[Stat.SpecialAttack];
            EVSpecialDefence.Value = pokemon.EV[Stat.SpecialDefence];
            EVSpeed.Value = pokemon.EV[Stat.Speed];
            IVAttack.Value = pokemon.IV[Stat.Attack];
            IVDefence.Value = pokemon.IV[Stat.Defence];
            IVHP.Value = pokemon.IV[Stat.HP];
            IVSpecialAttack.Value = pokemon.IV[Stat.SpecialAttack];
            IVSpecialDefence.Value = pokemon.IV[Stat.SpecialDefence];
            IVSpeed.Value = pokemon.IV[Stat.Speed];

            recalculate();
        }
	}
}
