using System;
using System.Diagnostics;

namespace EquipBestItem
{
    [Serializable]
    public class FilterWeaponSettings
    {
        public int MaxDataValue { get; set; } = 1;
        public int ThrustSpeed { get; set; } = 1;
        public int SwingSpeed { get; set; } = 1;
        public int MissileSpeed { get; set; } = 1;
        public int WeaponLength { get; set; } = 1;
        public int ThrustDamage { get; set; } = 1;
        public int SwingDamage { get; set; } = 1;
        public int Accuracy { get; set; } = 1;
        public int Handling { get; set; } = 1;
        public int WeaponWeight { get; set; } = 0;
        public int WeaponBodyArmor { get; set; } = 1;
        
        public int Get(Parameter param)
        {
            switch (param)
            {
                case Parameter.MaxDataValue: return MaxDataValue;
                case Parameter.ThrustSpeed: return ThrustSpeed;
                case Parameter.SwingSpeed: return SwingSpeed;
                case Parameter.MissileSpeed: return MissileSpeed;
                case Parameter.WeaponLength: return WeaponLength;
                case Parameter.ThrustDamage: return ThrustDamage;
                case Parameter.SwingDamage: return SwingDamage;
                case Parameter.Accuracy: return Accuracy;
                case Parameter.Handling: return Handling;
                case Parameter.WeaponWeight: return WeaponWeight;
                case Parameter.WeaponBodyArmor: return WeaponBodyArmor;
                default: throw new ArgumentOutOfRangeException(nameof(param), param, null);
            }
        }

        public void Set(Parameter param, int value)
        {
            switch (param)
            {
                case Parameter.MaxDataValue: MaxDataValue = value; break;
                case Parameter.ThrustSpeed: ThrustSpeed = value; break;
                case Parameter.SwingSpeed: SwingSpeed = value; break;
                case Parameter.MissileSpeed: MissileSpeed = value; break;
                case Parameter.WeaponLength: WeaponLength = value; break;
                case Parameter.ThrustDamage: ThrustDamage = value; break;
                case Parameter.SwingDamage: SwingDamage = value; break;
                case Parameter.Accuracy: Accuracy = value; break;
                case Parameter.Handling: Handling = value; break;
                case Parameter.WeaponWeight: WeaponWeight = value; break;
                case Parameter.WeaponBodyArmor: WeaponBodyArmor = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(param), param, null);
            }
        }

        //public DamageTypes SwingDamageType { get; set; } = 0;
        //public DamageTypes ThrustDamageType { get; set; } = 0;
        //public int MissileDamage { get; set; }
        //public float WeaponBalance { get; set; }


        //public WeaponClass? WeaponClass { get; set; }
        //public string ItemUsage { get; set; }


        //public WeaponFlags? WeaponFlags { get; set; }

        public FilterWeaponSettings()
        {

        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">weapon settings</param>
        public FilterWeaponSettings(FilterWeaponSettings other)
        {
            if (other == null) return;

            MaxDataValue = other.MaxDataValue;
            ThrustSpeed = other.ThrustSpeed;
            SwingSpeed = other.SwingSpeed;
            MissileSpeed = other.MissileSpeed;
            WeaponLength = other.WeaponLength;
            ThrustDamage = other.ThrustDamage;
            SwingDamage = other.SwingDamage;
            Accuracy = other.Accuracy;
            Handling = other.Handling;
            WeaponWeight = other.WeaponWeight;
            WeaponBodyArmor = other.WeaponBodyArmor;
        }

        public bool ThisFilterNotDefault()
        {
            if (this.MaxDataValue != 1f) return true;
            if (this.ThrustSpeed != 1f) return true;
            if (this.SwingSpeed != 1f) return true;
            if (this.MissileSpeed != 1f) return true;
            if (this.WeaponLength != 1f) return true;
            if (this.ThrustDamage != 1f) return true;
            if (this.SwingDamage != 1f) return true;
            if (this.Accuracy != 1f) return true;
            if (this.Handling != 1f) return true;
            if (this.WeaponWeight != 0f) return true;
            if (this.WeaponBodyArmor != 1f) return true;
            return false;
        }

        public bool ThisFilterLocked()
        {
            if (this.MaxDataValue == 0f &&
                this.ThrustSpeed == 0f &&
                this.SwingSpeed == 0f &&
                this.MissileSpeed == 0f &&
                this.WeaponLength == 0f &&
                this.ThrustDamage == 0f &&
                this.SwingDamage == 0f &&
                this.Accuracy == 0f &&
                this.Handling == 0f &&
                this.WeaponWeight == 0f &&
                this.WeaponBodyArmor == 0f)
                return true;
            return false;
        }
    }
}
