using Jotunn.Entities;
using Jotunn.Managers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace ReworkedArmors
{
    internal class UtilityFunctions
    {

        public static void GetRecipe(ref Recipe recipe, JToken json, string location, bool useName = true)
        {
            List<Piece.Requirement> requirementList = new List<Piece.Requirement>();
            int num = 0;
            foreach (JToken jtoken in (IEnumerable<JToken>)json[(object)location])
            {
                Piece.Requirement requirement = new Piece.Requirement()
                {
                    m_resItem = PrefabManager.Cache.GetPrefab<ItemDrop>((string)jtoken[(object)"item"]),
                    m_amount = (int)jtoken[(object)"amount"],
                    m_amountPerLevel = (int)jtoken[(object)"perLevel"]
                };
                requirementList.Add(requirement);

                ++num;
            }
            if (useName)
                ((Object)recipe).name = "Recipe_" + json.Path;
            recipe.m_resources = requirementList.ToArray();
            recipe.m_minStationLevel = (int)json[(object)"minLevel"];
            recipe.m_craftingStation = Mock<CraftingStation>.Create((string)json[(object)"station"]);
        }
  }
}
