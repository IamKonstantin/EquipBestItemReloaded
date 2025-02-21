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

        public int Get(Parameter param)
        {
            switch (param)
            {
                case Parameter.ChargeDamage: return ChargeDamage;
                case Parameter.HitPoints: return HitPoints;
                case Parameter.Maneuver: return Maneuver;
                case Parameter.Speed: return Speed;

                default: throw new ArgumentOutOfRangeException(nameof(param), param, null);
            }
        }

        public void Set(Parameter param, int value)
        {
            switch (param)
            {
                case Parameter.ChargeDamage: ChargeDamage = value; break;
                case Parameter.HitPoints: HitPoints = value; break;
                case Parameter.Maneuver: Maneuver = value; break;
                case Parameter.Speed: Speed = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(param), param, null);
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
