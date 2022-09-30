/*

The MIT License (MIT)

Copyright (c) 2015-2017 Secret Lab Pty. Ltd. and Yarn Spinner contributors.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

*/

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Yarn.Unity;

/// attached to the non-player characters, and stores the name of the Yarn
/// node that should be run when you talk to them.
// public class 
[System.Serializable]
public class GameEventDialogueNode
{
    public GameEvent gameEvent;
    public string dialogueNode;
}
public class NPC : MonoBehaviour, IInteractable
{

    public Animator animator;
    public List<GameEventDialogueNode> gameEventDialogueNodes = new List<GameEventDialogueNode>();
    DialogueUI dialogueUI;
    public List<string> ItemTalkNodes;
    public string characterName = "";
    public string defaultTalkNode = "";
    bool listening = false;
    bool characterNear = false;
    CharacterControls characterControls1;

    [Header("Optional")]
    public YarnProgram scriptToLoad;
    [YarnCommand("PlayAnimation")]
    public void PlayAnimation(string animation)
    {
        animator.Play(animation);
    }
    [ContextMenu("Startdialogue")]
    public void Test()
    {
        StartDialogueNode("Sophie.Failed");
    }
    public void StartDialogueNode(string nodeName)
    {
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
       if (dialogueRunner.IsDialogueRunning) return;
        dialogueRunner.StartDialogue(nodeName);

    }
    public void CharacterEnter(CharacterControls characterControls)
    {
        characterNear = true;
        characterControls1 = characterControls;
        characterControls.canUseObject = false;
        UIManager._instance.ShowInteractionText();
        UIManager._instance.ChangeInteractionText("Press e to talk");

    }

    public void CharacterExit(CharacterControls characterControls)
    {

        characterNear = false;
        characterControls1 = null;
        characterControls.canUseObject = true;

        UIManager._instance.HideInteractionText();
    }

    public void EquippedAction(CharacterControls characterControls)
    {
    }
    string CheckPlayerShowingKeyDialogueItem(string equippedItemName)
    {
        foreach (string talkNode in ItemTalkNodes)
        {
            if (equippedItemName.ToLower().Equals(talkNode.ToLower()))
            {
                return talkNode;
            }
        }
        return "";
    }
    string CheckEventsTriggered()
    {
        if (gameEventDialogueNodes.Count == 0) return "";
        for (int i = gameEventDialogueNodes.Count - 1; i >= 0; i--)
        {
            if (gameEventDialogueNodes[i].gameEvent.eventRaised)
            {
                return gameEventDialogueNodes[i].dialogueNode;
            }
        }
        return "";
    }
    public void Interact(CharacterControls characterControls, KeyCode keyCode)
    {
        DialogueRunner dialogueRunner = FindObjectOfType<DialogueRunner>();
        if (dialogueRunner.IsDialogueRunning) return;

        string talkNodetoUse = defaultTalkNode;
        if (CheckEventsTriggered() != "") talkNodetoUse = CheckEventsTriggered();
        if (characterControls.equippedObject != null)
        {
            if (CheckPlayerShowingKeyDialogueItem(characterControls.equippedObject.name) != "")
            {
                talkNodetoUse = CheckPlayerShowingKeyDialogueItem(characterControls.equippedObject.name);
            }
        }
        if (dialogueRunner.NodeExists(defaultTalkNode))
        {
            dialogueRunner.StartDialogue(talkNodetoUse);
            characterControls.SetMovement(false, "NPC");
            dialogueRunner = FindObjectOfType<DialogueRunner>();
            listening = true;
            dialogueRunner.onDialogueComplete.AddListener(() => EnableMovement(characterControls));
            dialogueRunner.onDialogueComplete.AddListener(() => UIManager._instance.ContinueCheckingForNearInteractable());
            dialogueRunner.onDialogueComplete.AddListener(() => listening = false);
            UIManager._instance.StopCheckingForNearInteractable();
            UIManager._instance.HideInteractionText();
        }
    }
    void EnableMovement(CharacterControls characterControls)
    {
        characterControls.SetMovement(true, "NPC");
    }

    void Start()
    {
        if (scriptToLoad != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.Add(scriptToLoad);
            if (!dialogueRunner.NodeExists(defaultTalkNode))
            {
                Debug.LogError("Default node name doesn't exist in the script " + gameObject);
            }
        }
        dialogueUI = FindObjectOfType<Yarn.Unity.DialogueUI>();

    }
    void Update()
    {
        if (listening)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueUI.MarkLineComplete();
            }
        }
    }

    public void CharacterStay(CharacterControls _characterControls)
    {
        
        UIManager._instance.ShowInteractionText();
        UIManager._instance.ChangeInteractionText("Press e to talk");
    }
}


