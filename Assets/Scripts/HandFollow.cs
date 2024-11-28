using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandFollow : MonoBehaviour {
    // Start is called before the first frame update
    [SerializeField] GameObject HandToFollow;
    
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = HandToFollow.transform.position;
    }
}
