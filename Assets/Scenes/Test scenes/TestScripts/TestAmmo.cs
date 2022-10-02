// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEditor;
// [Serializable]
// public class TestAmmo : MonoBehaviour
// {
//     public List<Ammo> ammos;
//     public Dictionary<universal_vars.AmmoType, Ammo> ammo=new Dictionary<universal_vars.AmmoType, Ammo>();
//     public void Start(){
//         int index=0;
//         foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType))){
//             ammo.Add(type,ammos[index]);
//             index++;
//         }
//         foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType))){
//             Debug.Log(ammo[type].getAmmo());
//             Debug.Log(ammo[type].getMaxAmmo());
//         }
//     }

// }

// [CustomEditor(typeof(TestAmmo))]
// public class TestAmmoEditor : Editor {
//     public override void OnInspectorGUI() {
//         TestAmmo T = (TestAmmo)target;
//         // base.OnInspectorGUI();
//         if(T.ammos==null){
//             Debug.Log("new");
//             T.ammos=new List<Ammo>();
//         }
//         EditorGUILayout.BeginHorizontal();
//         EditorGUILayout.LabelField("ammo type");
//         EditorGUILayout.LabelField("current amount");
//         EditorGUILayout.LabelField("max amount");
//         EditorGUILayout.EndHorizontal();
//         int index = 0;
//         foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType)))
//         {
//             if (T.ammos.Count-1<index)
//             {
//                 Debug.Log("adding");
//                 T.ammos.Add(Ammo.empty);
//             }
//             EditorGUILayout.BeginHorizontal();
//             T.ammos[index].setAmmo(EditorGUILayout.IntField(T.ammos[index].getAmmo()));
//             T.ammos[index].setMaxAmmo(EditorGUILayout.IntField(T.ammos[index].getMaxAmmo()));
//             EditorGUILayout.EndHorizontal();
//             index++;
//         }
//     }
// }


// [CustomPropertyDrawer(typeof(TestAmmo))]
// public class TestAmmoDrawer: PropertyDrawer {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
//         EditorGUI.BeginProperty(position,label,property);
//         position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
//         var TestRect = new Rect(position.x, position.y, 30, position.height);
//         EditorGUI.PropertyField(TestRect,property.FindPropertyRelative("ammo"),GUIContent.none);
//         EditorGUI.EndProperty();
//     }
// }
