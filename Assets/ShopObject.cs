using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopObject : MonoBehaviour
{
    private PlayerInput playerInput;
    public ShopSystem shopSystem;
    public GameObject Label;
    public bool interacted = false;

    public List<GameItem> itemsDisplayed;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //! Rework This if i have time. 
        //! I don't like how it stands.
        //! Maybe SC
        playerInput = GameObject.FindGameObjectWithTag("InputSystem").GetComponent<PlayerInput>();
        playerInput.actions["Interaction"].performed += OnInteraction;
        playerInput.actions["Interaction"].started += OnInteraction;
        playerInput.actions["Interaction"].canceled += OnInteraction;
    }

    public void GetList()
    {
        print("Displaying List");
    }

    public void OnInteraction(InputAction.CallbackContext context)
    {
        interacted = context.ReadValueAsButton();
    }

    public void LabelVisibility(bool value)
    {
        Label.SetActive(value);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!interacted)
                LabelVisibility(true);
            else
                GetList();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            LabelVisibility(false);
        }
    }
}

