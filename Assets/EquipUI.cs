using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipUI : MonoBehaviour
{
    public EquipmentSystem equipmentSystem;
    public InventorySystem inventorySystem;

    public Transform holder;
    public GameObject prefab;

    public GameObject Character;

    void OnEnable()
    {
        equipmentSystem.EquipmentChanged += OnEquipmentChange;
        Populate();
    }

    void OnEquipmentChange(EquipmentPosition pos)
    {
        Populate();
    }

    //Instantiate all Equipment not equiped from Inventory.
    public void Populate()
    {
        for (int i = holder.childCount - 1; i >= 0; i--)
        {
            Destroy(holder.GetChild(i).gameObject);
        }
        foreach (var kv in inventorySystem.inventory)
        {
            if (kv.Key.type == Inventory.TypeOfObject.Equipment)
            {
                if (!equipmentSystem.isEquiped(kv.Key))
                {
                    if (holder.Find(kv.Key.id))
                        return;
                    GameObject o = Instantiate(prefab, holder);
                    o.name = kv.Key.id;
                    o.transform.Find("Sprite").GetComponent<Image>().sprite = kv.Key.itemSprite;
                    o.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        equipmentSystem.Equip(kv.Key, kv.Key.EquipmentPosition);
                    });
                }
            }
        }
    }
}
