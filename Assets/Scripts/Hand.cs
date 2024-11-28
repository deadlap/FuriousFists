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
    float AttackCooldown;
    float CurrentCooldown;
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
        AttackCooldown = character.AttackCooldown;
        HitVector = Vector3.zero;
        PreviousPosition = Vector3.zero;
    }

    void Update() {
        // if (CurrentCooldown > 0) {
        //     CurrentCooldown -= Time.deltaTime;
        // } else if (CurrentCooldown < 0) {
        //     CurrentCooldown = 0;
        // }

        HitVector = (transform.position-PreviousPosition).normalized;
        PreviousPosition = transform.position;

    }

    void FixedUpdate(){
        // if (PositionList.Count == 0)
        //     return;
        // if (PositionList.Count == ListLength){
        //     Debug.Log(gameObject.name);
        //     Debug.Log("Distance: " + Vector3.Distance(PositionList[PositionList.Count-2],PositionList[PositionList.Count-1]));
        //     Debug.Log("Average: " + CalculateAverage(PositionList));
        // }

    }
    
    void OnTriggerEnter(Collider other) {
        Debug.Log("hit:" + other.name);
        // if (CurrentCooldown > 0){
        //     return;
        // }
        if (other.CompareTag("Target")) {
            CurrentCooldown = AttackCooldown;
            float speed = Vector3.Distance(PositionList[^2],PositionList[^1]);
            // CalculateAverage(PositionList);
            if (speed >= MinSpeed) {
                Debug.Log("Hit: " + HitVector);
                speed = Mathf.Clamp(speed, MinSpeed, MaxSpeed);
                float damage = speed/MaxSpeed*MaxDamage;
                other.gameObject.GetComponent<Target>().TakeHit(HitVector, damage);
                startVibra();
            }
            if (other.gameObject.GetComponent<Target>().DamageReduction > 0.25){
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
    public void startVibra()
    {
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetHapticCapabilities(out capabilitiesL);
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetHapticCapabilities(out capabilitiesL);
        if (capabilitiesL.supportsImpulse)
        {
            uint channel = 0;
            float amplitude = 0.5f;
            float duration = 0.4f;
            InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(channel, amplitude, duration);
            InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(channel, amplitude, duration);
        }
    }
}
