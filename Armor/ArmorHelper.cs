using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReworkedArmors
{
    internal class ArmorHelper
    {
        public static void AddArmorPiece(string setName, string setPart, string color = "")
        {
            Armor armorConfig = ReworkedArmors.root.armors.Where(x => x.type == setName).FirstOrDefault();
            int startingTier = armorConfig.startingTier;
            string armorId = "";

            if (setPart == "head") armorId = armorConfig.helmetID;
            if (setPart == "chest") armorId = armorConfig.chestID;
            if (setPart == "legs") armorId = armorConfig.legsID;

            if (!string.IsNullOrEmpty(color)) armorId += color;

            for (int i = startingTier; i <= ReworkedArmors.root.tiers.Count; ++i)
            {
                Tier armortTier = ReworkedArmors.root.tiers.Where(x => x.tier == i).FirstOrDefault();
                CustomItem customItem = new CustomItem(armorId + "T" + i, armorId);
                
                customItem.ItemDrop.m_itemData.m_shared.m_name = string.Format("{0} T{1}", customItem.ItemDrop.m_itemData.m_shared.m_name, i);
                customItem.ItemDrop.m_itemData.m_shared.m_armor = armortTier.baseArmor;
                customItem.ItemDrop.m_itemData.m_shared.m_armorPerLevel = armortTier.armorPerLevel;

                if (setPart != "head")        
                    customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = (float)armortTier.moveSpeed;

                if (armorConfig.NoSpeedPenaltyAnd6ArmorDebuff)
                {
                    customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = 0;
                    customItem.ItemDrop.m_itemData.m_shared.m_armor -= 6;
                }

                Recipe instance = ScriptableObject.CreateInstance<Recipe>();
                instance.name = string.Format("Recipe_{0}T{1}", armorId, i);

                List<Piece.Requirement> requirementList = new List<Piece.Requirement>();
                foreach (Cost cost in armortTier.costs.Where(x => x.itemType == setPart))
                {
                    requirementList.Add(MockRequirement.Create(cost.item, cost.amount, true));
                    requirementList.Last().m_amountPerLevel = cost.perLevel;
                }

                instance.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>(armortTier.station);
                instance.m_minStationLevel = armortTier.minLevel;
                instance.m_resources = requirementList.ToArray();
                instance.m_item = customItem.ItemDrop;
                CustomRecipe customRecipe = new CustomRecipe(instance, true, true);
                customItem.Recipe = customRecipe;

                ItemManager.Instance.AddItem(customItem);
            }
        }

        public static void RevemoOriginalVersion(string setName)
        {
            Armor armorConfig = ReworkedArmors.root.armors.Where(x => x.type == setName).FirstOrDefault();
            var head = ObjectDB.instance.GetItemPrefab(armorConfig.helmetID);
            var chest = ObjectDB.instance.GetItemPrefab(armorConfig.chestID);
            var legs = ObjectDB.instance.GetItemPrefab(armorConfig.legsID);

            head.SetActive(false);
            chest.SetActive(false);
            legs.SetActive(false);
        }

        public static void AddArmorSet(string setName)
        {
            AddArmorPiece(setName, "head");
            AddArmorPiece(setName, "chest");
            AddArmorPiece(setName, "legs");
        }
    }
}
