using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Greed : MonoBehaviour
{
    [SerializeField] PerkManager perkManager;
    [SerializeField] UIManager uiManager;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GlobalSoundManager soundManager;
    GameManager gm;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    bool inRange = false;
    public int price;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        PurchasePerk();
    }

    private void PurchasePerk()
    {
        if(Input.GetKeyDown(KeyCode.F) && inRange && gm.playerPoints >= price && !perkManager.greed)
        {
            gm.playerPoints -= price;
            perkManager.greed = true;
            dialogueManager.DrinkPerkDialogue();
            soundManager.Purchase();
            soundManager.GreedSodaSFX();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (!perkManager.greed)
            {
                interactText.text = "Press and hold F to buy Greed for " + price + ".";
                interactPrompt.SetActive(true);
                inRange = true;
            }
            else
            {
                interactText.text = "Already owned.";
                interactPrompt.SetActive(true);
                inRange = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!perkManager.greed)
            {
                interactText.text = "Press and hold F to buy Greed for " + price + ".";
                interactPrompt.SetActive(true);
                inRange = true;
            }
            else
            {
                interactText.text = "Already owned.";
                interactPrompt.SetActive(true);
                inRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            interactText.text = string.Empty;
            interactPrompt.SetActive(false);
            inRange = false;
        }
    }
}
