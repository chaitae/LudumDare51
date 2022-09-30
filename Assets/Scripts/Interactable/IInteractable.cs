using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    void Interact(CharacterControls _characterControls, KeyCode keyCode);
    void CharacterEnter(CharacterControls _characterControls);
    void CharacterExit(CharacterControls _characterControls);
    void CharacterStay(CharacterControls _characterControls);
    void EquippedAction(CharacterControls _characterControls);
}
