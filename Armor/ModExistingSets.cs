using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace ReworkedArmors
{
    internal static class ModExistingSets
    {
        private static JObject balance = JObject.Parse(File.ReadAllText(Path.Combine(ReworkedArmors.ModPath, "balance.json")));

        internal static void Init()
        {
            ItemManager.OnItemsRegistered += new Action(ModLeatherArmor);
            ItemManager.OnItemsRegistered += new Action(ModTrollArmor);
            ItemManager.OnItemsRegistered += new Action(ModBronzeArmor);
            ItemManager.OnItemsRegistered += new Action(ModIronArmor);
            ItemManager.OnItemsRegistered += new Action(ModSilverArmor);
            ItemManager.OnItemsRegistered += new Action(ModPaddedArmor);
        }

        private static void ModLeatherArmor()
        {
            ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetLeather");
            ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherChest");
            ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorLeatherLegs");
            JToken values = balance["leather"];
            Recipe recipe1 = ObjectDB.instance.GetRecipe(prefab1.m_itemData);
            Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
            Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            recipe1.m_minStationLevel = 1;
            recipe2.m_minStationLevel = 1;
            recipe3.m_minStationLevel = 1;
            ArmorHelper.ModArmorSet("leather", ref prefab1.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, false, -1);
            ItemManager.OnItemsRegistered -= new Action(ModLeatherArmor);
        }

        private static void ModTrollArmor()
        {
            JToken values = balance["trollLeather"];
            ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetTrollLeather");
            ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherChest");
            ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorTrollLeatherLegs");
            Recipe recipe1 = ObjectDB.instance.GetRecipe(prefab1.m_itemData);
            Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
            Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("piece_workbench");
            ArmorHelper.ModArmorSet("trollLeather", ref prefab1.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, false, -1);
            ItemManager.OnItemsRegistered -= new Action(ModTrollArmor);
        }

        private static void ModBronzeArmor()
        {
            ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetBronze");
            ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeChest");
            ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorBronzeLegs");
            JToken values = balance["bronze"];
            Recipe recipe1 = ObjectDB.instance.GetRecipe(prefab1.m_itemData);
            Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
            Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            ArmorHelper.ModArmorSet("bronze", ref prefab1.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, false, -1);
            ItemManager.OnItemsRegistered -= new Action(ModBronzeArmor);
        }

        private static void ModIronArmor()
        {
            ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetIron");
            ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronChest");
            ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorIronLegs");
            JToken values = balance["iron"];
            Recipe recipe1 = ObjectDB.instance.GetRecipe(prefab1.m_itemData);
            Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
            Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            ArmorHelper.ModArmorSet("iron", ref prefab1.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, false, -1);
            ItemManager.OnItemsRegistered -= new Action(ModIronArmor);
        }

        private static void ModSilverArmor()
        {
            ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetDrake");
            ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfChest");
            ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorWolfLegs");
            JToken values = balance["silver"];
            Recipe recipe1 = ObjectDB.instance.GetRecipe(prefab1.m_itemData);
            Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
            Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            ArmorHelper.ModArmorSet("silver", ref prefab1.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, false, -1);
            ItemManager.OnItemsRegistered -= new Action(ModSilverArmor);
        }

        private static void ModPaddedArmor()
        {
            ItemDrop prefab1 = PrefabManager.Cache.GetPrefab<ItemDrop>("HelmetPadded");
            ItemDrop prefab2 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedCuirass");
            ItemDrop prefab3 = PrefabManager.Cache.GetPrefab<ItemDrop>("ArmorPaddedGreaves");
            Recipe recipe1 = ObjectDB.instance.GetRecipe(prefab1.m_itemData);
            Recipe recipe2 = ObjectDB.instance.GetRecipe(prefab2.m_itemData);
            Recipe recipe3 = ObjectDB.instance.GetRecipe(prefab3.m_itemData);
            recipe1.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe2.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            recipe3.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>("forge");
            JToken values = balance["padded"];
            ArmorHelper.ModArmorSet("padded", ref prefab1.m_itemData, ref prefab2.m_itemData, ref prefab3.m_itemData, values, false, -1);
            ItemManager.OnItemsRegistered -= new Action(ModPaddedArmor);
        }  
    }
}
