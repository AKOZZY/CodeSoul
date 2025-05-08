using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpecialAbility : MonoBehaviour
{
    PlayerData currentCharacter;

    public float specialMeterCurrent;
    public float specialMeterMax;

    public GameObject boombox;
    public GameObject throwingKnife;

    public GameObject specialSprite;

    private void Start()
    {
        specialMeterCurrent = 100;
        specialMeterMax = 100;
    }

    private void Update()
    {
        currentCharacter = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().currentCharacter;

        UpdateSpecial();
        ActivateSpecial();
        ChangeSpecialSprite();
    }

    private void UpdateSpecial()
    {
        if(specialMeterCurrent < specialMeterMax)
        {
            specialMeterCurrent += Time.deltaTime;
        }
        else
        {
            specialMeterCurrent = specialMeterMax;
        }
    }

    private void OnKillIncreaseSpecial(int specialMeterReward)
    {
        specialMeterCurrent += specialMeterReward;
    }

    private void ActivateSpecial()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(currentCharacter.characterName == "K" && specialMeterCurrent == specialMeterMax)
            {
                Instantiate(boombox, transform.position, Quaternion.identity);
                specialMeterCurrent = 0;
            }
            else if(currentCharacter.characterName == "Slasher" && specialMeterCurrent == specialMeterMax)
            {
                Instantiate(throwingKnife, transform.position, Quaternion.identity);
                specialMeterCurrent = 0;
            }
        }
    }

    private void ChangeSpecialSprite()
    {
        specialSprite.GetComponent<Image>().sprite = currentCharacter.specialAbilityPortrait;
    }
}
