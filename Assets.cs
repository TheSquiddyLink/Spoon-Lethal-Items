using BepInEx;
using HarmonyLib;
using LethalLib.Modules;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;



namespace bigManItem
{
    [BepInPlugin(GUID, NAME, VERSION)]

    public class Plugin : BaseUnityPlugin
    {
        const string GUID = "squibs.bigMan";
        const string NAME = "Big Man Item";
        const string VERSION = "0.0.1";

        public static Plugin instance;
        void Awake()
        {
            instance = this;

            string assetDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "itemmod");
            AssetBundle bundle = AssetBundle.LoadFromFile(assetDir);

            Item assetBundle = bundle.LoadAsset<Item>("Assets/BigManItem.asset");


            NetworkPrefabs.RegisterNetworkPrefab(assetBundle.spawnPrefab);
            Utilities.FixMixerGroups(assetBundle.spawnPrefab);

            TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
            node.name = $"BigManInfoNode";
            node.clearPreviousText = true;
            node.displayText = "This info is about Big Man";
            node.maxCharactersToType = 35;
            
            assetBundle = bundle.LoadAsset<Item>("Assets/BigManItem.asset");
            Items.RegisterShopItem(assetBundle, buyNode1, buyNode2, node, 0);



        }
    }
}


