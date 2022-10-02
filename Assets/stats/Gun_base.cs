using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class Gun_base : MonoBehaviour
{
    //public universal_vars.AmmoType gunAmmo;
    public bool IsEquiped;
    public bool singleFire;
    public int damage;
    public float range;
    public float fireRate;
    public float reloadTime;
    public float switchingTime;
    public Transform bulletSpawn;
    public int max_mag_capacity;
    [HideInInspector]
    public int current_mag_capacity;
    public ParticleSystem muzzle_flash;
    public ParticleSystem impact;
    public AudioClip fire_sound,dryfire_sound,reload_sound;
    public float forwardDropForce,upwardDropForce;
    public Rigidbody rb;
    public BoxCollider coll;
    [HideInInspector]
    public float next_time_to_fire=0;
    public Transform RHandGrabPos,LHandGrabPos;
    private AmmoUIScript ammoUIScript;
    private void Start(){
        current_mag_capacity=max_mag_capacity;
        if (IsEquiped)
        {
            rb.isKinematic=true;
        }
        else{
            rb.isKinematic=false;
        }
    }
    public virtual void reload(){
        if (Time.time>=next_time_to_fire)
        {
            //userAmmo.addAmmo(-max_mag_capacity);
            next_time_to_fire=Time.time+reloadTime;
            AudioSource.PlayClipAtPoint(reload_sound,transform.position);
            current_mag_capacity=max_mag_capacity;
            if (ammoUIScript!=null)
            {
                ammoUIScript.SetCount(current_mag_capacity);
            }
        }
    }
    public virtual void partialReload(int ammo){
        if (Time.time>=next_time_to_fire)
        {
            //userAmmo.addAmmo(-ammo);
            next_time_to_fire=Time.time+reloadTime;
            AudioSource.PlayClipAtPoint(reload_sound,transform.position);
            current_mag_capacity=ammo;
            if (ammoUIScript!=null)
            {
                ammoUIScript.SetCount(current_mag_capacity);
            }
        }
    }
    private void Update(){
        Visualise_Aim();
    }
    public void Visualise_Aim(){
        Debug.DrawRay(bulletSpawn.position,bulletSpawn.forward*range,Color.green);
    }
    public virtual void shoot()
    {
        if (Time.time>=next_time_to_fire)
        {
            next_time_to_fire=Time.time+1f/fireRate;
            if (current_mag_capacity>0)
            {
                current_mag_capacity--;
                if (ammoUIScript!=null)
                {
                    ammoUIScript.SetCount(current_mag_capacity);
                }
                AudioSource.PlayClipAtPoint(fire_sound,transform.position);
                muzzle_flash.Play();
                RaycastHit hit;
                if(Physics.Raycast(bulletSpawn.position,bulletSpawn.forward,out hit,range,universal_vars.instance.ShootableLayers))
                {
                    Debug.Log(hit.transform.name);
                    if (hit.rigidbody!=null)
                    {
                        hit.rigidbody.AddRelativeForce(hit.point.normalized,ForceMode.Impulse);
                    }
                    CharacterStats target = hit.transform.GetComponent<CharacterStats>();
                    if (target!=null)
                    {
                        target.TakeDamage(damage);
                    }
                }
            }
            else{
                AudioSource.PlayClipAtPoint(dryfire_sound,transform.position);
            }
        }
    }
    public void ActivateWeapon(bool activate)
    {
        StopAllCoroutines();
        next_time_to_fire=Time.time+switchingTime;
        gameObject.SetActive(activate);
        if (ammoUIScript!=null)
        {
            //ammoUIScript.SetSum(userAmmo.getAmmo());
            ammoUIScript.SetCount(current_mag_capacity);
        }
    }
    public void PickUp(Transform Picker)
    {
        Picker.TryGetComponent<AmmoUIScript>(out ammoUIScript);
        //userAmmo = Picker.GetComponent<CharacterStats>().ammo[gunAmmo];
        IsEquiped=true;
        rb.isKinematic=true;
        transform.SetParent(Picker,false);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
        if (ammoUIScript!=null)
        {
            //ammoUIScript.SetSum(userAmmo.getAmmo());
            ammoUIScript.SetCount(current_mag_capacity);
        }
    }
    public void Drop(){
        if (ammoUIScript!=null)
        {
            //ammoUIScript.SetSum(0);
            ammoUIScript.SetCount(0);
        }
        ammoUIScript=null;
        transform.SetParent(null);
        IsEquiped=false;
        rb.isKinematic=false;
        rb.AddForce(((transform.up*upwardDropForce)+(-transform.forward*forwardDropForce)+rb.velocity),ForceMode.Impulse);
    }
    // public Dictionary<universal_vars.AmmoType,Ammo> GetUserAmmo(Transform parent=null){
    //     if (parent!=null)
    //     {
    //         if(parent.TryGetComponent<CharacterStats>(out CharacterStats Cstats)){
    //             return Cstats.ammo;
    //         }
    //         else{
    //             if (parent.parent==null)
    //             {
    //                 return null;
    //             }
    //             else{
    //                 return GetUserAmmo(parent.parent);
    //             }
    //         }
    //     }
    //     else
    //     {
    //         if(TryGetComponent<CharacterStats>(out CharacterStats Cstats)){
    //             return Cstats.ammo;
    //         }
    //         else{
    //             if (transform.parent==null)
    //             {
    //                 return null;
    //             }
    //             else{
    //                 return GetUserAmmo(transform.parent);
    //             }
    //         }
    //     }
    // }
}
