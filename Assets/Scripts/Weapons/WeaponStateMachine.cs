using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponStateMachine : MonoBehaviour
{
    [SerializeField] WeaponData[] weaponData;
    [SerializeField] WeaponSlot weaponSlot;
    [SerializeField] Animator animator;

    public enum WeaponList
    {
        none, // ID: 0
        pistol, // ID: 1
        shotgun // ID: 2
    }

    WeaponList weaponList;

    private void Update()
    {
        SetWeaponAnimationControl(weaponSlot.GetCurrentWeaponAnimatorController());
    }

    private void SetWeaponAnimationControl(RuntimeAnimatorController animatiorController)
    {
        int ID = weaponSlot.GetCurrentWeaponID();

        animator.runtimeAnimatorController = animatiorController;
    }

}
