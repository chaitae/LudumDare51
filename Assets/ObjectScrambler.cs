using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScrambler : MonoBehaviour
{
    public List<Transform> transformChildren;
    float elaspedTime = 0;
    float targetTime = 10;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            transformChildren.Add(child);
        }
        //interactableGameObjects = transform.getchildre
    }
    void ScramblePositions()
    {
        //maybe only scramble one position
        int firstRand = Random.Range(0,transformChildren.Count);
        int secondRand = Random.Range(0,transformChildren.Count);
        int thirdRand = Random.Range(0, transformChildren.Count);
        while(secondRand == firstRand)
        {
            secondRand = Random.Range(0, transformChildren.Count);
        }
        //swap position of a thing
        Vector3 tempPosition = transformChildren[firstRand].position;
        transformChildren[firstRand].position = transformChildren[secondRand].position;
        transformChildren[secondRand].position = transformChildren[thirdRand].position;
        transformChildren[thirdRand].position = tempPosition;
        
    }
    // Update is called once per frame
    void Update()
    {
        elaspedTime += Time.deltaTime;
        if(elaspedTime >= targetTime)
        {
            elaspedTime = 0;
            ScramblePositions();
        }
    }
}
