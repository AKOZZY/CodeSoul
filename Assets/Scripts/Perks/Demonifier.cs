using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Demonifier : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GlobalSoundManager soundManager;
    public WeaponData[] upgradedWeapons;
    [SerializeField] WeaponSlot slot;
    bool inRange;
    bool canUpgrade;
    public int price;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {

        //IsCurrentGunUpgradable();

        UpgradeWeapon();
    }

    private void UpgradeWeapon()
    {
        if(Input.GetKeyDown(KeyCode.F) && inRange && gameManager.playerPoints >= price && canUpgrade)
        {
            //ReplaceWeapon();
            CheckWeapon();
            gameManager.playerPoints -= price;
            //soundManager.DemonifierSFX();
            soundManager.Purchase();
        }
    }

    private void IsCurrentGunUpgradable()
    {
        if(slot.GetCurrentWeapon().isUpgraded)
        {
            canUpgrade = false;
        }
        else
        {
            canUpgrade = true;
        }
    }

    private void CheckWeapon()
    {
        foreach (WeaponData upgradedWeapon in upgradedWeapons)
        {
            if(upgradedWeapon.upgradedVariantOf == slot.GetCurrentWeapon().title)
            {
                slot.weapons[slot.currentWeapon] = upgradedWeapon;
            }
        }
    }

    private void ReplaceWeapon()
    {
        if (slot.GetCurrentWeapon().title == "Silver Edge")
        {
            foreach (WeaponData weaponUpgradeVariant in upgradedWeapons)
            {
                if (weaponUpgradeVariant.title == "Crimson Edge")
                {
                    slot.ReplaceWeaponWithUpgradedVersion(weaponUpgradeVariant);
                }
            }
        }
        else if(slot.GetCurrentWeapon().title == "M4 Commando")
        {
            foreach (WeaponData weaponUpgradeVariant in upgradedWeapons)
            {
                if (weaponUpgradeVariant.title == "Hellfire")
                {
                    slot.ReplaceWeaponWithUpgradedVersion(weaponUpgradeVariant);
                }
            }
        }
        else if (slot.GetCurrentWeapon().title == "The Trench")
        {
            foreach (WeaponData weaponUpgradeVariant in upgradedWeapons)
            {
                if (weaponUpgradeVariant.title == "D3ad Shot")
                {
                    slot.ReplaceWeaponWithUpgradedVersion(weaponUpgradeVariant);
                }
            }
        }
        else if (slot.GetCurrentWeapon().title == "AA-12")
        {
            foreach (WeaponData weaponUpgradeVariant in upgradedWeapons)
            {
                if (weaponUpgradeVariant.title == "AAA-HH")
                {
                    slot.ReplaceWeaponWithUpgradedVersion(weaponUpgradeVariant);
                }
            }
        }
        else if (slot.GetCurrentWeapon().title == "Fire Axe")
        {
            foreach (WeaponData weaponUpgradeVariant in upgradedWeapons)
            {
                if (weaponUpgradeVariant.title == "Axe Of Fire")
                {
                    slot.ReplaceWeaponWithUpgradedVersion(weaponUpgradeVariant);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            interactText.text = "Press and hold F to upgrade weapon for " + price + ".";
            interactPrompt.SetActive(true);

            IsCurrentGunUpgradable();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            interactText.text = "Press and hold F to upgrade weapon for " + price + ".";
            interactPrompt.SetActive(true);

            IsCurrentGunUpgradable();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;

            interactText.text = string.Empty;
            interactPrompt.SetActive(false);
        }
    }
}
