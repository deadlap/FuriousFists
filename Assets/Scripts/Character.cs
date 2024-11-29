using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using Unity.Netcode;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

public class Character : MonoBehaviour {

    [SerializeField] public float MaxHealth;
    [SerializeField] public float Health;
    [SerializeField] Rigidbody PlayerRigidbody;
    [SerializeField] GameObject OwnXROrigin;
    [SerializeField] GameObject LeftFakeHand;
    [SerializeField] GameObject RightFakeHand;
    [SerializeField] Hand LeftHand;
    [SerializeField] Hand RightHand;
    [SerializeField] AudioSource AudioBlock;
    [SerializeField] AudioSource AudioHit;
    public float MaxSpeed;
    public float MinSpeed;
    public float MaxDamage;

    bool DisableInput;
    
    void Start() {
        Health = MaxHealth;
        if (OwnXROrigin != null) {
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
            return;
        }
        if (GetComponent<NetworkObject>().NetworkObjectId == 1){
            if (GameObject.FindWithTag("Player1") == null)
                return;
            OwnXROrigin = GameObject.FindWithTag("Player1");
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player1"));
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
            // SetGameLayerRecursive(OwnXROrigin, LayerMask.NameToLayer("Player1"));
        } else {
            if (GameObject.FindWithTag("Player2") == null)
                return;
            OwnXROrigin = GameObject.FindWithTag("Player2");
            SetGameLayerRecursive(gameObject, LayerMask.NameToLayer("Player2"));
            PlayerRigidbody = OwnXROrigin.GetComponent<Rigidbody>();
            // SetGameLayerRecursive(OwnXROrigin, LayerMask.NameToLayer("Player2"));
        }
    }

    void Update() {
        if (Health > MaxHealth) {
            Health = MaxHealth;
        }
        if (Health < 0){
            Health = 0;
        }
        // if (DisableInput) {
        //     DisableInput = false;
        //     Invoke("EnableMovement", 1.5f);
        //     OwnXROrigin.GetComponent<DynamicMoveProvider>().enabled = false;
        // }
        // HitVector = (transform.position-PreviousPosition).normalized;
        // PreviousPosition = transform.position;
    }
    void FixedUpdate(){
        if (LeftHand == null)
            return;
        LeftHand.PositionList.Add(LeftFakeHand.transform.localPosition);
        RightHand.PositionList.Add(RightFakeHand.transform.localPosition);
        if (LeftHand.PositionList.Count > LeftHand.ListLength){
            LeftHand.PositionList = LeftHand.PositionList.GetRange(1,LeftHand.ListLength);
            // Debug.Log("Average: " + LeftHand.CalculateAverage(LeftHand.PositionList));
        }
        if (RightHand.PositionList.Count > RightHand.ListLength){
            RightHand.PositionList = RightHand.PositionList.GetRange(1,RightHand.ListLength);
        }

    }
    
    public void ApplyHit(Vector3 knockback, float damage) {
        Debug.Log("hej"+ gameObject.tag);
        DisableInput = true;
        ApplyDamage(damage);
        ApplyPureKnockBack(knockback*damage);
    }

    public void PlaySound(bool blocked){
        if (blocked){
            AudioBlock.Play(0);
        } else {
            AudioHit.Play(0);
        }
    }

    void EnableMovement(){
        OwnXROrigin.GetComponent<DynamicMoveProvider>().enabled = true;
    }

    public void ApplyPureKnockBack(Vector3 knockback) {
        Debug.Log("hit: "+ knockback);
        PlayerRigidbody.AddForce(knockback);
    }

    public void ApplyDamage(float damage){
        // Health -= damage;
    }
    private void SetGameLayerRecursive(GameObject _gameObject, int layer) {
         _gameObject.layer = layer;
        foreach (Transform child in _gameObject.transform) {
                child.gameObject.layer = layer;

                Transform _HasChildren = child.GetComponentInChildren<Transform>();
                if (_HasChildren != null)
                    SetGameLayerRecursive(child.gameObject, layer);
            }
    }
  
}
