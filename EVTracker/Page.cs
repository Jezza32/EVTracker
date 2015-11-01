using System.Collections.Generic;
using System.Windows.Forms;

namespace EVTracker
{
	public class Page
	{
		public Page() { Pokemon = new Pokemon(); StatLabels = new Dictionary<Stat, Label>(); }
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
	}
}
