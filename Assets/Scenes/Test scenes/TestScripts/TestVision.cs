using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class TestVision : MonoBehaviour
{
    [Range(0,360)]
    public float fov;
    public float LookRadius;
    
    void Update()
    {
        Collider[] TargetsInViewRadius = Physics.OverlapSphere(transform.position,LookRadius);
        if (TargetsInViewRadius.Length>0)
        {
            foreach (Collider target in TargetsInViewRadius)
            {
                if (target.gameObject!=gameObject)
                {
                    Vector3 Direction = -(transform.position-target.transform.position);
                    Debug.DrawRay(transform.position,Direction*LookRadius,Color.green,0.01f);
                    float angle = Vector3.Angle(Direction,transform.forward);
                    //angle+=transform.eulerAngles.y;
                    Debug.Log(target.transform.name+':'+angle);
                    if (angle<=fov/2&&angle>=(-fov/2))
                    {
                        Debug.DrawRay(transform.position,Direction,Color.blue,0.01f);
                        if (target.GetComponent<Renderer>()!=null)
                        {
                        target.gameObject.GetComponent<Renderer>().material.color=Color.green;    
                        }
                    }
                    else{
                        if (target.GetComponent<Renderer>()!=null)
                        {
                        target.gameObject.GetComponent<Renderer>().material.color=Color.white;    
                        }
                    }  
                }
            }
        }
    }
    public Vector3 angle2direction(float angle,bool isGlobal){
        if (!isGlobal)
        {
            angle+=transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angle*Mathf.Deg2Rad),0,Mathf.Cos(angle*Mathf.Deg2Rad));
    }
}
[CustomEditor(typeof(TestVision))]
public class TestVisionEditor : Editor {
    TestVision enemy;
    void OnSceneGUI() {
        enemy = (TestVision)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position,Vector3.up,Vector3.forward,360,enemy.LookRadius);
        Vector3 Angle_A=enemy.angle2direction(-enemy.fov/2,false);
        Vector3 Angle_B=enemy.angle2direction(enemy.fov/2,false);
        Handles.DrawLine(enemy.transform.position,enemy.transform.position+Angle_A*enemy.LookRadius);
        Handles.DrawLine(enemy.transform.position,enemy.transform.position+Angle_B*enemy.LookRadius);
    } 
}
