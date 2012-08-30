﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;

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
		public int Attack { get { return getStat(Stat.Attack); } }
		public int Defence { get { return getStat(Stat.Defence); } }
		public int SpecialAttack { get { return getStat(Stat.SpecialAttack); } }
		public int SpecialDefence { get { return getStat(Stat.SpecialDefence); } }
		public int Speed { get { return getStat(Stat.Speed); } }

		private int getStat(Stat s)
		{
			int value = (2 * Species.BaseStats[s]) + IV[s] + (EV[s] / 4);
			value *= Level;
			value /= 100;
			value += 5;
			value = (int)Math.Floor((double)value * Nature.GetModifier(s));
			return value;
		}


		#region Serializable
		public static void Serialize(string location, List<Pokemon> pokemon)
		{
			DataContractSerializer serializer = new DataContractSerializer(typeof(List<Pokemon>));
			Stream s = File.Create(location);
			serializer.WriteObject(s, pokemon);
			s.Close();
		}
		#endregion
	}
}
