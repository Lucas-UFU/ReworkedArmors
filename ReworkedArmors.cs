using BepInEx;
using BepInEx.Bootstrap;
using HarmonyLib;
using Jotunn.Managers;
using Jotunn.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ReworkedArmors
{
    [BepInPlugin("Detalhes.ReworkedArmors", "ReworkedArmors", "1.1.6")]
    public class ReworkedArmors : BaseUnityPlugin
    {
        public const string PluginGUID = "Detalhes.ReworkedArmors";
        Harmony harmony = new Harmony(PluginGUID);
        public static readonly string ModPath = Path.GetDirectoryName(typeof(ReworkedArmors).Assembly.Location);
        public static Root root = new Root();
        public static List<StatusEffect> effects = null;

        private void Awake()
        {
            string filePath = System.IO.Path.GetFullPath(@"..\..\");
            root = SimpleJson.SimpleJson.DeserializeObject<Root>(File.ReadAllText(Path.Combine(ModPath, "armorConfig.json")));
            PrefabManager.OnPrefabsRegistered += new Action(AddArmorSets);
        }

        private void AddArmorSets()
        {
            foreach(Armor armor in ReworkedArmors.root.armors)
            {
                if (armor.type == "rags")
                {
                    ArmorHelper.AddArmorPiece("rags", "chest");
                    ArmorHelper.AddArmorPiece("rags", "legs");
                }
                else if (armor.type == "sagetunic" || armor.type == "sagerobe" || armor.type == "sagehood") continue;
                else
                {
                    ArmorHelper.AddArmorSet(armor.type);
                }
            }

            List<string> sagecolors = new List<string> { "Black", "Blue", "Brown", "Gray", "Green", "Red", "White" };

            foreach (string color in sagecolors)
            {
                ArmorHelper.AddArmorPiece("sagetunic", "chest", color);
                ArmorHelper.AddArmorPiece("sagerobe", "chest", color);
                ArmorHelper.AddArmorPiece("sagehood", "head", color);
            }

        }
    }
}
