using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;
public class Hand_Animation_Handler : MonoBehaviour {
    public InputActionProperty ClenchInput;

    public Animator HandAnimator;

    void Update() {
        float clenchValue = ClenchInput.action.ReadValue<float>();
        HandAnimator.SetFloat("Clench", clenchValue);
    }

}