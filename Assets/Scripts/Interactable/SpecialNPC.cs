using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using Yarn.Unity;

public class SpecialNPC : MonoBehaviour, IInteractable
{
    // attached to the non-player characters, and stores the name of the Yarn
    /// node that should be run when you talk to them.
    // public class 
    [System.Serializable]
    public class GameEventDialogueNode
    {
        public GameEvent gameEvent;
        public string dialogueNode;
    }
    public List<GameEventDialogueNode> gameEventDialogueNodes = new List<GameEventDialogueNode>();
    DialogueUI dialogueUI;
    public List<string> ItemTalkNodes;
    public string characterName = "";
    public string defaultTalkNode = "";
    public LookAtConstraint lookAtConstraint;
    bool listening = false;

    [Header("Optional")]
    public YarnProgram scriptToLoad;

    void Awake()
    {
    }
    public void ShowSpecial()
    {
        UIManager._instance.ShowSpecialInteraction();
    }
    public void HideSpecial()
    {
        UIManager._instance.HideSpecialInteraction();

    }

    public void CharacterEnter(CharacterControls characterControls)
    {
        characterControls.canUseObject = false;
        UIManager._instance.ShowInteractionText();
        //UIManager._instance.ChangeInteractionText("Press T to talk");
        UIManager._instance.ShowSpecialInteraction();
        UIManager._instance.ChangeSpecialInteractionText("Press E to pick up");
    }

    public void CharacterExit(CharacterControls characterControls)
    {

        characterControls.canUseObject = true;
        UIManager._instance.HideInteractionText();
        UIManager._instance.HideSpecialInteraction();
    }

    public void EquippedAction(CharacterControls characterControls)
    {
        if (characterControls.CanDrop())
            characterControls.Drop();
    }
    string CheckPlayerShowingKeyDialogueItem(string equippedItemName)
    {
        foreach (string talkNode in ItemTalkNodes)
        {
            //Debug.Log(talkNode + "talk nodes!");
            if (equippedItemName.ToLower().Equals(talkNode.ToLower()))
            {
                // talkNodetoUse = talkNode;
                // Debug.Log(talkNode);
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
        if (keyCode == KeyCode.T)
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

            FindObjectOfType<DialogueRunner>().StartDialogue(talkNodetoUse);


            characterControls.SetMovement(false, "NPC");
            dialogueRunner = FindObjectOfType<DialogueRunner>();
            listening = true;
            dialogueRunner.onDialogueComplete.AddListener(() => EnableMovement(characterControls));
            dialogueRunner.onDialogueComplete.AddListener(() => UIManager._instance.ContinueCheckingForNearInteractable());
            dialogueRunner.onDialogueComplete.AddListener(() => { ShowSpecial(); UIManager._instance.ShowInteractionText(); });
            UIManager._instance.StopCheckingForNearInteractable();
            UIManager._instance.HideInteractionText();
            UIManager._instance.HideSpecialInteraction();
        }
        else if (keyCode == KeyCode.E)
        {
            if (characterControls.equippedObject == null)
            {

                characterControls.PickUp(gameObject);
                if (GetComponent<Rigidbody>() != null)
                {
                    GetComponent<Rigidbody>().isKinematic = true;
                }
            }
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
        //        throw new System.NotImplementedException();
    }
}
