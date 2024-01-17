using BepInEx;
using HarmonyLib;
using LethalLib.Modules;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;


using System.Collections.Generic;

namespace bigManItem
{
    [BepInPlugin(GUID, NAME, VERSION)]

    public class Plugin : BaseUnityPlugin
    {
        const string GUID = "squibs.spoon";
        const string NAME = "Splatoon Scrap";
        const string VERSION = "0.0.1";

        List<Dictionary<string, string>> allAssets = new List<Dictionary<string, string>> 
        {
            new Dictionary<string, string> 
            {
                {"name", "BigMan"}, 
                {"location", "Assets/BigManItem.asset"},
                {"nodeName", $"BigManInfoNode"},
                {"nodeDisplay", "This info is about Big Man"}
            },
            new Dictionary<string, string> 
            {
                {"name", "SmallFry"},
                {"location", "Assets/Little Buddy/SmallFryItem.asset"},
                {"nodeName", $"SmallFryInfoNode"},
                {"nodeDisplay", "This info is about Small Fry"}
            },
            new Dictionary<string, string> 
            {
                {"name", "GoldenEgg"},
                {"location", "Assets/GoldenEgg/GoldenEggItem.asset"},
                {"nodeName", $"GoldenEggInfoNode"},
                {"nodeDisplay", "This info is about Golden Egg"}
            }
        };

        public static Plugin instance;
        void Awake()
        {
            instance = this;

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "itemmod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);
            
            foreach(var asset in  allAssets)
            {
                Logger.LogInfo($"Registering {asset["name"]}");
                Item assetBundle = bundle.LoadAsset<Item>(asset["location"]);
                
                NetworkPrefabs.RegisterNetworkPrefab(assetBundle.spawnPrefab);
                Utilities.FixMixerGroups(assetBundle.spawnPrefab);
                Items.RegisterScrap(assetBundle, 1000, Levels.LevelTypes.All);

                TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
                node.name = asset["nodeName"];
                node.clearPreviousText = true;
                node.displayText = asset["nodeDisplay"];
                node.maxCharactersToType = 35;

                Items.RegisterShopItem(assetBundle, null, null, node, 0);

            }
        }
    }
}


