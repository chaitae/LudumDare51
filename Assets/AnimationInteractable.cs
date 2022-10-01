using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationInteractable : MonoBehaviour,IInteractable
{
    public Animator animator;
    bool on = false;
    [SerializeField]
    GameEvent gameEvent;
    public void CharacterEnter(CharacterControls _characterControls)
    {
    }

    public void CharacterExit(CharacterControls _characterControls)
    {
        UIManager._instance.HideInteractionText();
    }

    public void CharacterStay(CharacterControls _characterControls)
    {
        UIManager._instance.ShowInteractionText();
        if(gameEvent.isSwitch)
        {
            string action;
            if (gameEvent.isOn)
            {
                action = gameEvent.previewSetting[0];
            }
            else
            {
                action = gameEvent.previewSetting[1];
            }
            UIManager._instance.ChangeInteractionText($"{action} {gameEvent.objectName}");
        }
        else
        {
            string action = gameEvent.previewSetting[0];
            UIManager._instance.ChangeInteractionText($"{action} {gameEvent.objectName}");

        }

    }

    public void EquippedAction(CharacterControls _characterControls)
    {
    }

    public void Interact(CharacterControls _characterControls, KeyCode keyCode)
    {
        if(keyCode == KeyCode.E)
        {
            gameEvent.isOn = !gameEvent.isOn;
            animator.SetBool("switch", gameEvent.isOn);
        }
    }

}
