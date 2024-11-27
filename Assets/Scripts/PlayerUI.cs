using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] RectTransform HealthUI;

    private void OnEnable()
    {
        GetComponent<NetworkHealth>().HealthPoints.OnValueChanged += HealthChanged;
    }



    private void OnDisable()
    {
        GetComponent<NetworkHealth>().HealthPoints.OnValueChanged -= HealthChanged;

    }
    private void HealthChanged(int previousValue, int newValue)
    {
        HealthUI.transform.localScale = new Vector3(newValue/100f, 1, 1);
    }
}
