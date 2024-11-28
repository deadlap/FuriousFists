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
    public void SpawnBlockEffect(Vector3 Position){
        Instantiate(BlockEffect);
    }
    public void SpawnHitEffect(Vector3 Position){
        Instantiate(HitEffect);
    }
}
