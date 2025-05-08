using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoryUI : MonoBehaviour
{
    // Scripts
    [SerializeField] PlayerData playerData;
    [SerializeField] Player player;
    [SerializeField] StoryWeapon weapon;


    GameManager gameManager;
    EventsManager eventsManager;

    // UI Elements
    public GameObject fadeIn;

    Image weaponImage;
    TMP_Text weaponName;

    
    public TMP_Text ammoCount;
    public TMP_Text magazineCount;


    public TMP_Text interactText;
    public GameObject interactPrompt;
    public TMP_Text playerHPStatus;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        eventsManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventsManager>();

        //eventsManager.InitializeData();
        eventsManager.GetWeaponSlot();

        fadeIn.SetActive(true);
    }

    private void Update()
    {
        


        

        


        PlayerHPStatus();
    }

    private void PlayerHPStatus()
    {
        if (player.hp == 3)
        {
            playerHPStatus.text = "FINE";
            playerHPStatus.color = Color.green;
        }
        else if (player.hp == 2)
        {
            playerHPStatus.text = "CAUTION";
            playerHPStatus.color = Color.yellow;
        }
        else if (player.hp == 1)
        {
            playerHPStatus.text = "DANGER";
            playerHPStatus.color = Color.red;
        }
        else
        {
            playerHPStatus.text = "DEAD";
            playerHPStatus.color = Color.red;
        }
    }

   

    public bool IsMousePointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void onInteract()
    {
        interactPrompt.SetActive(true);
    }

    



    public void offInteract()
    {
        interactPrompt.SetActive(false);
    }

   

    


}
