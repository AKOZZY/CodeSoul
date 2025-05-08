using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    // Script Reference
    GameManager gameManager;

    // Player Unlocks
    public int hasK = 0;
    public int hasLieutenant = 0;
    public int hasAgent = 0;
    public int hasSlasher = 0;

    // Map Milestones
    public int highestWaveOnAnyMap = 0;
    //public int highestWaveOnCabin = 0;

    // Challenges
    public int kills;
    public int killsWithMeleeWeapons = 0;

    private void Awake()
    {
        LoadData();
    }

    private void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();

        hasK = 1;
        hasLieutenant = 1;
        hasAgent = 1;
        hasSlasher = 1;
    }

    private void Update()
    {
        //CheckForAchievements();
    }

    public void CheckForAchievements()
    {
        
        if(highestWaveOnAnyMap >= 10)
        {
            hasK = 1;
        }

        if (highestWaveOnAnyMap >= 20)
        {
            hasLieutenant = 1;
        }

        if (highestWaveOnAnyMap >= 30)
        {
            hasAgent = 1;
        }

        if(killsWithMeleeWeapons >= 500)
        {
            hasSlasher = 1;
        }
    }

    public void UnlockAllUnlockables()
    {
        PlayerPrefs.SetInt("unlockedK", 1);
        PlayerPrefs.SetInt("unlockedLieutenant", 1);
        PlayerPrefs.SetInt("unlockedAgent", 1);
        PlayerPrefs.SetInt("unlockedSlasher", 1);

        PlayerPrefs.SetInt("highestWaveCabin", highestWaveOnAnyMap);
        PlayerPrefs.SetInt("totalKills", kills);

        LoadData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("unlockedK", hasK);
        PlayerPrefs.SetInt("unlockedLieutenant", hasLieutenant);
        PlayerPrefs.SetInt("unlockedAgent", hasAgent);
        PlayerPrefs.SetInt("unlockedSlasher", hasSlasher);

        PlayerPrefs.SetInt("highestWaveCabin", highestWaveOnAnyMap);
        PlayerPrefs.SetInt("totalKills", kills);
    }

    public void LoadData()
    {
        hasK = PlayerPrefs.GetInt("unlockedK", hasK);
        hasLieutenant = PlayerPrefs.GetInt("unlockedLieutenant", hasLieutenant);
        hasAgent = PlayerPrefs.GetInt("unlockedAgent", hasAgent);
        hasSlasher = PlayerPrefs.GetInt("unlockedSlasher", hasSlasher);

        highestWaveOnAnyMap = PlayerPrefs.GetInt("highestWaveCabin", highestWaveOnAnyMap);
        kills = PlayerPrefs.GetInt("totalKills", kills);
    }

    public void DeleteData()
    {
        Debug.Log("data deleted");
        PlayerPrefs.DeleteAll();
        LoadData();
    }
}
