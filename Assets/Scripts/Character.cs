using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;
public class Character : MonoBehaviour {

    [SerializeField] float MaxHealth;
    [SerializeField] float Health;
    [SerializeField] CharacterController PlayerCharacterController;
    // [SerializeField] Rigidbody PlayerRigidbody;
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
            // PlayerCharacterController = OwnXROrigin.GetComponent<CharacterController>();
            // PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
        }
        //  else {
            // PlayerRigidbody = GetComponent<Rigidbody>();
        // }
        PlayerCharacterController = OwnXROrigin.GetComponent<CharacterController>();
        KnockBackVector = Vector3.zero;
    }

    void Update() {
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
        KnockBackVector = Vector3.Lerp(KnockBackVector, Vector3.zero, Time.deltaTime*KnockbackSmoothness);
        KnockBackVector = new Vector3(KnockBackVector.x, 0, KnockBackVector.z);
        // PlayerRigidbody.AddForce(KnockBackVector);
    }

    public void ApplyHit(Vector3 knockback, float damage) {
        ApplyDamage(damage);
        ApplyKnockBack(knockback*DamageToKnockbackRatio*damage);
    }

    public void ApplyKnockBack(Vector3 knockback) {
        KnockBackVector += knockback;
        // PlayerRigidbody.AddForce(knockback);
        
    }
    public void ApplyDamage(float damage){
        Health -= damage;
    }
  
}
