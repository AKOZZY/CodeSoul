using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    UIManager ui;
    Player player;
    GameManager gameManager;

    public int turretAmount = 0;
    public int barricadeAmount = 0;

    public GameObject turret;
    public GameObject barricade;

    public GameObject turretBuildButton;
    public GameObject barricadeBuildButton;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();  
    }

    private void Update()
    {
        if(turretAmount <= 0)
        {
            turretBuildButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            turretBuildButton.GetComponent<Button>().interactable = true;
        }

        if (barricadeAmount <= 0)
        {
            barricadeBuildButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            barricadeBuildButton.GetComponent<Button>().interactable = true;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            SpawnTurret();
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            SpawnBarricade();
        }
    }

    public void PurchaseTurret(int price)
    {
        if(gameManager.playerPoints >= price)
        {
            gameManager.playerPoints -= price;
            turretAmount++;
        }
    }

    public void PurchaseBarricade(int price)
    {
        if (gameManager.playerPoints >= price)
        {
            gameManager.playerPoints -= price;
            barricadeAmount++;
        }
    }

    public void SpawnTurret()
    {
        if(!player.standingNearBuildable && turretAmount > 0)
        {
            Vector3 playerPos;
            Vector3 offset = new Vector3(0, .5f, 0);
            playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

            Instantiate(turret, playerPos + offset, Quaternion.identity);
            turretAmount--;
        }
    }

    public void SpawnBarricade()
    {
        if(!player.standingNearBuildable && barricadeAmount > 0)
        {
            Vector3 playerPos;
            Vector3 offset = new Vector3(0, .5f, 0);
            playerPos = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;

            Instantiate(barricade, playerPos + offset, Quaternion.identity);
            barricadeAmount--;
        }
    }
}
