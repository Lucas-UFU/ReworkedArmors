using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using SimpleJson;
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

            nexusID = Config.Bind<int>("General", "NexusID", 1419, "Nexus mod ID for updates");

            harmony.PatchAll();
            LoadAssets();
        }

        private void LoadAssets()
        {
            ModExistingSets.Init();
            AddNewSets.Init();
        }       
    }
}
