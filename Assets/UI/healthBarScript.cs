using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class healthBarScript : MonoBehaviour
{
    public Slider slider;
    public Text CurrentHP;
    public Text MaxHP;
    public void SetMaxHealth(int health){
        slider.maxValue = health;
        MaxHP.text = "/"+health.ToString();
    }
    public void SetHealth(int health){
        slider.value = health;
        CurrentHP.text = health.ToString();
    }
}
