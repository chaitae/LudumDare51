using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grave : MonoBehaviour, IDiggable
{
    public GameObject treasure;
    public GameObject treasureLandingPosition;
    public GameObject dugGraveMesh;
    public GameObject graveMesh;
    
    bool hasBeenDug = false;
    public void Dig()
    {
        if (!hasBeenDug)
        {
            hasBeenDug = true;
            Debug.Log("I'm digging");
            if (graveMesh != null)
                graveMesh.SetActive(false);
            if (dugGraveMesh != null)
                dugGraveMesh.SetActive(true);
            if (treasure != null)
            {
                treasure.SetActive(true);
            }
        }
    }

    public bool HasBeenDug()
    {
       return hasBeenDug;
    }
}
