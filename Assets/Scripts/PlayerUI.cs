using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] RectTransform ArmHealth;
    [SerializeField] RectTransform AboveHeadHealth;
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
        ArmHealth.transform.localScale = new Vector3(newValue/100f, 1, 1);
        AboveHeadHealth.transform.localScale = new Vector3(newValue / 100f, 1, 1);
    }
}
