using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VRrigReferences : MonoBehaviour
{
    public Transform root;
    public Transform head;
    public Transform lHand;
    public Transform rHAnd;

    public static VRrigReferences singleTon;

    public networkPlayer localPlayer { private set; get; }

    private void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setNetworkPlayer(networkPlayer myPlayer)
    {
        localPlayer = myPlayer;
    }
}
