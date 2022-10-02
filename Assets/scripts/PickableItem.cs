using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class PickableItem : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    //public float pickUpRange;

    // public void PickUp(){
        
    // }
    // public void Drop(){
    //     transform.SetParent(null);
    //     rb.isKinematic=false;
    //     rb.AddForce(((Vector3.up*upwardDropForce)+(Vector3.forward*forwardDropForce)),ForceMode.Impulse);
    // }
}
