using UnityEngine;
using System;

[Serializable]
public class Ammo
{
    [SerializeField]
    public int Max_ammo;
    [SerializeField]
    public int Current_ammo;
    public static Ammo empty
    {
        get
        {
            return new Ammo(0);
        }
        
    }
    public Ammo(int Max_ammo){
        this.Max_ammo=Max_ammo;
        this.Current_ammo=Max_ammo;
    }
    public Ammo(int Max_ammo,int Current_ammo){
        this.Max_ammo=Max_ammo;
        this.Current_ammo=Current_ammo;
    }
    public Ammo(Ammo ammo){
        this.Max_ammo=ammo.getMaxAmmo();
        this.Current_ammo=ammo.getAmmo();
    }

    public void setAmmo(int Ammo){
        Current_ammo=Mathf.Clamp(Ammo,0,Max_ammo);
    }
    public void addAmmo(int Ammo){
        if (Current_ammo+Ammo>Max_ammo)
        {
            Current_ammo=Max_ammo;
            return;
        }
        if (Current_ammo+Ammo<0)
        {
            Current_ammo=0;
            return;
        }
        Current_ammo+=Ammo;
    }
    public int getAmmo(){
        return Current_ammo;
    }
    public void setMaxAmmo(int N_MaxAmmo){
        Max_ammo=N_MaxAmmo;
    }
    public int getMaxAmmo(){
        return Max_ammo;
    }
}
