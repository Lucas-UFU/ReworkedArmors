using Jotunn.Managers;
using System;

namespace ReworkedArmors
{
    internal static class AddNewSets
    {
        internal static void Init()
        {
            ItemManager.OnVanillaItemsAvailable += new Action(AddArmorSets);
            ItemManager.OnItemsRegistered += new Action(ModExistingRecipes);
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
            ArmorHelper.AddArmorSet("padded");

            ItemManager.OnVanillaItemsAvailable -= new Action(AddNewSets.AddArmorSets);
        }

        private static void ModExistingRecipes()
        {
            //ArmorHelper.AddTieredRecipes("leather");
            //ArmorHelper.AddTieredRecipes("trollLeather");
            //ArmorHelper.AddTieredRecipes("bronze");
            //ArmorHelper.AddTieredRecipes("iron");
            //ArmorHelper.AddTieredRecipes("silver");
            //ArmorHelper.AddTieredRecipes("padded");
            //ArmorHelper.AddTieredRecipes("rags", false);

            ItemManager.OnItemsRegistered -= new Action(AddNewSets.ModExistingRecipes);
        }
    }
}
