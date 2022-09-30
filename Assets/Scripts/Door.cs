using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : GameEventListener
{
    public LookAt lookAt;
    IEnumerator WaitSetActive()
    {
        yield return new WaitForSeconds(.5f);
        gameObject.SetActive(false);
    }
    public void OpenDoor()
    {
        lookAt.ExitLook();
        StartCoroutine("WaitSetActive");
    }
}
