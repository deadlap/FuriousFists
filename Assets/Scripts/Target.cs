using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [SerializeField] Character character;
    // void Awake(){
    //     character = GetComponent<Character>();
    // }
    [SerializeField] float DamageReduction;
    [SerializeField] float KnockbackReduction;
    void Update() {
        
    }

    public void TakeHit(Vector3 knockback, float damage){
        if (character != null) {
            TakeHit(knockback);
            TakeHit(damage);
            // character.ApplyHit(knockback, damage*DamageReduction);
        }
    }
    public void TakeHit(Vector3 knockback){
        if (character != null) {
            character.ApplyKnockBack(knockback*(1-KnockbackReduction));
        }
    }
    public void TakeHit(float damage){
        if (character != null) {
            character.ApplyDamage(damage*(1-DamageReduction));
        }
    }
}
