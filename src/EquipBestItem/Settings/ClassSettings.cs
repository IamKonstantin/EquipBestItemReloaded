using System;
using System.Collections.Generic;
using static EquipBestItem.ClassSettings;

namespace EquipBestItem
{
    [Serializable]
    public class ClassSettings
    {
        public FilterWeaponSettings FilterWeaponOneHanded { get; set; }
        public ClassSettings()
        {
        }
        
        void Set()
        {

        }
    }
}
