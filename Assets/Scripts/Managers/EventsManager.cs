using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class EventsManager : MonoBehaviour
{
    MenuManager menuManager;
    GameManager gameManager;
    PerkManager perkManager;

    //[SerializeField] PlayerData playerData;
    public WeaponSlot weaponSlot;

    private void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        
    }

    public void BuyableEnding(int price)
    {
        gameManager.playerPoints -= price;
        SceneManager.LoadScene("RankingScreen");
    }

    public void InitializeData()
    {
        if (!menuManager.inGame)
        {
            menuManager = GameObject.FindGameObjectWithTag("UI").GetComponent<MenuManager>();
            //playerData = menuManager.GetCharacter();
        }
    }

    public void GetWeaponSlot()
    {
        weaponSlot = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponSlot>();
    }

    public void GivePoints(double points)
    {
        gameManager.playerPoints += points;
    }

    public void GiveWeapon(WeaponData weapon, int cost)
    {
        if (weaponSlot.weapons[1] == null)
        {
            weaponSlot.weapons[1] = weapon;
            gameManager.playerPoints -= cost;
        }
        else if(weaponSlot.weapons[1] != null && !weaponSlot.LookForWeaponInInventory(weapon.id))
        {
            weaponSlot.weapons[weaponSlot.currentWeapon] = weapon;
            gameManager.playerPoints -= cost;
        }
    }

    
}
