using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Hand : MonoBehaviour {
    public List<Vector3> PositionList;
    public int ListLength;
    public float MaxKnockback;
    public float MaxSpeed;
    void Start() {
        PositionList = new List<Vector3>();
    }

    void Update() {
        PositionList.Add(transform.position);
        if (PositionList.Count > ListLength){
            PositionList = PositionList.GetRange(1, PositionList.Count-1);
            Debug.Log(CalculateAverage(PositionList)/(PositionList.Count-1));
            Debug.Log(PositionList);
        }
    }
    
    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Target")) {
            
        }
    }

    float CalculateAverage(List<Vector3> list){
        float sum = 0;
        for (int i = 0; i == list.Count-2; i++) {
            sum = Vector3.Distance(list[i], list[i+1]);
        }
        return sum/(float)(list.Count-1);
    }
}
