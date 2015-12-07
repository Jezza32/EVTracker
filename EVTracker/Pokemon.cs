using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using EVTracker.Annotations;
using JulMar.Core.Collections;

namespace EVTracker
{
	[DataContract]
	public class Pokemon : INotifyPropertyChanged
	{
	    private PokemonType _species;
	    private int _level;
	    private readonly ObservableDictionary<Stat, int> _iv;
	    private readonly ObservableDictionary<Stat, int> _ev;
	    private Nature _nature;
	    private Items _heldItem;
	    private bool _hasPokerus;

	    [DataMember]
	    public PokemonType Species
	    {
	        get { return _species; }
	        set
	        {
	            if (_species == value) return;
	            _species = value;
                OnPropertyChanged();
	        }
	    }

	    [DataMember]
	    public int Level
	    {
	        get { return _level; }
	        set
	        {
	            if (_level == value) return;
	            _level = value;
                OnPropertyChanged();
	        }
	    }

	    [DataMember]
	    public IDictionary<Stat, int> IV => _iv;

	    [DataMember]
	    public IDictionary<Stat, int> EV => _ev;

	    [DataMember]
	    public Nature Nature
	    {
	        get { return _nature; }
	        set
	        {
	            if (Equals(value, _nature)) return;
	            _nature = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
            }
	    }

	    [DataMember]
	    public Items HeldItem
	    {
	        get { return _heldItem; }
	        set
	        {
	            if (value == _heldItem) return;
	            _heldItem = value;
	            OnPropertyChanged();
	        }
	    }

	    [DataMember]
	    public bool HasPokerus
	    {
	        get { return _hasPokerus; }
	        set
	        {
	            if (value == _hasPokerus) return;
	            _hasPokerus = value;
	            OnPropertyChanged();
	        }
	    }

	    public Pokemon()
		{
			Level = 1;
			Species = new PokemonType();
			Nature = new Nature();
			_iv = new ObservableDictionary<Stat, int>();
	        _iv.CollectionChanged += (o, e) => OnPropertyChangeStat();
	        _ev = new ObservableDictionary<Stat, int>();
	        _ev.CollectionChanged += (o, e) => OnPropertyChangeStat();
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

	    private void ApplyItem(Items item)
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
        
	    public void ApplyStatBoost(Stat stat)
	    {
            var value = EV[stat];
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            EV[stat] = value;
            OnPropertyChangeStat();
        }

        public void ApplyStatBerry(Stat stat)
        {
            var value = EV[stat];
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            EV[stat] = value;
            OnPropertyChangeStat();
        }

	    private void OnPropertyChangeStat()
	    {
	        OnPropertyChanged(nameof(HP));
	        OnPropertyChanged(nameof(Attack));
	        OnPropertyChanged(nameof(Defence));
	        OnPropertyChanged(nameof(SpecialAttack));
	        OnPropertyChanged(nameof(SpecialDefence));
	        OnPropertyChanged(nameof(Speed));
	    }

	    public void Defeat(PokemonType pokemonType)
	    {
            var dict = pokemonType.GivenEffortValues;

            if (dict.ContainsKey(Stat.HP))
            {
                UpdateStat(Stat.HP, dict[Stat.HP]);
            }
            if (dict.ContainsKey(Stat.Attack))
            {
                UpdateStat(Stat.Attack, dict[Stat.Attack]);
            }
            if (dict.ContainsKey(Stat.Defence))
            {
                UpdateStat(Stat.Defence, dict[Stat.Defence]);
            }
            if (dict.ContainsKey(Stat.SpecialAttack))
            {
                UpdateStat(Stat.SpecialAttack, dict[Stat.SpecialAttack]);
            }
            if (dict.ContainsKey(Stat.SpecialDefence))
            {
                UpdateStat(Stat.SpecialDefence, dict[Stat.SpecialDefence]);
            }
            if (dict.ContainsKey(Stat.Speed))
            {
                UpdateStat(Stat.Speed, dict[Stat.Speed]);
            }

            ApplyItem(HeldItem);
            OnPropertyChangeStat();
        }

        private void UpdateStat(Stat stat, int statIncrease)
        {
            EV[stat] = Math.Min(statIncrease * (HeldItem == Items.MachoBrace ? 2 : 1) * (HasPokerus ? 2 : 1) + EV[stat], 255);
        }

	    public event PropertyChangedEventHandler PropertyChanged;

	    [NotifyPropertyChangedInvocator]
	    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
	        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
	}
}
