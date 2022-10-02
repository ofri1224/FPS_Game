using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hand_pos_setter : MonoBehaviour
{
    public Transform Rhand,Lhand;
    private CharacterStats characterStats;
    void Start()
    {
        characterStats = GetComponent<CharacterStats>();
    }
    void Update()
    {
        if (characterStats!=null)
        {
            Rhand.position = characterStats.selectedWeapon.RHandGrabPos.position;
            Lhand.position = characterStats.selectedWeapon.LHandGrabPos.position;
            Rhand.rotation = characterStats.selectedWeapon.RHandGrabPos.rotation;
            Lhand.rotation = characterStats.selectedWeapon.LHandGrabPos.rotation;
        }
    }
}
