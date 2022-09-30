using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class DialogueCommands : MonoBehaviour
{

    // Drag and drop your Dialogue Runner into this variable.
    public DialogueRunner dialogueRunner;

    public void Awake()
    {
        dialogueRunner = FindObjectOfType<DialogueRunner>();

        dialogueRunner.AddCommandHandler(
            "ChangeScene",
            ChangeScene
        );
        dialogueRunner.AddCommandHandler(
            "RaiseEvent",
            RaiseGameEvent
        );
        // Create a new command called 'camera_look', which looks at a target.
        dialogueRunner.AddCommandHandler(
            "camera_look",     // the name of the command
            CameraLookAtTarget // the method to run
        );
    }
    
    void ChangeScene(string[] parameters)
    {
        SceneManager.LoadScene(parameters[0]);
    }
    void RaiseGameEvent(string[] parameters)
    {
        Debug.Log(GameManager._instance + " hello this is checking game manager");
        Debug.Log("parameters[0]" + parameters[0]);
        GameManager._instance.gameEventsDirectory[parameters[0]].Raise();
    }

    // The method that gets called when '<<camera_look>>' is run.
    private void CameraLookAtTarget(string[] parameters)
    {

        // Take the first parameter, and use it to find the object
        string targetName = parameters[0];
        GameObject target = GameObject.Find(targetName);

        // Log an error if we can't find it
        if (target == null)
        {
            Debug.LogError($"Cannot make camera look at {targetName}:" +
                "cannot find target");
            return;
        }

        // Make the main camera look at this target
        Camera.main.transform.LookAt(target.transform);
    }
}
