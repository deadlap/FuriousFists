using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Unity.Netcode;

public class Character : MonoBehaviour {

    [SerializeField] public float MaxHealth;
    [SerializeField] public float Health;
    [SerializeField] Rigidbody PlayerRigidbody;
    [SerializeField] GameObject OwnXROrigin;
    [SerializeField] GameObject LeftFakeHand;
    [SerializeField] GameObject RightFakeHand;
    [SerializeField] Hand LeftHand;
    [SerializeField] Hand RightHand;
    
    public float AttackCooldown; //per hand.
    public float MaxKnockback;
    public float MaxSpeed;
    public float MinSpeed;
    public float MaxDamage;
    public float DamageToKnockbackRatio;
    
    void Start() {
        Health = MaxHealth;
        if (OwnXROrigin != null) {
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
            return;
        }
        if (GetComponent<NetworkObject>().NetworkObjectId == 1){
            OwnXROrigin = GameObject.FindWithTag("Player1");
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player1"));
            // SetGameLayerRecursive(OwnXROrigin, LayerMask.NameToLayer("Player1"));
        } 
        if (GetComponent<NetworkObject>().NetworkObjectId == 2){
            OwnXROrigin = GameObject.FindWithTag("Player2");
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player2"));
            // SetGameLayerRecursive(OwnXROrigin, LayerMask.NameToLayer("Player2"));
        }
        PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
    }

    void Update() {
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
        if (Health < 0){
            Health = 0;
        }
        // HitVector = (transform.position-PreviousPosition).normalized;
        // PreviousPosition = transform.position;
    }
    void FixedUpdate(){
        if (LeftHand == null)
            return;
        LeftHand.PositionList.Add(LeftFakeHand.transform.localPosition);
        RightHand.PositionList.Add(RightFakeHand.transform.localPosition);
        if (LeftHand.PositionList.Count > LeftHand.ListLength){
            LeftHand.PositionList = LeftHand.PositionList.GetRange(1,LeftHand.ListLength);
            // Debug.Log("Average: " + LeftHand.CalculateAverage(LeftHand.PositionList));
        }
        if (RightHand.PositionList.Count > RightHand.ListLength){
            RightHand.PositionList = RightHand.PositionList.GetRange(1,RightHand.ListLength);
        }

    }
    public void ApplyHit(Vector3 knockback, float damage) {
        ApplyDamage(damage);
        ApplyPureKnockBack(knockback*DamageToKnockbackRatio*damage);
    }

    public void ApplyPureKnockBack(Vector3 knockback) {
        Debug.Log("hit: "+ knockback);
        PlayerRigidbody.AddForce(knockback);
    }

    public void ApplyDamage(float damage){
        Health -= damage;
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
