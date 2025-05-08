using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public MenuManager menuManager;
    public CharacterSelectUI characterSelectUI;
    public PlayerData defaultChar;
    public PlayerData currentCharacter;

    public enum currentDifficulty
    {
        easy,
        normal,
        hard
    }

    public currentDifficulty difficulty;

    public double playerPoints;
    public int ammo;

    public int wavesSurvived = 0;
    public int kills = 0;
    public string ranking = "F";

    public void Awake()
    {
        SetFPS(144);

        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //currentCharacter = defaultChar; 

        playerPoints = 500;

        difficulty = currentDifficulty.easy;
        
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("MainMenu"))
        {
            menuManager = GameObject.FindGameObjectWithTag("UI").GetComponent<MenuManager>();
        }
        

        //characterSelectUI = menuManager.GetComponentInChildren<CharacterSelectUI>();
        //currentCharacter = characterSelectUI.currentCharacter;

    }

    public void RankCalculator()
    {
        int sumOfKillsAndWavesWon = kills + wavesSurvived;

        if(sumOfKillsAndWavesWon < 50)
        {
            ranking = "F";
        }

        if(sumOfKillsAndWavesWon > 100)
        {
            ranking = "C";
        }

        if(sumOfKillsAndWavesWon > 300)
        {
            ranking = "B";
        }

        if(sumOfKillsAndWavesWon > 500)
        {
            ranking = "A";
        }

        if (sumOfKillsAndWavesWon > 1000)
        {
            ranking = "S";
        }
    }

    public void SetFPS(int value)
    {
        Application.targetFrameRate = value;
    }

    public void SetDifficulty(int val)
    {
        if (!menuManager.inGame)
        {
            if(val == 0)
            {
                difficulty = currentDifficulty.easy;
            }
            else if (val == 1)
            {
                difficulty = currentDifficulty.normal;
            }
            else
            {
                difficulty = currentDifficulty.hard;
            }
        }
    }

    public void UpdateSelectedCharacter()
    {
        if (!menuManager.inGame)
        {
            currentCharacter = menuManager.GetCharacter();
        }
    }
}
