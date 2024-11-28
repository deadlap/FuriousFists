using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Respawn : MonoBehaviour
{
    public Transform xrRig;
    public Transform teleportTarget1;
    public Transform teleportTarget2;
    // public Transform player;
    public List<GameObject> gameObjects;
    private void OnCollisionEnter(Collision collision) {
        Teleport();
        SetActive();
    }

     private void Teleport() {
        if (xrRig.tag == "Player1") {
            xrRig.position = teleportTarget1.position;
            xrRig.rotation = teleportTarget1.rotation;
        } else {
            xrRig.position = teleportTarget2.position;
            xrRig.rotation = teleportTarget2.rotation;
        }
        // player.position = xrRig.position;
        // player.rotation = xrRig.rotation;

    }

    private void SetActive()
    {
        foreach (GameObject go in gameObjects)
        {
            if (go != null)
            {
                go.SetActive(true);
            }
        }
    }
}
