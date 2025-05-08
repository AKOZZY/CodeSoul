using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum ActiveScreen
{
    none,
    playScreen,
    settingsScreen,
    storyScreen,
    compendiumScreen
}

public class MenuManager : MonoBehaviour
{
    
    ActiveScreen activeScreen;

    
    public PlayerData currentCharacter;
    GameManager gm;
    SaveDataManager saveDataManager;

    public GameObject playScreenUI;
    public GameObject settingsScreenUI;
    public GameObject storyScreenUI;
    public GameObject compendiumScreenUI;

    public bool inGame = false;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        saveDataManager = gm.GetComponent<SaveDataManager>();
        

        activeScreen = ActiveScreen.none;

        if(!inGame)
        {
            gm.kills = 0;
            gm.wavesSurvived = 0;
            gm.ranking = string.Empty;
        }

        
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainMenu"))
        {
            inGame = true;
            
        }
        else
        {
            OneScreenAtATime();
        }
    }

    public void OpenPlayScreen()
    {
        //playScreenUI.SetActive(true);
        activeScreen = ActiveScreen.playScreen;
    }

    public void OpenComendiumScreen()
    {
        activeScreen = ActiveScreen.compendiumScreen;
    }

    public void CloseScreen()
    {
        //playScreenUI.SetActive(false);
        activeScreen = ActiveScreen.none;
    }

    public void OpenSettingsScreen()
    {
        //settingsScreenUI.SetActive(true);
        activeScreen = ActiveScreen.settingsScreen;
    }

    public void OpenStoryScreen()
    {
        activeScreen = ActiveScreen.storyScreen;
    }

    

    public void OneScreenAtATime()
    {
        if(!inGame)
        {
            switch (activeScreen)
            {
                case ActiveScreen.playScreen:
                    playScreenUI.SetActive(true);
                    settingsScreenUI.SetActive(false);
                    storyScreenUI.SetActive(false);
                    compendiumScreenUI.SetActive(false);
                    break;

                case ActiveScreen.storyScreen:
                    playScreenUI.SetActive(false);
                    settingsScreenUI.SetActive(false);
                    storyScreenUI.SetActive(true);
                    compendiumScreenUI.SetActive(false);
                    break;

                case ActiveScreen.settingsScreen:
                    playScreenUI.SetActive(false);
                    settingsScreenUI.SetActive(true);
                    storyScreenUI.SetActive(false);
                    compendiumScreenUI.SetActive(false);
                    break;

                case ActiveScreen.compendiumScreen:
                    playScreenUI.SetActive(false);
                    settingsScreenUI.SetActive(true);
                    storyScreenUI.SetActive(false);
                    compendiumScreenUI.SetActive(true);
                    break;

                case ActiveScreen.none:
                    playScreenUI.SetActive(false);
                    settingsScreenUI.SetActive(false);
                    storyScreenUI.SetActive(false);
                    compendiumScreenUI.SetActive(false);
                    break;
            }
        }
    }

    public void QuitButton()
    {
        saveDataManager.SaveData();
        Application.Quit();
    }

    public void SelectCharacter(PlayerData character)
    {
        currentCharacter = character;
        gm.UpdateSelectedCharacter();
        //eventsManager.InitializeData();
    }

    public PlayerData GetCharacter()
    {
        return currentCharacter;
    }

    public void LoadIntoMap()
    {
        if(currentCharacter != null)
        {
            inGame = true;
            
            SceneManager.LoadScene("TheCabin");

        }
    }
}
