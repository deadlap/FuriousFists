using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandActions : MonoBehaviour {
    [SerializeField] private GameObject LeftHand;
    [SerializeField] private GameObject RightHand;

    private Vector3 PreviousLeftHandPosition;
    private Vector3 PreviousRightHandPosition;
    [SerializeField] private InputActionProperty LeftControllerTrigger;
    [SerializeField] private InputActionProperty RightControllerTrigger;
    [SerializeField] float SwingThreshold;
    [SerializeField] float DistanceThreshold;
    void Start() { 
    // PreviousLeftHandPosition = LeftHand.transform.position; //set previous positions
    // PreviousRightHandPosition = RightHand.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
