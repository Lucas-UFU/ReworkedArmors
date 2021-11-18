using BepInEx;
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
    [BepInPlugin("Detalhes.ReworkedArmors", "ReworkedArmors", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class ReworkedArmors : BaseUnityPlugin
    {
        public const string PluginGUID = "Detalhes.ReworkedArmors";
        Harmony harmony = new Harmony(PluginGUID);
        public static readonly string ModPath = Path.GetDirectoryName(typeof(ReworkedArmors).Assembly.Location);
        public static Root root = new Root();
        private void Awake()
        {
            string filePath = System.IO.Path.GetFullPath(@"..\..\");
            root = SimpleJson.SimpleJson.DeserializeObject<Root>(File.ReadAllText(Path.Combine(ModPath, "armorConfig.json")));
            ItemManager.OnVanillaItemsAvailable += new Action(AddArmorSets);
        }

        private void AddArmorSets()
        {
            ArmorHelper.AddArmorSet("leather");
            ArmorHelper.AddArmorPiece("rags", "chest");
            ArmorHelper.AddArmorPiece("rags", "legs");
            ArmorHelper.AddArmorSet("trollLeather");
            ArmorHelper.AddArmorSet("bronze");
            ArmorHelper.AddArmorSet("iron");
            ArmorHelper.AddArmorSet("silver");
            ArmorHelper.AddArmorSet("plate");
            ArmorHelper.AddArmorSet("barbarian");
            ArmorHelper.AddArmorSet("nomad");
            ArmorHelper.AddArmorSet("wanderer");
            ArmorHelper.AddArmorSet("dragonslayer");
            ArmorHelper.AddArmorSet("mistlands");

            List<string> sagecolors = new List<string> { "Black", "Blue", "Brown", "Gray", "Green", "Red", "White" };

            foreach (string color in sagecolors)
            {
                ArmorHelper.AddArmorPiece("sagetunic", "chest", color);
                ArmorHelper.AddArmorPiece("sagerobe", "chest", color);
                ArmorHelper.AddArmorPiece("sagehood", "head", color);
            }
    

            ItemManager.OnVanillaItemsAvailable -= new Action(AddArmorSets);
        }
    }
}
