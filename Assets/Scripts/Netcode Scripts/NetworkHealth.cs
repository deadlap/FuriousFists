using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkHealth : NetworkBehaviour
{
    [HideInInspector]
    public NetworkVariable<int> HealthPoints = new NetworkVariable<int>();

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        HealthPoints.Value = 100;

    }



    /*Den her skal ind et eller andet sted så jeg ligger den her pt
        private void OnTriggerEnter(Collider collider)
    {
    if(!IsServer) return;
        if (collider.GetComponent<idk vores spillers hænder: shrugging:> ()){
            Midst HP
        } 
    }
    */
}
