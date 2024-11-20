using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Character : MonoBehaviour {

    [SerializeField] float MaxHealth;
    [SerializeField] float Health;

    // public HealthBar healthbar;

    [SerializeField] CharacterController PlayerCharacterController;
    [SerializeField] GameObject OwnXROrigin;
    [SerializeField] Vector3 KnockBackVector;
    // [SerializeField]
    [SerializeField] float KnockbackSmoothness;

    public float AttackCooldown; //per hand.
    public float MaxKnockback;
    public float MaxSpeed;
    public float MaxDamage;
    public float DamageToKnockbackRatio;
    
    void Start() {
        Health = MaxHealth;
        // healthbar.SetHealth(MaxHealth);
        if (OwnXROrigin == null) {
            OwnXROrigin = GameObject.FindWithTag("Player");
            PlayerCharacterController = OwnXROrigin.GetComponent<CharacterController>();
        } else {
            PlayerCharacterController = GetComponent<CharacterController>();
        }
        KnockBackVector = Vector3.zero;
    }

    void Update() {
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        KnockBackVector = Vector3.Lerp(KnockBackVector, Vector3.zero, Time.deltaTime*KnockbackSmoothness);
        PlayerCharacterController.Move(Time.deltaTime*KnockBackVector);

        // if (Input.GetKeyDown(KeyCode.Space)) {
        //     Debug.Log("heff");
        //     ApplyDamage(20); }
    }

    public void ApplyHit(Vector3 knockback, float damage) {
        ApplyDamage(damage);
        ApplyKnockBack(knockback*DamageToKnockbackRatio*damage);
    }
    public void ApplyKnockBack(Vector3 knockback) {
        KnockBackVector += knockback;
    }
    public void ApplyDamage(float damage){
        Health -= damage;
        // healthbar.SetHealth(Health);
        // Debug.Log("Health: " + health + " Armor: " + armor);
    }
}
