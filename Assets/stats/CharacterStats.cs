using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int Team;
    public Transform GunContainer;
    public Stat MaxHealth;
    public int CurrentHealth {get; private set;}
    [HideInInspector]
    // public List<Ammo> ammos;
    // [HideInInspector]
    // public Dictionary<universal_vars.AmmoType, Ammo> ammo=new Dictionary<universal_vars.AmmoType, Ammo>();
    public float pickUpRange;

    [Header("Movement speeds")]
    public Stat walkingSpeed;//7.5f
    public Stat runningSpeed;//11.5f
    public Stat crouchWalkingSpeed;// 5.5f
    public Stat jumpSpeed; //8.0f
    [Header("Weapons")]
    public Gun_base primaryWeapon;
    public Gun_base secondaryWeapon;

    [HideInInspector]
    public Gun_base selectedWeapon;
    private void Awake() {
        // int index=0;
        // foreach (universal_vars.AmmoType type in System.Enum.GetValues(typeof(universal_vars.AmmoType))){
        //     print(ammos[index].getAmmo());
        //     print(ammos[index].getMaxAmmo());
        //     ammo.Add(type,this.ammos[index]);
        //     index++;
        // }
        CurrentHealth=((int)MaxHealth.GetValue());
        if (primaryWeapon!=null)
        {
           selectedWeapon = primaryWeapon; 
        }
        else{
            selectedWeapon = secondaryWeapon;
        }
    }

    public void Shoot(){
        //ammo[selectedWeapon.gunAmmo].addAmmo(-1);
        selectedWeapon.shoot();
    }
    public void Reload(){
        selectedWeapon.reload();
        // if (ammo[selectedWeapon.gunAmmo].getAmmo()-selectedWeapon.max_mag_capacity<0)
        // {
        //     selectedWeapon.partialReload(ammo[selectedWeapon.gunAmmo].getAmmo());
        //     ammo[selectedWeapon.gunAmmo].setAmmo(0);
        // }
        // else{
            //selectedWeapon.reload();
            //ammo[selectedWeapon.gunAmmo].addAmmo(-selectedWeapon.max_mag_capacity);
        //}
    }
    public virtual void TakeDamage(int damage)
    {
        Mathf.Clamp(damage,0,int.MaxValue);
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Debug.Log(transform.name +"Has Died");
    }
}
