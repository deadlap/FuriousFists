using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class networkPlayer : NetworkBehaviour
{

    [SerializeField] private Transform root;
    [SerializeField] private Transform head;
    [SerializeField] private Transform lHand;
    [SerializeField] private Transform rHand;
    [SerializeField] private Animator animator;

    private NetworkVariable<int> score = new NetworkVariable<int>();




    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsOwner)
        {
            VRrigReferences.singleTon.setNetworkPlayer(this);
        }
        if (GetComponent<NetworkObject>().NetworkObjectId == 1){
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player1"));
        } else {
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player2"));
        }
        // DisableClientInputs();
    }



    // Update is called once per frame
    void Update()
    {
        if (IsOwner)
        {
            root.position = VRrigReferences.singleTon.root.position;
            root.rotation = VRrigReferences.singleTon.root.rotation;

            head.position = VRrigReferences.singleTon.head.position;
            head.rotation = VRrigReferences.singleTon.head.rotation;

            lHand.position = VRrigReferences.singleTon.lHand.position;
            lHand.rotation = VRrigReferences.singleTon.lHand.rotation;

            rHand.position = VRrigReferences.singleTon.rHAnd.position;
            rHand.rotation = VRrigReferences.singleTon.rHAnd.rotation;




        }
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }
    private void SetGameLayerRecursive(GameObject _gameObject, int layer) {
         _gameObject.layer = layer;
        foreach (Transform child in _gameObject.transform) {
                child.gameObject.layer = layer;

                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if (_HasChildren != null)
                    SetGameLayerRecursive(child.gameObject, layer);
            }
    }
}
