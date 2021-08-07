using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using Jotunn.Managers;
using SimpleJson;
using System;
using System.IO;

namespace ReworkedArmors
{
    [BepInPlugin("Detalhes.ReworkedArmors", "ReworkedArmors", "1.0.0")]
    [BepInProcess("valheim.exe")]
    public class ReworkedArmors : BaseUnityPlugin
    {
        public const string PluginGUID = "Detalhes.ReworkedArmors";
        Harmony harmony = new Harmony(PluginGUID);
        public static readonly string ModPath = Path.GetDirectoryName(typeof (ReworkedArmors).Assembly.Location);
        public static Root root = new Root();

        public static ConfigEntry<int> nexusID;

        private void Awake()
        {
            root = SimpleJson.SimpleJson.DeserializeObject<Root>(File.ReadAllText(Path.Combine(ModPath, "armorConfig.json")));

            nexusID = Config.Bind<int>("General", "NexusID", 1420, "Nexus mod ID for updates");

            harmony.PatchAll();
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

            ItemManager.OnVanillaItemsAvailable -= new Action(AddArmorSets);
        }
    }
}
