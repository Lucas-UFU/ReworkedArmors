using Jotunn.Entities;
using Jotunn.Managers;
using Jotunn.Configs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReworkedArmors
{
    internal class ArmorHelper
    {
        public static void AddArmorPiece(string setName, string setPart)
        {
            Armor armorConfig = ReworkedArmors.root.armors.Where(x => x.type == setName).FirstOrDefault();
            int startingTier = armorConfig.startingTier;
            string armorId = "";
            if (setPart == "head") armorId = armorConfig.helmetID;
            if (setPart == "chest") armorId = armorConfig.chestID;
            if (setPart == "legs") armorId = armorConfig.legsID;

            for (int i = startingTier; i <= 5; ++i)
            {
                Tier armortTier = ReworkedArmors.root.tiers.Where(x => x.tier == i).FirstOrDefault();
                CustomItem customItem = new CustomItem(armorId + "T" + i, armorId);
                customItem.ItemDrop.m_itemData.m_shared.m_name = string.Format("{0} T{1}", customItem.ItemDrop.m_itemData.m_shared.m_name, i);

                customItem.ItemDrop.m_itemData.m_shared.m_armor = armortTier.baseArmor;
                customItem.ItemDrop.m_itemData.m_shared.m_armorPerLevel = armortTier.armorPerLevel;

                if (setPart == "head")
                    customItem.ItemDrop.m_itemData.m_shared.m_helmetHideHair = false;
                else
                    customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = (float)armortTier.moveSpeed;

                Recipe instance = ScriptableObject.CreateInstance<Recipe>();
                instance.name = string.Format("Recipe_{0}T{1}", armorId, i);
                List<Piece.Requirement> requirementList = new List<Piece.Requirement>();
                string tier = i == startingTier ? "" : "T" + (i - 1);

                requirementList.Add(MockRequirement.Create(armorId + tier, 1, false));
                requirementList.Last().m_amountPerLevel = 0;

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

                ItemManager.Instance.AddItem(customItem);
                ItemManager.Instance.AddRecipe(customRecipe);
            }
        }

        public static void AddArmorSet(string setName)
        {
            AddArmorPiece(setName, "head");
            AddArmorPiece(setName, "chest");
            AddArmorPiece(setName, "legs");
        }
    }
}
