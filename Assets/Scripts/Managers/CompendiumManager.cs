using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CompendiumManager : MonoBehaviour
{
    SaveDataManager saveData;

    public TMP_Text reachWave10;
    public TMP_Text reachWave20;
    public TMP_Text reachWave30;

    public TMP_Text meleeKills;
    public TMP_Text totalKills;

    private void Start()
    {
        saveData = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveDataManager>();

        reachWave10.text = saveData.highestWaveOnAnyMap.ToString();
        reachWave20.text = saveData.highestWaveOnAnyMap.ToString();
        reachWave30.text = saveData.highestWaveOnAnyMap.ToString();

        meleeKills.text = saveData.killsWithMeleeWeapons.ToString();
        totalKills.text = saveData.kills.ToString();
    }
}
