using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : MonoBehaviour, IInteractable
{
    CharacterControls characterControls;
    IDiggable diggableThing;
    bool equipped = false;
    public void CharacterEnter(CharacterControls _characterControls)
    {
        UIManager._instance.ShowInteractionText();
        UIManager._instance.ChangeInteractionText("Press E to pick up");

    }

    public void CharacterExit(CharacterControls _characterControls)
    {
        UIManager._instance.HideInteractionText();
    }
    public void EquippedAction(CharacterControls _characterControls)
    {
        if (CheckDiggables())
        {
            diggableThing.Dig();
        }
    }
    bool CheckDiggables()
    {
        LayerMask mask = LayerMask.GetMask("Player");
        RaycastHit hit;
        for (int i = 0; i < 3; i++)
        {
            if (Physics.Raycast(characterControls.transform.position + characterControls.transform.forward * i + Vector3.up, -Vector3.up, out hit, Mathf.Infinity,~mask))
            {
                Debug.DrawRay(characterControls.transform.position + characterControls.transform.forward * i + Vector3.up, -Vector3.up, Color.green, 2, false);

                IDiggable temp = hit.collider.gameObject.GetComponent<IDiggable>();
                if (temp != null)
                {
                    if (!temp.HasBeenDug())
                    {
                        diggableThing = temp;
                        UIManager._instance.ChangeSpecialInteractionText("Press E to dig");
                        UIManager._instance.ShowSpecialInteraction();
                    }

                    return true;
                }
                else
                {
                    UIManager._instance.ChangeSpecialInteractionText("Press X to drop shovel");
                }
            }
            else
            {
                diggableThing = null;
            }

        }

        return false;
    }

    public void Interact(CharacterControls _characterControls, KeyCode keyCode)
    {
        Debug.Log("interacted shovel");
        if (_characterControls.equippedObject == null)
        {

            UIManager._instance.ChangeSpecialInteractionText("Press X to drop shovel");
            UIManager._instance.ShowSpecialInteraction();
            equipped = true;
            _characterControls.PickUp(gameObject);
            characterControls = _characterControls;

        }
    }
    void Update()
    {
        if (equipped)
        {
            CheckDiggables();
        }
        if (equipped && Input.GetKeyDown(KeyCode.X))
        {

            UIManager._instance.HideSpecialInteraction();
            if (characterControls.CanDrop())
            {
                characterControls.Drop();
                characterControls = null;
                equipped = false;
            }
        }
    }

    public void CharacterStay(CharacterControls _characterControls)
    {
    }
}
