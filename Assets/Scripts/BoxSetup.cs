using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BoxSetup : MonoBehaviour {
    [SerializeField] GameObject ObjectToFollow;
    [SerializeField] CapsuleCollider Collider;
    [SerializeField] float MinHeight;
    [SerializeField] float MaxHeight;
    void Update() {
        Collider.height = Mathf.Clamp(Camera.main.transform.localPosition.y, MinHeight, MaxHeight);
        Collider.center = new UnityEngine.Vector3(Camera.main.transform.localPosition.x, Collider.height / 2, Camera.main.transform.localPosition.z);
    }
}
