using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // public Transform xrRig;
    public Transform teleportTarget;
    // public Transform player;

    // private void OnCollisionEnter(Collision collision) {
    //     Teleport();
    // }

    public void Teleport(GameObject gameObject)
    {
        gameObject.transform.position = teleportTarget.position;
        gameObject.transform.rotation = teleportTarget.rotation;
        // player.position = xrRig.position;
        // player.rotation = xrRig.rotation;


    }
}
