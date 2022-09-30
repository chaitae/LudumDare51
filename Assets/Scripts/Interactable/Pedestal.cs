using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Can place whatever item on this thing puts it specific location
public class Pedestal : MonoBehaviour, IInteractable
{
    public GameObject objectPosition;
    public GameObject placedObject;
    public bool allowPickUp = true;
    public void CharacterEnter(CharacterControls characterControls)
    {
        if (characterControls.equippedObject != null && placedObject == null)
        {
            UIManager._instance.ChangeInteractionText("Press e to place");
            UIManager._instance.ShowInteractionText();

        }

        if (placedObject != null)
        {
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().ShowSpecial();
            }
        }
    }

    public void CharacterExit(CharacterControls characterControls)
    {
        UIManager._instance.HideInteractionText();

        if (placedObject != null)
        {
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().HideSpecial();

            }
        }
    }

    public void CharacterStay(CharacterControls _characterControls)
    {
    }

    public void EquippedAction(CharacterControls characterControls)
    {
    }

    public void Interact(CharacterControls characterControls, KeyCode keyCode)
    {
        if (keyCode == KeyCode.T)
        {
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().Interact(characterControls, keyCode);
            }
        }

        if (characterControls.equippedObject != null && placedObject == null) // place on pedestle
        {
            Debug.Log("Place item on pedastal");
            characterControls.equippedObject.transform.position = objectPosition.transform.position;
            characterControls.equippedObject.transform.parent = null;

            if (characterControls.equippedObject.GetComponent<Rigidbody>() != null)
                characterControls.equippedObject.GetComponent<Rigidbody>().isKinematic = false;
            placedObject = characterControls.equippedObject;
            placedObject.GetComponent<Collider>().enabled = false;
            characterControls.equippedObject = null;
            if (placedObject.GetComponent<SpecialNPC>() != null)
            {
                placedObject.GetComponent<SpecialNPC>().ShowSpecial();
            }
            allowPickUp = true;
            UIManager._instance.HideInteractionText();

        }
        else if (keyCode != KeyCode.T && characterControls.equippedObject == null && allowPickUp) //pickup code
        {

            if (placedObject != null)
                characterControls.PickUp(placedObject);
            placedObject.GetComponent<Collider>().enabled = true;
            placedObject = null;

        }
    }

}
