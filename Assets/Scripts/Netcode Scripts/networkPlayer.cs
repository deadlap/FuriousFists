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
        score.OnValueChanged += scoreChanged;
        DisableClientInputs();
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

    public void DisableClientInputs()
    {
        if (IsClient && !IsHost)
        {
            var clientCamera = GetComponentInChildren<Camera>();

            clientCamera.enabled = false;
            animator.enabled = false;
        }
    }

    public void IncrementScore()
    {
        IncrementScoreServerRPC();
    }

    [ServerRpc]
    public void IncrementScoreServerRPC()
    {
        score.Value++;
    }
    public void scoreChanged(int oldValue, int currentValue)
    {
        print(currentValue);
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        score.OnValueChanged -= scoreChanged;
    }
}
