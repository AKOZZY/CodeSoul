using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ChalkBuyable : MonoBehaviour
{
    
    public GlobalSoundManager gsm;
    public GameManager gm;
    public bool inRange;
    public bool weaponOwned;

    public int cost;

    public WeaponData weaponData;
    public WeaponSlot weaponSlot;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        cost = weaponData.price;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (!weaponOwned)
            {
                interactText.text = "Press and hold F to buy " + weaponData.title + " for " + cost;
            }
            else
            {
                interactText.text = "Already owned.";
            }

            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (!weaponOwned)
            {
                interactText.text = "Press and hold F to buy " + weaponData.title + " for " + cost;
            }
            else
            {
                interactText.text = "Already owned.";
            }

            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;

            if (!weaponOwned)
            {
                interactText.text = string.Empty;
            }
            else
            {
                interactText.text = string.Empty;
            }

            interactPrompt.SetActive(false);
        }
    }

    private void Update()
    {
        if(inRange && Input.GetKeyDown(KeyCode.F) && gm.playerPoints >= cost && !weaponOwned)
        {
            gm.playerPoints -= cost;
            gsm.Purchase();
            weaponSlot.SetWeapon(weaponData);
            weaponSlot.OnWeaponPickup();
        }

        if (weaponSlot.GetPrimaryWeapon().title == weaponData.title)
        {
            weaponOwned = true;
        }
        else
        {
            weaponOwned = false;
        }

        if(weaponSlot.GetSecondaryWeapon() != null)
        {
            if(weaponSlot.GetSecondaryWeapon().title == weaponData.title)
            {
                weaponOwned = true;
            }
            else
            {
                weaponOwned = false;
            }
        }

        
        
    }
}
