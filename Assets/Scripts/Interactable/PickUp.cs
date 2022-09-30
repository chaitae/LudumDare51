using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, IInteractable
{
    CharacterControls characterControls1;
    public Items.Ingredient ingredientName;
    public string itemName;
    public bool infiniteObject = false;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void CharacterEnter(CharacterControls characterControls)
    {
        characterControls1 = characterControls;
        UIManager._instance.ShowInteractionText();
        UIManager._instance.ChangeInteractionText("E to pick up item");
    }

    public void CharacterExit(CharacterControls characterControls)
    {
        UIManager._instance.HideInteractionText();

    }
    public void Consume()
    {
        Destroy(transform.parent.gameObject);
    }
    public void Interact(CharacterControls characterControls, KeyCode keyCode)
    {
        if (characterControls.equippedObject == null)
        {
            if (infiniteObject)
            {
                GameObject temp = Instantiate(gameObject);
                temp.GetComponent<PickUp>().infiniteObject = false;
                characterControls1 = characterControls;
                characterControls.PickUp(temp);

            }
            else
            {
                //this is pick up..
                characterControls1 = characterControls;
                characterControls.PickUp(gameObject);
                UIManager._instance.ShowSpecialInteraction();
                UIManager._instance.ChangeSpecialInteractionText("E to drop item");
            }

            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }
    public void EquippedAction(CharacterControls characterControls)
    {
        characterControls.Drop();
        UIManager._instance.HideSpecialInteraction();

    }

    public void CharacterStay(CharacterControls _characterControls)
    {
    }
}
