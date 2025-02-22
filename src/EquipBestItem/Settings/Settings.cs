using System;

namespace EquipBestItem
{
    [Serializable]
    public class Settings
    {
        public bool IsEnabledEquipCurrentCharacterButton { get; set; } = true;
        public bool IsEnabledEquipAllButton { get; set; } = true;
        public bool IsEnabledStandardButtons { get; set; } = true;
        public bool IsLeftPanelLocked { get; set; } = true;
        public bool IsRightPanelLocked { get; set; } = false;

        public SerializableDictionary<WeaponClass, FilterWeaponSettings> FilterWeapon { get; set; }
        public SerializableDictionary<ArmorType, FilterArmorSettings> FilterArmor { get; set; }
        public FilterMountSettings FilterMount { get; set; }

        public Settings()
        {
            FilterWeapon = new SerializableDictionary<WeaponClass, FilterWeaponSettings>();
            FilterArmor = new SerializableDictionary<ArmorType, FilterArmorSettings>();
            FilterMount = new FilterMountSettings();
        }
    }
}
