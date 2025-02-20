using System;
using System.Diagnostics;

namespace EquipBestItem
{
    [Serializable]
    public class FilterMountSettings
    {
        public int ChargeDamage { get; set; } = 1;
        public int HitPoints { get; set; } = 1;
        public int Maneuver { get; set; } = 1;
        public int Speed { get; set; } = 1;

        public FilterMountSettings()
        {

        }

        public int Get(CharacterSettings.ValueType valueType)
        {
            switch (valueType)
            {
                case CharacterSettings.ValueType.ChargeDamage: return ChargeDamage;
                case CharacterSettings.ValueType.HitPoints: return HitPoints;
                case CharacterSettings.ValueType.Maneuver: return Maneuver;
                case CharacterSettings.ValueType.Speed: return Speed;

                default: throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
            }
        }

        public void Set(CharacterSettings.ValueType valueType, int value)
        {
            switch (valueType)
            {
                case CharacterSettings.ValueType.ChargeDamage: ChargeDamage = value; break;
                case CharacterSettings.ValueType.HitPoints: HitPoints = value; break;
                case CharacterSettings.ValueType.Maneuver: Maneuver = value; break;
                case CharacterSettings.ValueType.Speed: Speed = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
            }
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">mount settings</param>
        public FilterMountSettings(FilterMountSettings other)
        {
            if (other == null) return;

            ChargeDamage = other.ChargeDamage;
            HitPoints = other.HitPoints;
            Maneuver = other.Maneuver;
            Speed = other.Speed;
        }

        public bool ThisFilterNotDefault()
        {
            if (this.ChargeDamage != 1) return true;
            if (this.HitPoints != 1) return true;
            if (this.Maneuver != 1) return true;
            if (this.Speed != 1) return true;
            return false;
        }

        public bool ThisFilterLocked()
        {
            if (this.ChargeDamage == 0 &&
                this.HitPoints == 0 &&
                this.Maneuver == 0 &&
                this.Speed == 0)
                return true;
            return false;
        }
    }
}
