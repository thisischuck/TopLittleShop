using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Inventory;
using Quests;
using Newtonsoft.Json;
using MyJsonConverters;
using AYellowpaper.SerializedCollections;

[CreateAssetMenu(menuName = "Items/CraftRecipe")]
public class CraftRecipe : ScriptableObject
{
    public string id;
    [JsonConverter(typeof(RewardsJsonConveter))]
    public SerializedDictionary<string, int> recipe;
    public string itemId;
    public bool locked = false;
    public static CraftRecipe Create(CraftRecipe i)
    {
        string pathName = i.id + ".asset";
        if (File.Exists("Assets/ScriptableObjects/GameItems/Recipes/" + pathName)) return null;

        CraftRecipe item = CreateInstance<CraftRecipe>();
        item.id = i.id;
        item.itemId = i.itemId;
        item.recipe = new SerializedDictionary<string, int>(i.recipe);
        AssetUtils.CreateAsset<CraftRecipe>("Assets/ScriptableObjects/GameItems/Recipes/" + pathName, item);
        return item;
    }
}