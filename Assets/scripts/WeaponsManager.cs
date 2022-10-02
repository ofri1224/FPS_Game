using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public Gun_base primaryWeapon;
    public Gun_base secondaryWeapon;

    [HideInInspector]
    public Gun_base selectedWeapon;

    public void Start() {
        if (primaryWeapon!=null)
        {
           selectedWeapon = primaryWeapon; 
        }
        else{
            selectedWeapon = secondaryWeapon;
        }
    }
        void Update()
    {
        //Select primary weapon when pressing 1
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            primaryWeapon.ActivateWeapon(true);
            secondaryWeapon.ActivateWeapon(false);
            selectedWeapon = primaryWeapon;
        }

        //Select secondary weapon when pressing 2
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            primaryWeapon.ActivateWeapon(false);
            secondaryWeapon.ActivateWeapon(true);
            selectedWeapon = secondaryWeapon;
        }
    }
}
