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
        transform.eulerAngles = new UnityEngine.Vector3(0, 0, 0);
        Collider.height = Mathf.Clamp(Camera.main.transform.localPosition.y, MinHeight, MaxHeight);
        Collider.center = new UnityEngine.Vector3(Camera.main.transform.localPosition.x, Collider.height / 2, Camera.main.transform.localPosition.z);
        // transform.position = new UnityEngine.Vector3(
        //     transform.parent.transform.position.x-(Camera.main.transform.position.x) 
        //     0, 
        //     transform.parent.transform.position.z-(Camera.main.transform.position.z)
        //     );
        // transform.localPosition = new UnityEngine.Vector3(0, (ObjectToFollow.transform.position).y, 0);
        // gameObject.transform.rotation = -1*gameObject.transform.parent.transform.rotation;// new UnityEngine.Quaternion(0, 0, 0, 0);
    }
}
