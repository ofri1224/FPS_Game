using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class field_of_view : MonoBehaviour
{
    public float fov=90f;
    public int rayCount=8;
    public float viewDistance=50f;
    public float heightMult;
    private float angle;
    private float angleIncrease;
    void Update()
    {

        
    }

    void FindVisibleTargets(){
        Collider[] TargetsInViewRadius = Physics.OverlapSphere(transform.position,viewDistance,universal_vars.instance.EntityLayer);
        foreach (Collider target in TargetsInViewRadius)
        {
            
        }
    }



}
