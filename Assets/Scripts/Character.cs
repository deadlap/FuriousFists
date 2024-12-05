using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Character : MonoBehaviour {

    [SerializeField] public float MaxHealth;
    [SerializeField] public float Health;
    [SerializeField] Rigidbody PlayerRigidbody;
    [SerializeField] GameObject OwnXROrigin;
    [SerializeField] GameObject LeftFakeHand;
    [SerializeField] GameObject RightFakeHand;
    [SerializeField] Hand LeftHand;
    [SerializeField] Hand RightHand;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip AudioBlock;
    [SerializeField] AudioClip AudioHit;
    public float MaxSpeed;
    public float MinSpeed;
    public float MaxDamage;

    // Settings to be applied to the offline player
    public Vector3 KnockbackVector;
    public Vector3 PlaySoundPosition;
    public bool BlockedSound;
    public bool ApplyRumbleLeft;
    public bool ApplyRumbleRight;
    void Start() {
        KnockbackVector = Vector3.zero;
        ApplyRumbleLeft = false;
        ApplyRumbleRight = false;
        PlaySoundPosition = Vector3.zero;
        BlockedSound = false;

        Health = MaxHealth;
        if (OwnXROrigin != null) {
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
            return;
        }
        if (GetComponent<NetworkObject>().NetworkObjectId == 1){
            if (GameObject.FindWithTag("Player1") == null)
                return;
            OwnXROrigin = GameObject.FindWithTag("Player1");
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player1"));
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
        } else {
            if (GameObject.FindWithTag("Player2") == null)
                return;
            OwnXROrigin = GameObject.FindWithTag("Player2");
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player2"));
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
        }
        OwnXROrigin.gameObject.GetComponent<OfflineCharacterManager>().PlayerOnlineCharacter = this;
    }

    void Update() {
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
        if (Health < 0){
            Health = 0;
        }
    }
    void FixedUpdate(){
        if (LeftHand == null)
            return;
        LeftHand.PositionList.Add(LeftFakeHand.transform.localPosition);
        RightHand.PositionList.Add(RightFakeHand.transform.localPosition);
        if (LeftHand.PositionList.Count > LeftHand.ListLength){
            LeftHand.PositionList = LeftHand.PositionList.GetRange(1,LeftHand.ListLength);
        }
        if (RightHand.PositionList.Count > RightHand.ListLength){
            RightHand.PositionList = RightHand.PositionList.GetRange(1,RightHand.ListLength);
        }

    }
    
    public void ApplyHit(Vector3 knockback, float damage) {
        ApplyDamage(damage);
        ApplyPureKnockBack(knockback*damage);
    }

    public void PlaySound(bool blocked){
        if (blocked){
            audioSource.PlayOneShot(AudioBlock);
        } else {
            audioSource.PlayOneShot(AudioHit);
        }
    }

    public void ApplyPureKnockBack(Vector3 knockback) {
        KnockbackVector = knockback;
    }

    public void ApplyDamage(float damage){
        // Health -= damage;
    }

    private void SetGameLayerRecursive(GameObject _gameObject, int layer) {
         _gameObject.layer = layer;
        foreach (Transform child in _gameObject.transform) {
                child.gameObject.layer = layer;

                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if (_HasChildren != null)
                    SetGameLayerRecursive(child.gameObject, layer);
            }
    }
  
}
