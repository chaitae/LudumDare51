using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class LookAt : MonoBehaviour, IInteractable
{
    public delegate void GeneralFunction();
    public event GeneralFunction OnLookAt;
    public event GeneralFunction OnLookAway;
    public CinemachineVirtualCamera cinemachineVirtualCamera;
    CharacterControls characterControls;
    public bool hidePlayerOnView = false;
    bool playerLooking = false;


    public void ExitLook()
    {

        UIManager._instance.HideHint();
        playerLooking = false;
        cinemachineVirtualCamera.Priority = 1;
        characterControls.SetMovement(true, "Lookat");
        characterControls.canUseObject = true;
        UIManager._instance.ContinueCheckingForNearInteractable();
        Camera.main.cullingMask = ~0;
        if (OnLookAway != null) OnLookAway();
    }
    void Update()
    {
        if (playerLooking)
        {
            if (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.E))
            {
                ExitLook();
            }
        }

    }
    // public cinemachine
    public void CharacterEnter(CharacterControls _characterControls)
    {
        UIManager._instance.ShowInteractionText();
        UIManager._instance.ChangeInteractionText("Press T to look");
    }

    public void CharacterExit(CharacterControls _characterControls)
    {

        UIManager._instance.HideInteractionText();
    }

    public void EquippedAction(CharacterControls _characterControls)
    {
    }

    public void Interact(CharacterControls _characterControls, KeyCode keyCode)
    {
        if (keyCode == KeyCode.T)
        {

            if (OnLookAt != null) OnLookAt();
            Debug.Log("look at button");
            _characterControls.canUseObject = false;
            cinemachineVirtualCamera.Priority = 100;
            _characterControls.SetMovement(false, " Look at set false");
            playerLooking = true;
            characterControls = _characterControls;
            UIManager._instance.StopCheckingForNearInteractable();
            UIManager._instance.HideInteractionText();
            UIManager._instance.ShowHintText("Press E to escape view");
            if (hidePlayerOnView)
            {
                Camera.main.cullingMask = ~LayerMask.GetMask("Player");
            }
        }

    }

    public void CharacterStay(CharacterControls _characterControls)
    {
    }
}
