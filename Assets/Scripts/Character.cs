using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Unity.Netcode;

public class Character : MonoBehaviour {

    [SerializeField] float MaxHealth;
    [SerializeField] float Health;
    // [SerializeField] CharacterController PlayerCharacterController;
    [SerializeField] Rigidbody PlayerRigidbody;
    [SerializeField] GameObject OwnXROrigin;
    [SerializeField] Vector3 KnockBackVector;
    [SerializeField] float KnockbackSmoothness;
    
    public float AttackCooldown; //per hand.
    public float MaxKnockback;
    public float MaxSpeed;
    public float MaxDamage;
    public float DamageToKnockbackRatio;
    
    void Start() {
        Health = MaxHealth;
        if (OwnXROrigin == null) {
            OwnXROrigin = GameObject.FindWithTag("Player");
            if (GetComponent<NetworkObject>().NetworkObjectId == 1){
                SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player1"));
            } if (GetComponent<NetworkObject>().NetworkObjectId == 2){
                SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player2"));
            }
        }
        PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
        KnockBackVector = Vector3.zero;
    }

    void Update() {
        if (Health > MaxHealth) {
            Health = MaxHealth;
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
