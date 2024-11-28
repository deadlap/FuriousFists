using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GlobalEffectsSpawner : MonoBehaviour {
    public static GlobalEffectsSpawner INSTANCE;
    [SerializeField] GameObject HitEffect;
    [SerializeField] GameObject BlockEffect;
    void Start(){
        INSTANCE = this;
    }
    // public void SpawnBlockEffect(Vector3 position){
    //     Instantiate(BlockEffect, position, Quaternion.identity);
    // }
    // public void SpawnHitEffect(Vector3 position){
    //     Instantiate(HitEffect, position, Quaternion.identity);
    // }
}
