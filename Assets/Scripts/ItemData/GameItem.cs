using System.IO;
using UnityEngine;
using Inventory;
using Quests;
using Newtonsoft.Json;
using MyJsonConverters;
using UnityEditor;

[CreateAssetMenu(menuName = "Items/GameItem")]
public class GameItem : ScriptableObject
{
    public string id;
    public TypeOfObject type;
    public string itemName;
    public ResourceTag tag;
    public int weight;
    public int health;
    public int worth;

    public bool isBothSides = false;
    [JsonIgnore]
    public Sprite itemSprite, rightSprite;

    public CraftRecipe recipe = null;

    public static GameItem Create(GameItem i)
    {
        string pathName = i.id + ".asset";
        GameItem item = Resources.Load<GameItem>("Resources/Data/GameItems/" + i.id);
        if (item != null)
        {
            Debug.Log("item exists");
            return null;
        }

        item = CreateInstance<GameItem>();
        item.id = i.id;
        item.itemName = i.itemName;
        item.type = i.type;
        item.tag = i.tag;
        item.weight = i.weight;
        item.health = i.health;

        if (item.recipe != null)
        {
            Debug.Log("Recipe not null");
            CraftRecipe recipeItem = Resources.Load<CraftRecipe>("Data/GameItems/Recipes/" + i.id);
            if (recipeItem != null)
            {
                Debug.Log("File Exists");
                item.recipe = recipeItem;
            }
            else
            {
                Debug.Log("Files does not Exist");
                item.recipe = CraftRecipe.Create(item.recipe);
            }
        }


        AssetUtils.CreateAsset<GameItem>("Assets/Resources/Data/GameItems/" + pathName, item);
        return item;
    }
}
