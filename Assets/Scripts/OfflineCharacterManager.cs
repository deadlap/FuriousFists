using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class OfflineCharacterManager : MonoBehaviour {
    UnityEngine.XR.HapticCapabilities capabilitiesL;
    [SerializeField] public Character PlayerOnlineCharacter;
    [SerializeField] Rigidbody OfflineRigidbody;
    // [SerializeField] GameObject AudioBlockPrefab;
    // [SerializeField] GameObject AudioHitPrefab;
    public bool IsOnline;
    void Start(){
        IsOnline = false;
    }
    void Update() {
        if (!IsOnline)
            return;
        if (PlayerOnlineCharacter.ApplyRumbleLeft) {
            StartVibration(false);
            PlayerOnlineCharacter.ApplyRumbleLeft = false;
        }
        if (PlayerOnlineCharacter.ApplyRumbleRight) {
            StartVibration(true);
            PlayerOnlineCharacter.ApplyRumbleRight = false;
        }
        OfflineRigidbody.AddForce(PlayerOnlineCharacter.KnockbackVector);
        PlayerOnlineCharacter.KnockbackVector = Vector3.zero;
    }
    public void GoOnline(){
        IsOnline = true;
    }
    public void StartVibration(bool right) {
        InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetHapticCapabilities(out capabilitiesL);
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetHapticCapabilities(out capabilitiesL);
        if (capabilitiesL.supportsImpulse) {
            uint channel = 0;
            float amplitude = 0.5f;
            float duration = 0.4f;
            if (right) {
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).SendHapticImpulse(channel, amplitude, duration);
            } else {
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }
}
