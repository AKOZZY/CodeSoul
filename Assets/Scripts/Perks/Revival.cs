using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Revival : MonoBehaviour
{
    [SerializeField] PerkManager perkManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GlobalSoundManager soundManager;
    GameManager gm;
    Animator animator;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    bool inRange = false;
    bool alreadyBoughtBefore = false;
    public int price;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        PurchasePerk();
    }

    private void PurchasePerk()
    {
        if (Input.GetKeyDown(KeyCode.F) && inRange && gm.playerPoints >= price && !perkManager.revival && !alreadyBoughtBefore)
        {
            gm.playerPoints -= price;
            perkManager.revival = true;
            dialogueManager.DrinkPerkDialogue();
            animator.SetBool("bought", true);
            alreadyBoughtBefore = true;
            soundManager.Purchase();
            soundManager.ReviveSodaSFX();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!perkManager.revival && !alreadyBoughtBefore)
            {
                interactText.text = "Press and hold F to buy Revival for " + price + ".";
                interactPrompt.SetActive(true);
                inRange = true;
            }
            else
            {
                interactText.text = "Perk not in service!";
                interactPrompt.SetActive(true);
                inRange = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!perkManager.revival && !alreadyBoughtBefore)
            {
                interactText.text = "Press and hold F to buy Revival for " + price + ".";
                interactPrompt.SetActive(true);
                inRange = true;
            }
            else
            {
                interactText.text = "Perk not in service!";
                interactPrompt.SetActive(true);
                inRange = true;
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
