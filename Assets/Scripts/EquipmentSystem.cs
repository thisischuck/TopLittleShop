using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using UnityEngine.Events;

public enum EquipmentPosition
{
    Boot, Elbow, Face, Hood, Leg, Pelvis, Shoulder, Torso, Weapon, Wrist, None
}

[CreateAssetMenu(menuName = "Systems/EquipmentSystem", fileName = "EquipmentSystem")]
public class EquipmentSystem : ScriptableObject
{
    public UnityAction<EquipmentPosition> EquipmentChanged;
    public UnityAction EquipOpen;

    public InventorySystem inventorySystem;

    public SerializedDictionary<EquipmentPosition, GameItem> Equipment;

    public void Equip(GameItem item, EquipmentPosition position)
    {
        if (Equipment[position])
        {
            Equipment[position] = item;
            Debug.Log("Replaced");
        }
        else
            Equipment.Add(position, item);

        EquipmentChanged?.Invoke(position);
    }

    public bool isEquiped(GameItem item)
    {
        return Equipment.ContainsValue(item);
    }
}
