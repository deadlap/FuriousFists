using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    [SerializeField] Character character;
    // void Awake(){
    //     character = GetComponent<Character>();
    // }

    void Update() {
        
    }

    public void TakeHit(Vector3 knockback, float damage){
        if (character != null) {
            character.ApplyHit(knockback, damage);
        }
    }
    public void TakeHit(Vector3 knockback){
        if (character != null) {
            character.ApplyKnockBack(knockback);
        }
    }
    public void TakeHit(float damage){
        if (character != null) {
            character.ApplyDamage(damage);
        }
    }
}
