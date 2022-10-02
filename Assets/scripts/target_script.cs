using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_script : MonoBehaviour
{
    public float health = 50f;

    public void take_damage(float damage){
        health-=damage;
        if (health<=0)
        {
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
    }

}
