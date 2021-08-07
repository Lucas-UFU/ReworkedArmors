using Jotunn.Entities;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ReworkedArmors
{
    internal class ArmorHelper
    {
        public static void ModArmorSet(
          string setName,
          ref ItemDrop.ItemData helmet,
          ref ItemDrop.ItemData chest,
          ref ItemDrop.ItemData legs,
          JToken values,
          bool isNewSet,
          int i)
        {
            Data.ArmorSet armorSet = Data.ArmorSets[setName];
            List<ItemDrop.ItemData> itemDataList1 = new List<ItemDrop.ItemData>
            {
                helmet,
                chest,
                legs
            };

            JToken values1;

            if (isNewSet)
            {
                values1 = values[(object)"upgrades"][(object)string.Format("t{0}", (object)i)];
            }
            else
            {
                values1 = values;
                helmet.m_shared.m_name = (armorSet.HelmetID + "T0");
                chest.m_shared.m_name = armorSet.ChestID + "T0";
                legs.m_shared.m_name = (armorSet.LegsID + "T0");
            }

            foreach (ItemDrop.ItemData itemData in itemDataList1)
            {
                itemData.m_shared.m_armor = (float)values1[(object)"baseArmor"];
                itemData.m_shared.m_armorPerLevel = (float)values1[(object)"armorPerLevel"];
                itemData.m_shared.m_setSize = 3;
                if (!(itemData.m_shared.m_name.Contains(nameof(helmet))))
                    itemData.m_shared.m_movementModifier = (float)values1["globalMoveMod"];
            }
        }

        public static void ModArmorPiece(
          string setName,
          ref ItemDrop.ItemData piece,
          bool isNewSet,
          int tier,
          Armor armor)
        {
            Data.ArmorSet armorSet = Data.ArmorSets[setName];
            if (isNewSet)
            {
                values1 = values[(object)"upgrades"][(object)string.Format("t{0}", (object)i)];
            }
            else
            {
                values1 = values;
                piece.m_shared.m_name = (armorSet.HelmetName + "0");
            }
            piece.m_shared.m_armor = (float)values1[(object)"baseArmor"];
            piece.m_shared.m_armorPerLevel = (float)values1[(object)"armorPerLevel"];
            piece.m_shared.m_setSize = setName != "rags" ? 3 : 2;
            piece.m_shared.m_setName = (string)values[(object)"name"];
            if (!piece.m_shared.m_name.Contains("helmet"))
                piece.m_shared.m_movementModifier = (float)values1[(object)"globalMoveMod"];   
        }

        public static void AddArmorPiece(string setName, string location)
        {
            Armor armorConfig = ReworkedArmors.root.armors.Where(x => x.type == "setName").FirstOrDefault();
            Data.ArmorSet armorSet = Data.ArmorSets[setName];
            int startingTier = armorConfig.startingTier;

            for (int i = startingTier; i <= 5; ++i)
            {
                string str1 = "";
                string str3 = location;
                if (!(str3 == "head"))
                {
                    if (!(str3 == "chest"))
                    {
                        if (str3 == "legs")
                        {
                            str1 = armorSet.LegsID;
                        }
                    }
                    else
                    {
                        str1 = armorSet.ChestID;
                    }
                }
                else
                {
                    str1 = armorSet.HelmetID;
                }

                CustomItem customItem = new CustomItem(str1 + "T" + i, str1);
                customItem.ItemDrop.m_itemData.m_shared.m_name = string.Format("{0}T{1}", str1, i);
                ModArmorPiece(setName, ref customItem.ItemDrop.m_itemData, true, i, armorConfig);
                Recipe instance = ScriptableObject.CreateInstance<Recipe>();
                instance.name = string.Format("Recipe_{0}T{1}", (object)str1, (object)i);
                List<Piece.Requirement> requirementList = new List<Piece.Requirement>();

                if (i >= startingTier)
                {
                    string tier = i == startingTier ? "" : "T" + i;
                    requirementList.Add(MockRequirement.Create(str1 + tier, 1, false));
                }

                Tier armortTier = ReworkedArmors.root.tiers.Where(x => x.tier == i).FirstOrDefault();

                foreach (Cost cost in armortTier.costs)
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
