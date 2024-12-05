using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class Hand : MonoBehaviour {
    public List<Vector3> PositionList;
    [SerializeField] Character character;
    public int ListLength;
    float MaxSpeed;
    float MinSpeed;
    float MaxDamage;
    UnityEngine.XR.HapticCapabilities capabilitiesL;
    Vector3 PreviousPosition;
    Vector3 HitVector;
    [SerializeField] GameObject HitEffect;
    [SerializeField] GameObject BlockEffect;
    void Start() {
        PositionList = new List<Vector3>();
        MaxSpeed = character.MaxSpeed;
        MinSpeed = character.MinSpeed;
        MaxDamage = character.MaxDamage;
        HitVector = Vector3.zero;
        PreviousPosition = Vector3.zero;
    }

    void Update() {
        HitVector = (transform.position-PreviousPosition).normalized;
        PreviousPosition = transform.position;

    }
    
    void OnTriggerEnter(Collider other) {
        Debug.Log("hit:" + other.name);
        if (other.CompareTag("Target")) {
             float speed = Vector3.Distance(PositionList[^2],PositionList[^1]);
            // CalculateAverage(PositionList);
            if (speed >= MinSpeed) {
                Debug.Log("Hit: " + HitVector);
                speed = Mathf.Clamp(speed, MinSpeed, MaxSpeed);
                float damage = speed/MaxSpeed*MaxDamage;
                other.gameObject.GetComponent<Target>().TakeHit(HitVector, damage);
            }
            if (other.gameObject.GetComponent<Target>().DamageReduction > 0.2){
                Instantiate(BlockEffect, transform);
            } else {
                Instantiate(HitEffect, transform);
            }
        }
    }

    float CalculateAverage(List<Vector3> list){
        float sum = 0;
        for (int i = 1; i < list.Count; i++) {
            sum += Vector3.Distance(list[i-1],list[i]);
        }
        return (sum/(float)(list.Count-1));
    }
}
