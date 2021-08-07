using System.Collections.Generic;

namespace ReworkedArmors
{
    internal class Data
    {
        public static Dictionary<string, ArmorSet> ArmorSets = new Dictionary<string, ArmorSet>()
    {
      {
        "rags",
        new ArmorSet()
        {
          HelmetID = "n/a",
          ChestID = "ArmorRagsChest",
          LegsID = "ArmorRagsLegs",
          HelmetName = "n/a",
          ChestName = "$item_chest_rags_t",
          LegsName = "$item_legs_rags_t"
        }
      },
      {
        "leather",
        new ArmorSet()
        {
          HelmetID = "HelmetLeather",
          ChestID = "ArmorLeatherChest",
          LegsID = "ArmorLeatherLegs",
          HelmetName = "$item_helmet_leather_t",
          ChestName = "$item_chest_leather_t",
          LegsName = "$item_legs_leather_t"
        }
      },
      {
        "trollLeather",
        new ArmorSet()
        {
          HelmetID = "HelmetTrollLeather",
          ChestID = "ArmorTrollLeatherChest",
          LegsID = "ArmorTrollLeatherLegs",
          HelmetName = "$item_helmet_trollleather_t",
          ChestName = "$item_chest_trollleather_t",
          LegsName = "$item_legs_trollleather_t"
        }
      },
      {
        "bronze",
        new ArmorSet()
        {
          HelmetID = "HelmetBronze",
          ChestID = "ArmorBronzeChest",
          LegsID = "ArmorBronzeLegs",
          HelmetName = "$item_helmet_bronze_t",
          ChestName = "$item_chest_bronze_t",
          LegsName = "$item_legs_bronze_t"
        }
      },
      {
        "iron",
        new ArmorSet()
        {
          HelmetID = "HelmetIron",
          ChestID = "ArmorIronChest",
          LegsID = "ArmorIronLegs",
          HelmetName = "$item_helmet_iron_t",
          ChestName = "$item_chest_iron_t",
          LegsName = "$item_legs_iron_t"
        }
      },
      {
        "silver",
        new ArmorSet()
        {
          HelmetID = "HelmetDrake",
          ChestID = "ArmorWolfChest",
          LegsID = "ArmorWolfLegs",
          HelmetName = "$item_helmet_drake_t",
          ChestName = "$item_chest_wolf_t",
          LegsName = "$item_legs_wolf_t"
        }
      },
      {
        "padded",
        new ArmorSet()
        {
          HelmetID = "HelmetPadded",
          ChestID = "ArmorPaddedCuirass",
          LegsID = "ArmorPaddedGreaves",
          HelmetName = "$item_helmet_padded_t",
          ChestName = "$item_chest_padded_t",
          LegsName = "$item_legs_padded_t"
        }
      }      
    };

        public class ArmorSet
        {
            public string HelmetID { get; set; }

            public string ChestID { get; set; }

            public string LegsID { get; set; }

            public string HelmetName { get; set; }

            public string ChestName { get; set; }

            public string LegsName { get; set; }
        }
    }
}
