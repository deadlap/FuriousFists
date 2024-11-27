using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Respawn : MonoBehaviour
{
    public Transform xrRig;
    public Transform teleportTarget;
    public Transform player;
    public List<GameObject> gameObjects;
    private void OnCollisionEnter(Collision collision)
    {
        Teleport();
        SetActive();
    }

     private void Teleport()
    {
        xrRig.position = teleportTarget.position;
        xrRig.rotation = teleportTarget.rotation;
        player.position = xrRig.position;
        player.rotation = xrRig.rotation;

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
