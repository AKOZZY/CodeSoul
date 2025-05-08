using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WeaponsManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] PerkManager perkManager;
    [SerializeField] WeaponSlot playerWeaponSlot;
    [SerializeField] WeaponData[] weapons;

    private void Start()
    {

        ResetAmmo();
        GiveCharacterWeapon();
    }

    private void GiveCharacterWeapon()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        if(gameManager.currentCharacter.characterName == "Finch")
        {
            //perkManager.quick = true;
            foreach (WeaponData weapon in weapons)
            {
                if (weapon.title == "Silver Edge")
                {
                    playerWeaponSlot.weapons[0] = weapon;
                }
            }
        }
        else if(gameManager.currentCharacter.characterName == "K")
        {
            //perkManager.revival = true;
            foreach (WeaponData weapon in weapons)
            {
                if (weapon.title == "Fire Axe")
                {
                    playerWeaponSlot.weapons[0] = weapon;
                }
            }
        }
        else if(gameManager.currentCharacter.characterName == "Agent")
        {
            //perkManager.greed = true;
            foreach (WeaponData weapon in weapons)
            {
                if(weapon.title == "AA-12")
                {
                    playerWeaponSlot.weapons[0] = weapon;
                }
            }
        }
        else if (gameManager.currentCharacter.characterName == "Lieutenant")
        {
            //perkManager.greed = true;
            foreach (WeaponData weapon in weapons)
            {
                if (weapon.title == "The Trench")
                {
                    playerWeaponSlot.weapons[0] = weapon;
                }
            }
        }
        else if (gameManager.currentCharacter.characterName == "Slasher")
        {
            //perkManager.greed = true;
            foreach (WeaponData weapon in weapons)
            {
                if (weapon.title == "Hunting Knife")
                {
                    playerWeaponSlot.weapons[0] = weapon;
                }
            }
        }
    }

    private void ResetAmmo()
    {
        foreach (WeaponData weapon in weapons)
        {
            weapon.ammo = weapon.magazine;
        }
    }
}
