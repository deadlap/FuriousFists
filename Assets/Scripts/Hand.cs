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
    float BlockThreshold;
    Vector3 PreviousPosition; //A list of positions the hand has had within the last x frames. Relative to its parent.
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
        BlockThreshold = 0.2f;
    }

    void Update() {
        HitVector = (transform.position-PreviousPosition).normalized;
        PreviousPosition = transform.position;
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Target")) {
            //check the distance between the current and previous position of the hand
            // Not technically speed, since time has not been included.
            float speed = Vector3.Distance(PositionList[^2],PositionList[^1]); 
            
            // Is the fist moving fast enough to be a punch?
            if (speed >= MinSpeed) {
                speed = Mathf.Clamp(speed, MinSpeed, MaxSpeed);
                float damage = speed/MaxSpeed*MaxDamage;
                other.gameObject.GetComponent<Target>().TakeHit(HitVector, damage);
                // Does the attack count as a "hit" or a "block"?
                if (other.gameObject.GetComponent<Target>().DamageReduction > BlockThreshold){
                    Instantiate(BlockEffect, transform);
                } else {
                    Instantiate(HitEffect, transform);
                }
                //Which hand are we punching with?
                if (transform.name == "RightHand"){
                    character.ApplyRumbleRight = true;
                } else if (transform.name == "LeftHand"){
                    character.ApplyRumbleLeft = true;
                }
            }
        }
    }
}
