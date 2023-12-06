using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    public EquipmentSystem equipmentSystem;
    public InventorySystem inventorySystem;

    public Transform holder;
    public GameObject prefab;

    public GameObject Character;

    //Instantiate all Equipment not equiped from Inventory.
    public void Populate()
    {
        foreach (var kv in inventorySystem.inventory)
        {
            if (kv.Key.type == Inventory.TypeOfObject.Equipment)
            {
                if (!equipmentSystem.isEquiped(kv.Key))
                {
                    GameObject o = Instantiate(prefab, holder);
                    o.name = kv.Key.id;
                    o.GetComponentInChildren<Image>().sprite = kv.Key.itemSprite;
                    o.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        foreach (var kv2 in equipmentSystem.Equipment)
                        {
                            if (kv2.Value == kv.Key)
                            {
                                button_click(o.name, kv2.Key);
                            }
                        }
                    });
                }
            }
        }
    }

    public void button_click(string id, EquipmentPosition position)
    {

    }
}
