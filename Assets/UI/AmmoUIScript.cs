using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoUIScript : MonoBehaviour
{
    public Text CountUI;
    private int Count;

    public void SetCount(int Count){
        this.Count=Count;
        this.CountUI.text=Count.ToString();
    }
    public int GetCount(){
        return Count;
    }

}
