using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ShopSystem shopSystem;
    public EquipmentSystem equipmentSystem;

    public GameObject ShopUI, EquipUI;
    // Start is called before the first frame update
    void Start()
    {
        shopSystem.ShopOpen += OpenShop;
        equipmentSystem.EquipOpen += OpenEquip;
    }

    void OpenShop()
    {
        ShopUI.SetActive(true);
    }

    void OpenEquip()
    {
        EquipUI.SetActive(true);
    }
}
