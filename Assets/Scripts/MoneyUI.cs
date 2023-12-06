using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    public InventorySystem inventorySystem;
    public TextMeshProUGUI textMesh;

    void Start()
    {
        inventorySystem.moneyChanged += UpdateMoney;
    }

    void UpdateMoney(int money)
    {
        textMesh.text = money.ToString();
    }
}
