using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour
{
    public List<Color> colors;
    public Material material;
    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<MeshRenderer>().materials[0];
        material.color = colors[Random.Range(0,colors.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
