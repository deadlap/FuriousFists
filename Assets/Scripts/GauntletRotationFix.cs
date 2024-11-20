using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauntletRotationFix : MonoBehaviour {
    [SerializeField] GameObject HandObject;

    void Update() {
        // Quaternion originalRot = transform.rotation;    
        // transform.rotation = originalRot * Quaternion.AngleAxis(, Vector3.Up);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(HandObject.transform.rotation.eulerAngles.x, eulerRotation.y, eulerRotation.z);
        
    }
}
