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
	}
}
