using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;

namespace EVTracker
{
	[DataContract]
	public class Pokemon
	{
		[DataMember]
		public PokemonType Species { get; set; }
		[DataMember]
		public int Level { get; set; }
		[DataMember]
		public IDictionary<Stat, int> IV { get; private set; }
		[DataMember]
		public IDictionary<Stat, int> EV { get; private set; }
		[DataMember]
		public Nature Nature { get; set; }
		[DataMember]
		public Items HeldItem { get; set; }
		[DataMember]
		public bool HasPokerus { get; set; }

		public Pokemon()
		{
			Level = 1;
			Species = new PokemonType();
			Nature = new Nature();
			IV = new Dictionary<Stat, int>();
			EV = new Dictionary<Stat, int>();
			foreach (Stat s in Enum.GetValues(typeof(Stat)))
			{
				IV.Add(s, 0);
				EV.Add(s, 0);
			}
			HeldItem = Items.None;
		}

		public int HP
		{
			get
			{
				int value = (2*Species.BaseStats[Stat.HP]) + IV[Stat.HP] + (EV[Stat.HP] / 4);
				value *= Level;
				value /= 100;
				value += Level + 10;
				return value;
			}
		}
		public int Attack => GetStat(Stat.Attack);
	    public int Defence => GetStat(Stat.Defence);
	    public int SpecialAttack => GetStat(Stat.SpecialAttack);
	    public int SpecialDefence => GetStat(Stat.SpecialDefence);
	    public int Speed => GetStat(Stat.Speed);

	    public static Pokemon MissingNo => new Pokemon { Species = new PokemonType()};

	    private int GetStat(Stat s)
		{
			var value = (2 * Species.BaseStats[s]) + IV[s] + (EV[s] / 4);
			value *= Level;
			value /= 100;
			value += 5;
			value = (int)Math.Floor(value * Nature.GetModifier(s));
			return value;
		}


		#region Serializable
		public static void Serialize(string location, List<Pokemon> pokemon)
		{
			var serializer = new DataContractSerializer(typeof(List<Pokemon>));
		    using (var stream = File.Create(location))
		    {
		        serializer.WriteObject(stream, pokemon);
		        stream.Close();
		    }
		}
		#endregion

	    public void ApplyItem(Items item)
	    {
            switch (item)
            {
                case Items.PowerWeight:
                    EV[Stat.HP] = Math.Min(255, EV[Stat.HP] + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerBracer:
                    EV[Stat.Attack] = Math.Min(255, EV[Stat.Attack] + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerBelt:
                    EV[Stat.Defence] = Math.Min(255, EV[Stat.Defence] + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerLens:
                    EV[Stat.SpecialAttack] = Math.Min(255, EV[Stat.SpecialAttack] + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerBand:
                    EV[Stat.SpecialDefence] = Math.Min(255, EV[Stat.SpecialDefence] + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerAnklet:
                    EV[Stat.Speed] = Math.Min(255, EV[Stat.Speed] + (HasPokerus ? 8 : 4));
                    break;
            }
        }

	    public void UpdateStat(Stat stat, int statIncrease)
	    {
            EV[stat] = Math.Min(statIncrease * (HeldItem == Items.MachoBrace ? 2 : 1) * (HasPokerus ? 2 : 1) + EV[stat], 255);
        }

	    public void ApplyStatBoost(Stat stat)
	    {
            var value = EV[stat];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EV[stat] = value;
        }

        public void ApplyStatBerry(Stat stat)
        {
            var value = EV[stat];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            EV[stat] = value;
        }
    }
}
