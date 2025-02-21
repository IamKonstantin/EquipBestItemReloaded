using System;
using System.Diagnostics;


namespace EquipBestItem
{
    [Serializable]
    public class FilterArmorSettings
    {
        public int HeadArmor { get; set; } = 1;
        public int ArmorBodyArmor { get; set; } = 1;
        public int LegArmor { get; set; } = 1;
        public int ArmArmor { get; set; } = 1;

        public int ManeuverBonus { get; set; } = 1;
        public int SpeedBonus { get; set; } = 1;
        public int ChargeBonus { get; set; } = 1;
        public int ArmorWeight { get; set; } = 0;

        public void Set(Parameter param, int value)
        {
            switch (param)
            {
                case Parameter.HeadArmor: HeadArmor = value; break;
                case Parameter.ArmorBodyArmor: ArmorBodyArmor = value; break;
                case Parameter.LegArmor: LegArmor = value; break;
                case Parameter.ArmArmor: ArmArmor = value; break;
                case Parameter.ManeuverBonus: ManeuverBonus = value; break;
                case Parameter.SpeedBonus: SpeedBonus = value; break;
                case Parameter.ChargeBonus: ChargeBonus = value; break;
                case Parameter.ArmorWeight: ArmorWeight = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(param), param, null);
            }
        }

        public int Get(Parameter param)
        {
            switch (param)
            {
                case Parameter.HeadArmor: return HeadArmor;
                case Parameter.ArmorBodyArmor: return ArmorBodyArmor;
                case Parameter.LegArmor: return LegArmor;
                case Parameter.ArmArmor: return ArmArmor;
                case Parameter.ManeuverBonus: return ManeuverBonus;
                case Parameter.SpeedBonus: return SpeedBonus;
                case Parameter.ChargeBonus: return ChargeBonus;
                case Parameter.ArmorWeight: return ArmorWeight;
                default: throw new ArgumentOutOfRangeException(nameof(param), param, null);
            }
        }

        public FilterArmorSettings()
        {

        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other">armor settings</param>
        public FilterArmorSettings(FilterArmorSettings other)
        {
            if (other == null) return;

            HeadArmor = other.HeadArmor;
            ArmorBodyArmor = other.ArmorBodyArmor;
            LegArmor = other.LegArmor;
            ArmArmor = other.ArmArmor;
            ManeuverBonus = other.ManeuverBonus;
            SpeedBonus = other.SpeedBonus;
            ChargeBonus = other.ChargeBonus;
            ArmorWeight = other.ArmorWeight;
        }

        public bool ThisFilterNotDefault()
        {
            if (this.HeadArmor != 1) return true;
            if (this.ArmorBodyArmor != 1) return true;
            if (this.LegArmor != 1) return true;
            if (this.ArmArmor != 1) return true;
            if (this.ManeuverBonus != 1) return true;
            if (this.SpeedBonus != 1) return true;
            if (this.ChargeBonus != 1) return true;
            if (this.ArmorWeight != 0) return true;
            return false;
        }

        public bool ThisFilterLocked()
        {
            if (this.HeadArmor == 0 &&
                this.ArmorBodyArmor == 0 &&
                this.LegArmor == 0 &&
                this.ArmArmor == 0 &&
                this.ManeuverBonus == 0 &&
                this.SpeedBonus == 0 &&
                this.ChargeBonus == 0 &&
                this.ArmorWeight == 0)
                return true;
            return false;
        }
    }
}
