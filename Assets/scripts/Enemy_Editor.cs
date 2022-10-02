using UnityEngine;
using System.Collections;
using UnityEditor;
[CustomEditor(typeof(enemy_script))]
public class Enemy_Editor : Editor
//: CharacterStatsEditor 
{
    enemy_script enemy;
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        enemy_script T = (enemy_script)target;
        
    }
    void OnSceneGUI() {
        enemy = (enemy_script)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(enemy.transform.position,Vector3.up,Vector3.forward,360,enemy.LookRadius);
        Vector3 Angle_A=enemy.angle2direction(-enemy.fov/2,false);
        Vector3 Angle_B=enemy.angle2direction(enemy.fov/2,false);
        Handles.DrawLine(enemy.transform.position,enemy.transform.position+Angle_A*enemy.LookRadius);
        Handles.DrawLine(enemy.transform.position,enemy.transform.position+Angle_B*enemy.LookRadius);
        if (enemy.LastSeenLocation!=null)
        {
            Handles.color = Color.gray;
            Handles.DrawSolidDisc(enemy.LastSeenLocation,Vector3.up,0.1f);
        }
        if (enemy.RoamPoints.Length>0)
        {
            // Handles.color = Color.black;
            // Handles.DrawDottedLine(enemy.transform.position,enemy.RoamPoints[0],10f);
            for (int i = 0; i < enemy.RoamPoints.Length; i++)
            {
            enemy.RoamPoints[i] = Handles.DoPositionHandle(enemy.RoamPoints[i],Quaternion.identity);
            }
        }
    } 
}