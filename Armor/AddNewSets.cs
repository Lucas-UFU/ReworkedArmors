using Jotunn.Managers;
using System;

namespace ReworkedArmors
{
    internal static class AddNewSets
    {
        internal static void Init()
        {
            ItemManager.OnVanillaItemsAvailable += new Action(AddArmorSets);
        }

        private static void AddArmorSets()
        {
            ArmorHelper.AddArmorSet("leather");
            ArmorHelper.AddArmorPiece("rags", "chest");
            ArmorHelper.AddArmorPiece("rags", "legs");
            ArmorHelper.AddArmorSet("trollLeather");
            ArmorHelper.AddArmorSet("bronze");
            ArmorHelper.AddArmorSet("iron");
            ArmorHelper.AddArmorSet("silver");

            ItemManager.OnVanillaItemsAvailable -= new Action(AddNewSets.AddArmorSets);
        }
    }
}
