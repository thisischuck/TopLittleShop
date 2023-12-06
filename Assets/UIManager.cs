using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public ShopSystem shopSystem;

    public GameObject ShopUI;
    // Start is called before the first frame update
    void Start()
    {
        shopSystem.ShopOpen += OpenShop;
    }

    void OpenShop()
    {
        ShopUI.SetActive(true);
    }
}
