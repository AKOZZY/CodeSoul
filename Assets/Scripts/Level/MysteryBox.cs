using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MysteryBox : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GlobalSoundManager gsm;

    [SerializeField] WeaponData[] weaponsInBox;
    [SerializeField] WeaponData[] upgradedWeapons;

    [SerializeField] WeaponSlot weaponSlot;

    Animator animator;
    public GameObject weaponSprite;

    bool inRange;
    bool canGrabWeapon;
    public bool opened = false;
    public bool dupeDetected = false;

    public WeaponData boxWeapon;

    public float timer;
    public int pickUpTime = 5;

    public float boxTimer;
    public int timeUntilCanOpenBox = 2;

    public int price;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        
        if(!opened)
        {
            boxTimer += Time.deltaTime;
        }
        else
        {
            boxTimer = 0;
        }

        if(timeUntilCanOpenBox < boxTimer)
        {
            OpenBox();
        }


        TimeToGrab();
    }

    private void OpenBox()
    {
        if(Input.GetKeyDown(KeyCode.F) && inRange && !opened && gameManager.playerPoints >= price)
        {
            gameManager.playerPoints -= price;
            opened = true;
            animator.SetBool("open", true);
            StartCoroutine(GiveRandomWeapon(80, .05f));
            gsm.Purchase();
            gsm.MysteryBoxSFX();
        }
    }

    private void TimeToGrab()
    {
        if(canGrabWeapon)
        {
            timer += Time.deltaTime;

            if (boxWeapon.title == weaponSlot.weapons[0].title || boxWeapon.title == weaponSlot.weapons[0].upgradedVariantOf)
            {
                //Debug.Log("Dupe detected");
                dupeDetected = true;
            }
            else if (weaponSlot.weapons[1] != null && boxWeapon.title == weaponSlot.weapons[1].title || weaponSlot.weapons[1] != null && boxWeapon.title == weaponSlot.weapons[1].upgradedVariantOf)
            {
                //Debug.Log("Dupe detected");
                dupeDetected = true;
            }
            else
            {
                dupeDetected = false;
            }


            if (Input.GetKeyDown(KeyCode.F) && inRange)
            {
                if (weaponSlot.weapons[1] == null)
                {
                    weaponSlot.weapons[1] = boxWeapon;
                    weaponSlot.OnWeaponPickup();
                }
                else
                {
                    weaponSlot.weapons[weaponSlot.currentWeapon] = boxWeapon;
                    weaponSlot.OnWeaponPickup();
                }

                boxWeapon = null;
                canGrabWeapon = false;
                weaponSprite.SetActive(false);
                timer = 0;
                opened = false;
                animator.SetBool("open", false);
            }

            if(dupeDetected)
            {
                WeaponData randomWeapon = weaponsInBox[Random.Range(0, weaponsInBox.Length)];
                weaponSprite.GetComponent<SpriteRenderer>().sprite = randomWeapon.sprite;
                boxWeapon = randomWeapon;
            }
            
            
            if(timer > pickUpTime)
            {
                timer = 0;
                weaponSprite.SetActive(false);
                canGrabWeapon = false;
                opened = false;
                animator.SetBool("open", false);
            }
        }
    }

    private IEnumerator GiveRandomWeapon(int amountOfSwaps, float duration)
    {
        weaponSprite.SetActive(true);

        for (int i = 0; i < amountOfSwaps; i++)
        {
            WeaponData randomWeapon = weaponsInBox[Random.Range(0, weaponsInBox.Length)];
            weaponSprite.GetComponent<SpriteRenderer>().sprite = randomWeapon.sprite;
            boxWeapon = randomWeapon;
            yield return new WaitForSeconds(duration);
        }
        
        canGrabWeapon = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (!opened)
            {
                interactText.text = "Press and hold F to buy random weapon for " + price + ".";
                interactPrompt.SetActive(true);
            }
            else
            {
                interactText.text = string.Empty;
                interactPrompt.SetActive(false);

                if(canGrabWeapon)
                {
                    interactText.text = "Press and hold F to grab " + boxWeapon.title + ".";
                    interactPrompt.SetActive(true);
                }
                else
                {
                    interactText.text = string.Empty;
                    interactPrompt.SetActive(false);
                }
            }

            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            if (!opened)
            {
                interactText.text = "Press and hold F to buy random weapon for " + price + ".";
                interactPrompt.SetActive(true);
            }
            else
            {
                interactText.text = string.Empty;
                interactPrompt.SetActive(false);

                if (canGrabWeapon)
                {
                    interactText.text = "Press and hold F to grab " + boxWeapon.title + ".";
                    interactPrompt.SetActive(true);
                }
                else
                {
                    interactText.text = string.Empty;
                    interactPrompt.SetActive(false);
                }
            }
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
