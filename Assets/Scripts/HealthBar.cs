using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    public Gradient gradient;
    public Image fill;
    [SerializeField] Character character;
    
    void Start(){
        SetMaxHealth(character.MaxHealth);
        SetHealth(character.Health);
    }
    void Update(){
        SetHealth(character.Health);
    }
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        fill.color = gradient.Evaluate(1f) ;
    }

    public void SetHealth(float health)
    {
        healthSlider.value = health;

        fill.color = gradient.Evaluate(healthSlider.normalizedValue);
    }


}
