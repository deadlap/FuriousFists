using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Target : MonoBehaviour {
    [SerializeField] Character character;
    // void Awake(){
    //     character = GetComponent<Character>();
    // }
    [SerializeField] public float DamageReduction;
    [SerializeField] public float KnockbackReduction;
    void Update() {
        
    }

    public void TakeHit(Vector3 knockback, float damage){
        if (character != null) {
            character.ApplyHit(knockback*(1-KnockbackReduction), damage*(1-DamageReduction));
        }
    }
    public void TakeHit(Vector3 knockback){
        if (character != null) {
            character.ApplyPureKnockBack(knockback*(1-KnockbackReduction));
        }
    }
    public void TakeHit(float damage){
        if (character != null) {
            character.ApplyDamage(damage*(1-DamageReduction));
        }
    }
}
