using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.XR.CoreUtils;
public class networkGrabable : NetworkBehaviour
{
    //Hvis folk skal samle ting op og serveren skal vide hvem der gør det (hvilken client), kunne være godt hvis man har pickups i spillet

    private NetworkObject netObject;



    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        netObject = GetComponent<NetworkObject>();
        XRGrabInteractable theGunGrab = GetComponent<XRGrabInteractable>();
        theGunGrab.hoverEntered.AddListener(touched);

    }

    public void touched(HoverEnterEventArgs arg)
    {
        var interactorXR = arg.interactorObject.transform.gameObject.GetComponentInParent<XROrigin>();

        if (interactorXR.gameObject == VRrigReferences.singleTon.gameObject)
        {
            var player = VRrigReferences.singleTon.localPlayer;
            player.IncrementScore();
        }

    }



    public void requestOwnerShip()
    {
        requestOwnerShip_ServerRpc(NetworkManager.Singleton.LocalClient.ClientId);

    }

    //adding a specification for the method under (..._ServerRpc)
    [ServerRpc(RequireOwnership = false)]

    private void requestOwnerShip_ServerRpc(ulong clientID)
    {
        netObject.ChangeOwnership(clientID);
        Debug.Log("Yup the ownership changed");


    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("super collider");
        if (collision.gameObject.CompareTag("Player") && IsOwner)
        {
            var player = collision.gameObject.GetComponent<networkPlayer>();
            player.IncrementScoreServerRPC();

        }
    }
}
