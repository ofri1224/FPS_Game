using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField]
    private int baseValue;
    private List<int> modifiers = new List<int>();
    public int GetValue(){
        return baseValue;
    }

    public void AddModifier(int Modifier)
    {
        if (Modifier!=0)
        {
            modifiers.Add(Modifier);
        }
    }
    public void RemoveModifier(int Modifier)
    {
        modifiers.Remove(Modifier);
    }

}
