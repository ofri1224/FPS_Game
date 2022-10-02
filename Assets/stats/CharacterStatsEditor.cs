// using UnityEngine;
// using UnityEditor;
// using System.Collections.Generic;
// [CustomEditor(typeof(CharacterStats),true)]
// [DisallowMultipleComponent()]
// public class CharacterStatsEditor : Editor {
//     bool isOpen = false;
//     CharacterStats T;
//     public void Awake()    
//     {
//         T = (CharacterStats)target;
//         Debug.Log(T.transform.name);
//     }
//     public override void OnInspectorGUI() {
//         T = (CharacterStats)target;
        
//         base.OnInspectorGUI();
//         EditorGUILayout.BeginVertical();
//         if(T.ammos==null){
//             T.ammos=new List<Ammo>();
//         }
//         while(T.ammos.Count<System.Enum.GetValues(typeof(universal_vars.AmmoType)).Length){
//             T.ammos.Add(Ammo.empty);    
//         }
//         isOpen = EditorGUILayout.Foldout(isOpen,"AmmoType Carry Capacity");
//         if(isOpen){
//             EditorGUIUtility.fieldWidth=1;
//             EditorGUIUtility.labelWidth=0;
//             EditorGUILayout.BeginHorizontal();
//             EditorGUILayout.LabelField("ammo type");
//             EditorGUILayout.LabelField("current amount");
//             EditorGUILayout.LabelField("max amount");
//             EditorGUILayout.EndHorizontal();
            
//             int index = 0;
//             foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType)))
//             {
//                 EditorGUILayout.BeginHorizontal();
//                 EditorGUILayout.LabelField(type.ToString());
//                 EditorGUI.BeginChangeCheck();
//                 T.ammos[index].setAmmo(EditorGUILayout.IntField(T.ammos[index].getAmmo()));
//                 T.ammos[index].setMaxAmmo(EditorGUILayout.IntField(T.ammos[index].getMaxAmmo()));
//                 EditorGUI.EndChangeCheck();
//                 EditorGUILayout.EndHorizontal();
//                 index++;
//             }
//             EditorGUILayout.BeginHorizontal();
//             if (GUILayout.Button("auto fill ammo"))
//             {
//                 index=0;
//                 foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType)))
//                 {
//                     T.ammos[index].setAmmo(T.ammos[index].getMaxAmmo());
//                     index++;
//                 }
//             }
//             if (GUILayout.Button("auto empty ammo"))
//             {
//                 index=0;
//                 foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType)))
//                 {
//                     T.ammos[index].setAmmo(0);
//                     index++;
//                 }
//             }
//             EditorGUILayout.EndHorizontal();
//         }        
//         EditorGUILayout.EndVertical();
//     }
// }
