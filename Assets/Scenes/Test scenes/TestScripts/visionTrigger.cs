using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visionTrigger : MonoBehaviour
{
    public TestAi testAi;
       
    void OnTriggerEnter2D(Collider2D o)
    {
        
        if (o.gameObject.tag == "Player")
        {
            testAi.inView = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D o)
    {
 
 
        if (o.gameObject.tag == "Player")
        {
            testAi.inView = false;
        }
    }
}
