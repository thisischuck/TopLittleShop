using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Systems/ShopSystem")]
public class ShopSystem : ScriptableObject
{
    public UnityAction ShopOpen;

    public SerializedDictionary<GameItem, int> ShopInventory;

    public bool Buy(GameItem item, InventorySystem inventorySystem)
    {
        if (ContainsById(item.id) && ShopInventory[item] > 0 && inventorySystem.MoneyInTheBank > item.worth)
        {
            ShopInventory[item] -= 1;
            Debug.Log("BuysIt");
            inventorySystem.AddToInventory(item, 1);
            inventorySystem.MoneyInTheBank -= item.worth;
            if (ShopInventory[item] <= 0)
                ShopInventory.Remove(item);
            return true;
        }
        return false;
    }

    public bool Sell(GameItem item, InventorySystem inventorySystem)
    {
        if (ContainsById(item.id))
        {
            //Finds Item Elsewhere
            ShopInventory.Add(item, 1);
            Debug.Log("SellsIt");
            inventorySystem.MoneyInTheBank += item.worth;
            inventorySystem.RemoveFromInventory(item, 1);
            return true;
        }
        return false;
    }

    public GameItem ContainsById(string id)
    {
        foreach (var k in ShopInventory.Keys)
        {
            if (k.id == id)
                return k;
        }
        return null;
    }
}