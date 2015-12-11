using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using EVTracker.Annotations;

namespace EVTracker
{
	[DataContract]
	public class Pokemon : INotifyPropertyChanged
	{
	    private PokemonType _species;
	    private int _level;
	    private Nature _nature;
	    private Items _heldItem;
	    private bool _hasPokerus;
	    private int _ivHP;
	    private int _ivAttack;
	    private int _ivDefence;
	    private int _ivSpecialAttack;
	    private int _ivSpecialDefence;
	    private int _ivSpeed;
	    private int _evHP;
	    private int _evAttack;
	    private int _evDefence;
	    private int _evSpecialAttack;
	    private int _evSpecialDefence;
	    private int _evSpeed;

	    private int GetIV(Stat s)
	    {
	        switch (s)
	        {
	            case Stat.HP:
	                return _ivHP;
	            case Stat.Attack:
	                return _ivAttack;
	            case Stat.Defence:
	                return _ivDefence;
	            case Stat.SpecialAttack:
	                return _ivSpecialAttack;
	            case Stat.SpecialDefence:
	                return _ivSpecialDefence;
	            case Stat.Speed:
	                return _ivSpeed;
	            default:
	                throw new ArgumentOutOfRangeException(nameof(s), s, null);
	        }
	    }

	    private int GetEV(Stat s)
	    {
	        switch (s)
	        {
	            case Stat.HP:
	                return _evHP;
	            case Stat.Attack:
	                return _evAttack;
	            case Stat.Defence:
	                return _evDefence;
	            case Stat.SpecialAttack:
	                return _evSpecialAttack;
	            case Stat.SpecialDefence:
	                return _evSpecialDefence;
	            case Stat.Speed:
	                return _evSpeed;
	            default:
	                throw new ArgumentOutOfRangeException(nameof(s), s, null);
	        }
	    }

	    private void SetEV(Stat s, int value)
	    {
	        switch (s)
	        {
	            case Stat.HP:
	                EVHP = value;
                    break;
	            case Stat.Attack:
	                EVAttack = value;
                    break;
	            case Stat.Defence:
	                EVDefence = value;
                    break;
	            case Stat.SpecialAttack:
	                EVSpecialAttack = value;
                    break;
	            case Stat.SpecialDefence:
	                EVSpecialDefence = value;
                    break;
	            case Stat.Speed:
	                EVSpeed = value;
                    break;
	            default:
	                throw new ArgumentOutOfRangeException(nameof(s), s, null);
	        }
	    }

	    [DataMember]
	    public PokemonType Species
	    {
	        get { return _species; }
	        set
	        {
	            if (_species == value) return;
	            _species = value;
                OnPropertyChanged();
                OnPropertyChangeStat();
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
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int IVHP
	    {
	        get { return _ivHP; }
	        set
	        {
	            if (value == _ivHP) return;
	            _ivHP = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int IVAttack
	    {
	        get { return _ivAttack; }
	        set
	        {
	            if (value == _ivAttack) return;
	            _ivAttack = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int IVDefence
	    {
	        get { return _ivDefence; }
	        set
	        {
	            if (value == _ivDefence) return;
	            _ivDefence = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int IVSpecialAttack
	    {
	        get { return _ivSpecialAttack; }
	        set
	        {
	            if (value == _ivSpecialAttack) return;
	            _ivSpecialAttack = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int IVSpecialDefence
	    {
	        get { return _ivSpecialDefence; }
	        set
	        {
	            if (value == _ivSpecialDefence) return;
	            _ivSpecialDefence = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int IVSpeed
	    {
	        get { return _ivSpeed; }
	        set
	        {
	            if (value == _ivSpeed) return;
	            _ivSpeed = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int EVHP
	    {
	        get { return _evHP; }
	        set
	        {
	            if (value == _evHP) return;
	            _evHP = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int EVAttack
	    {
	        get { return _evAttack; }
	        set
	        {
	            if (value == _evAttack) return;
	            _evAttack = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int EVDefence
	    {
	        get { return _evDefence; }
	        set
	        {
	            if (value == _evDefence) return;
	            _evDefence = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int EVSpecialAttack
	    {
	        get { return _evSpecialAttack; }
	        set
	        {
	            if (value == _evSpecialAttack) return;
	            _evSpecialAttack = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int EVSpecialDefence
	    {
	        get { return _evSpecialDefence; }
	        set
	        {
	            if (value == _evSpecialDefence) return;
	            _evSpecialDefence = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

	    [DataMember]
	    public int EVSpeed
	    {
	        get { return _evSpeed; }
	        set
	        {
	            if (value == _evSpeed) return;
	            _evSpeed = value;
	            OnPropertyChanged();
                OnPropertyChangeStat();
	        }
	    }

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
			Nature = Nature.Hardy;
			HeldItem = Items.None;
		}

		public int HP
		{
			get
			{
				int value = (2*Species.BaseStats[Stat.HP]) + _ivHP + (_evHP / 4);
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
			var value = (2 * Species.BaseStats[s]) + GetIV(s) + (GetEV(s) / 4);
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
                    EVHP = Math.Min(255, EVHP + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerBracer:
                    EVAttack = Math.Min(255, EVAttack + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerBelt:
                    EVDefence = Math.Min(255, EVDefence + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerLens:
                    EVSpecialAttack = Math.Min(255, EVSpecialAttack + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerBand:
                    EVSpecialDefence = Math.Min(255, EVSpecialDefence + (HasPokerus ? 8 : 4));
                    break;
                case Items.PowerAnklet:
                    EVSpeed = Math.Min(255, EVSpeed + (HasPokerus ? 8 : 4));
                    break;
            }
        }
        
	    public void ApplyStatBoost(Stat stat)
	    {
            var value = GetEV(stat);
            if (value >= 100) return;
            value = Math.Min(100, value + 10);
            SetEV(stat, value);
            OnPropertyChangeStat();
        }

        public void ApplyStatBerry(Stat stat)
        {
            var value = GetEV(stat);
            value = value <= 100 ? Math.Max(0, value - 10) : 100;

            SetEV(stat, value);
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
            SetEV(stat, Math.Min(statIncrease * (HeldItem == Items.MachoBrace ? 2 : 1) * (HasPokerus ? 2 : 1) + GetEV(stat), 255));
        }

	    public event PropertyChangedEventHandler PropertyChanged;

	    [NotifyPropertyChangedInvocator]
	    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
	    {
	        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }

	    public override string ToString()
	    {
	        return Species.ToString();
	    }
	}
}
