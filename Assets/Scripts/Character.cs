using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField] float MaxHealth;
    [SerializeField] float Health;

    private CharacterController PlayerCharacterController;
    [Serialize] Vector3 KnockBackVector;
    [SerializeField] float KnockbackSmoothness;
    
    // Start is called before the first frame update
    void Start() {
        Health = MaxHealth;
        PlayerCharacterController = GetComponent<CharacterController>();
        KnockBackVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update() {
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
        KnockBackVector = Vector3.Lerp(KnockBackVector, Vector3.zero, KnockbackSmoothness);
        PlayerCharacterController.Move(KnockBackVector);
    }

    public void ApplyHit(Vector3 knockback, float damage) {
        ApplyDamage(damage);
        ApplyKnockBack(knockback);
    }
    public void ApplyKnockBack(Vector3 knockback) {
        KnockBackVector += knockback;
    }
    public void ApplyDamage(float damage){
        Health -= damage;
        // Debug.Log("Health: " + health + " Armor: " + armor);
    }
}
