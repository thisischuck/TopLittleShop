using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class CharacterEquipment : MonoBehaviour
{
    public EquipmentSystem equipmentSystem;

    public SerializedDictionary<EquipmentPosition, List<GameObject>> EquipmentSprites;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        equipmentSystem.EquipmentChanged += OnEquipmentChange;
    }

    public void OnEquipmentChange(EquipmentPosition position)
    {
        foreach (var o in EquipmentSprites[position])
        {
            if (o.name.Contains("_r_"))
            {
                SpriteRenderer spriteRenderer = o.GetComponent<SpriteRenderer>();
                if (spriteRenderer)
                {
                    spriteRenderer.sprite = equipmentSystem.Equipment[position].rightSprite;
                }
            }
            else
            {
                SpriteRenderer spriteRenderer = o.GetComponent<SpriteRenderer>();
                if (spriteRenderer)
                {
                    spriteRenderer.sprite = equipmentSystem.Equipment[position].itemSprite;
                }
            }
        }
    }
}
