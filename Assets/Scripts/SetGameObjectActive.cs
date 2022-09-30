using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameObjectActive : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetGO;
    public void SetGameObjectActiveMethod()
    {
        targetGO.SetActive(true);
    }
}
