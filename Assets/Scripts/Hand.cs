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
    [SerializeField]  float MaxKnockback;
    [SerializeField]  float MaxSpeed;
    [SerializeField]  float MaxDamage;
    // float DamageToKnockbackRatio;
    [SerializeField] float AttackCooldown;
    [SerializeField] float CurrentCooldown;
    UnityEngine.XR.HapticCapabilities capabilitiesL;

    void Start() {
        PositionList = new List<Vector3>();
        MaxKnockback = character.MaxKnockback;
        MaxSpeed = character.MaxSpeed;
        MaxDamage = character.MaxDamage;
        AttackCooldown = character.AttackCooldown;
        // DamageToKnockbackRatio = character.DamageToKnockbackRatio;
    }

    void Update() {
        PositionList.Add(transform.position);
        if (CurrentCooldown > 0) {
            CurrentCooldown -= Time.deltaTime;
        }
        if (PositionList.Count > ListLength){
            PositionList = PositionList.GetRange(1, PositionList.Count-1);
            // Debug.Log(CalculateAverage(PositionList)/(PositionList.Count-1));
            // Debug.Log(PositionList);
        }
    }
    
    void OnTriggerEnter(Collider other) {
        if (CurrentCooldown > 0){
            Debug.Log("Cooldown");
            return;
        }
        if (other.CompareTag("Target")) {
            Debug.Log("hit");
            Vector3 hitVector = transform.position - PositionList[PositionList.Count-1];
            other.gameObject.GetComponent<Target>().TakeHit(hitVector.normalized, MaxDamage);
            startVibra();
        }
    }

    float CalculateAverage(List<Vector3> list){
        float sum = 0;
        for (int i = 0; i == list.Count-2; i++) {
            sum = Vector3.Distance(list[i], list[i+1]);
        }
        return sum/(float)(list.Count-1);
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
