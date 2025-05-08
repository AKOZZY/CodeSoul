using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClearDebris : MonoBehaviour
{
    GameManager gameManager;
    bool inRange;

    public int price;

    [SerializeField] GameObject interactPrompt;
    [SerializeField] TMP_Text interactText;

    public GameObject spawnerToActivate;

    Animator animator;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && gameManager.playerPoints >= price && inRange)
        {
            gameManager.playerPoints -= price;
            DeleteDebris();
        }
    }

    private void DeleteDebris()
    {
        if(spawnerToActivate != null)
        {
            spawnerToActivate.SetActive(true);
        }
        animator.SetBool("clear", true);
        Destroy(gameObject, .30f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            interactText.text = "Press and hold F to clear debris for " + price + ".";
            interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;

            interactText.text = "Press and hold F to clear debris for " + price + ".";
            interactPrompt.SetActive(true);
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
