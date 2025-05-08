using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    GameManager gameManager;

    public TMP_Text amountOfkills;
    public TMP_Text amountOfWaves;
    public TMP_Text rank;

    public GameObject characterPortait;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Start()
    {
        amountOfkills.text = "DEMONS KILLED: " + gameManager.kills.ToString();
        amountOfWaves.text = "WAVES SURVIVED: " + gameManager.wavesSurvived.ToString();
        rank.text = gameManager.ranking;
        characterPortait.GetComponent<Image>().sprite = gameManager.currentCharacter.portraitNobackground;
    }

    
}
