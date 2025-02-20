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

        public void Set(CharacterSettings.ValueType valueType, int value)
        {
            switch (valueType)
            {
                case CharacterSettings.ValueType.HeadArmor: HeadArmor = value; break;
                case CharacterSettings.ValueType.ArmorBodyArmor: ArmorBodyArmor = value; break;
                case CharacterSettings.ValueType.LegArmor: LegArmor = value; break;
                case CharacterSettings.ValueType.ArmArmor: ArmArmor = value; break;
                case CharacterSettings.ValueType.ManeuverBonus: ManeuverBonus = value; break;
                case CharacterSettings.ValueType.SpeedBonus: SpeedBonus = value; break;
                case CharacterSettings.ValueType.ChargeBonus: ChargeBonus = value; break;
                case CharacterSettings.ValueType.ArmorWeight: ArmorWeight = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
            }
        }

        public int Get(CharacterSettings.ValueType valueType)
        {
            switch (valueType)
            {
                case CharacterSettings.ValueType.HeadArmor: return HeadArmor;
                case CharacterSettings.ValueType.ArmorBodyArmor: return ArmorBodyArmor;
                case CharacterSettings.ValueType.LegArmor: return LegArmor;
                case CharacterSettings.ValueType.ArmArmor: return ArmArmor;
                case CharacterSettings.ValueType.ManeuverBonus: return ManeuverBonus;
                case CharacterSettings.ValueType.SpeedBonus: return SpeedBonus;
                case CharacterSettings.ValueType.ChargeBonus: return ChargeBonus;
                case CharacterSettings.ValueType.ArmorWeight: return ArmorWeight;
                default: throw new ArgumentOutOfRangeException(nameof(valueType), valueType, null);
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
