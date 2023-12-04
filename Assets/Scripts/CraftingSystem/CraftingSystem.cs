using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public InventorySystem Is;

    public void Craft(GameItem i)
    {
        Debug.Log("[Craft] Called");
        bool canCraft = true;
        //spend the items
        foreach (var kv in i.recipe.recipe)
        {
            //Find object
            //Check if i have enough
            if (Is.Contains(kv.Key) < kv.Value)
            {
                canCraft = false;
                Debug.Log("Not Enough Resources");
                break;
            }
        }
        if (canCraft)
        {
            foreach (var kv in i.recipe.recipe)
            {
                Is.RemoveFromInventory(kv.Key, kv.Value);
            }
            Is.AddToInventory(i, 1);
        }
    }
}
