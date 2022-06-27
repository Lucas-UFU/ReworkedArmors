using Jotunn.Entities;
using Jotunn.Managers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReworkedArmors
{
    internal class ArmorHelper
    {
        public static void AddArmorPiece(string setName, string setPart, string color = "", object instance = null)
        {
            Armor armorConfig = ReworkedArmors.root.armors.Where(x => x.type == setName).FirstOrDefault();
            int startingTier = armorConfig.startingTier;
            string armorId = getArmorId(setPart, color, armorConfig);

            StatusEffect effect = ObjectDB.m_instance.GetStatusEffect(armorConfig.helmetID);

            for (int i = startingTier; i <= ReworkedArmors.root.tiers.Count; ++i)
            {
                Tier armortTier = ReworkedArmors.root.tiers.Where(x => x.tier == i).FirstOrDefault();
                GameObject oldItem = PrefabManager.Instance.GetPrefab(armorId);
                StatusEffect oldItemEffect = oldItem.GetComponent<ItemDrop>().m_itemData.m_shared.m_setStatusEffect;

                if (oldItemEffect is not null)
                    effect = oldItemEffect;

                CustomItem customItem = new CustomItem(armorId + "T" + i, armorId);
                if (customItem is null)
                {
                    Debug.LogError("Reworked Armors - " + armorId + " not found");
                    return;
                }

                var cuirass = ObjectDB.instance.GetItemPrefab("ArmorPaddedCuirass");
                customItem.ItemDrop.m_itemData.m_shared.m_setSize = 3;
                customItem.ItemDrop.m_itemData.m_shared.m_setStatusEffect = effect;
                customItem.ItemDrop.m_itemData.m_shared.m_setName = setName;
                customItem.ItemDrop.m_itemData.m_shared.m_name = string.Format("{0} T{1}", customItem.ItemDrop.m_itemData.m_shared.m_name, i);
                customItem.ItemDrop.m_itemData.m_shared.m_armor = armortTier.baseArmor;
                customItem.ItemDrop.m_itemData.m_shared.m_armorPerLevel = armortTier.armorPerLevel;
                customItem.ItemDrop.m_itemData.m_shared.m_maxDurability = 1000;
                customItem.ItemDrop.m_itemData.m_shared.m_useDurabilityDrain = cuirass.GetComponent<ItemDrop>().m_itemData.m_shared.m_useDurabilityDrain;
                customItem.ItemDrop.m_itemData.m_shared.m_durabilityPerLevel = cuirass.GetComponent<ItemDrop>().m_itemData.m_shared.m_durabilityPerLevel;
                customItem.ItemDrop.m_itemData.m_shared.m_canBeReparied = true;
                customItem.ItemDrop.m_itemData.m_shared.m_destroyBroken = false;
                customItem.ItemDrop.m_itemData.m_shared.m_useDurability = cuirass.GetComponent<ItemDrop>().m_itemData.m_shared.m_useDurability;
                customItem.ItemDrop.m_itemData.m_shared.m_durabilityDrain = cuirass.GetComponent<ItemDrop>().m_itemData.m_shared.m_durabilityDrain;
                customItem.ItemDrop.m_itemData.m_shared.m_maxQuality = cuirass.GetComponent<ItemDrop>().m_itemData.m_shared.m_maxQuality;
                customItem.ItemDrop.m_itemData.m_quality = cuirass.GetComponent<ItemDrop>().m_itemData.m_quality;
                customItem.ItemDrop.m_itemData.m_variant = cuirass.GetComponent<ItemDrop>().m_itemData.m_variant;

                if (setPart != "head")
                    customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = (float) armortTier.moveSpeed;

                if (armorConfig.NoSpeedPenaltyAnd6ArmorDebuff)
                {
                    customItem.ItemDrop.m_itemData.m_shared.m_movementModifier = 0;
                    customItem.ItemDrop.m_itemData.m_shared.m_armor -= 6;
                }

                Recipe recipx = ScriptableObject.CreateInstance<Recipe>();
                recipx.name = string.Format("Recipe_{0}T{1}", armorId, i);

                List<Piece.Requirement> requirementList = new List<Piece.Requirement>();
                foreach (Cost cost in armortTier.costs.Where(x => x.itemType == setPart))
                {
                    requirementList.Add(MockRequirement.Create(cost.item, cost.amount, true));
                    requirementList.Last().m_amountPerLevel = cost.perLevel;
                }

                recipx.m_craftingStation = PrefabManager.Cache.GetPrefab<CraftingStation>(armortTier.station);
                recipx.m_minStationLevel = armortTier.minLevel;
                recipx.m_resources = requirementList.ToArray();
                recipx.m_item = customItem.ItemDrop;
                CustomRecipe customRecipe = new CustomRecipe(recipx, true, true);
                customItem.Recipe = customRecipe;

                ItemManager.Instance.AddItem(customItem);
            }
        }

        private static string getArmorId(string setPart, string color, Armor armorConfig)
        {
            string armorId = "";

            switch (setPart)
            {
                case "head":
                    armorId = armorConfig.helmetID;
                    break;
                case "chest":
                    armorId = armorConfig.chestID;
                    break;
                case "legs":
                    armorId = armorConfig.legsID;
                    break;
            }

            if (!string.IsNullOrEmpty(color))
                armorId += color;
            return armorId;
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
