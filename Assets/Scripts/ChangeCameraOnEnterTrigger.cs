using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ChangeCameraOnEnterTrigger : MonoBehaviour
{

    public CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterControls>() != null)
        {
            cinemachineVirtualCamera.Priority = 20;
        }
        Debug.Log("Player here");
    }
    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterControls>() != null)
        {
            cinemachineVirtualCamera.Priority = 1;

        }
        Debug.Log("Player exit");

    }
    void Start()
    {
        //Choose the distance the 
    }
    void FixedUpdate()
    {
        // RaycastHit hit;

        // Vector3 p1 = transform.position;
        // float distanceToObstacle = 0;
        // m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale, transform.forward, out m_Hit, transform.rotation, m_MaxDistance, LayerMask.GetMask("Player"));
        // if (m_HitDetect)
        // {
        //     //Output the name of the Collider your Box hit
        //     Debug.Log("Hit : " + m_Hit.collider.name);
        // }
        // // Cast a sphere wrapping character controller 10 meters forward
        // // to see if it is about to hit anything.
        // if (Physics.SphereCast(p1, 10, transform.forward, out hit, 10, LayerMask.GetMask("Player")))
        // {
        //     distanceToObstacle = hit.distance;
        //     Debug.Log(hit.collider.name);
        // }
    }
    void OnDrawGizmos()
    {
        // Gizmos.color = Color.red;

        // //Check if there has been a hit yet
        // if (m_HitDetect)
        // {
        //     //Draw a Ray forward from GameObject toward the hit
        //     Gizmos.DrawRay(transform.position, transform.forward * m_Hit.distance);
        //     //Draw a cube that extends to where the hit exists
        //     Gizmos.DrawWireCube(transform.position + transform.forward * m_Hit.distance, transform.localScale);
        // }
        // //If there hasn't been a hit yet, draw the ray at the maximum distance
        // else
        // {
        //     //Draw a Ray forward from GameObject toward the maximum distance
        //     Gizmos.DrawRay(transform.position, transform.forward * m_MaxDistance);
        //     //Draw a cube at the maximum distance
        //     Gizmos.DrawWireCube(transform.position + transform.forward * m_MaxDistance, transform.localScale);
        // }
    }
    // Update is called once per frame
    void Update()
    {
    }
}
