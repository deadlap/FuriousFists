using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform xrRig;  
    public Transform teleportTarget;
    public Transform player;

    private void OnCollisionEnter(Collision collision)
    {
  
         Teleport();
    }

    void Teleport()
    {
        xrRig.position = teleportTarget.position;
        xrRig.rotation = teleportTarget.rotation;
        player.position = xrRig.position;
        player.rotation = xrRig.rotation;

    }
}
