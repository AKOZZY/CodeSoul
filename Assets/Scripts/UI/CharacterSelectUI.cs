using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectUI : MonoBehaviour
{
    public PlayerData currentCharacter;
    [SerializeField] SaveDataManager saveDataManager;

    // Character Name Text
    public TMP_Text characterName;

    // Character Bio Text
    public TMP_Text bio;

    string samBio = "Samantha Finch used to be a famous urban explorer. She will outrun the danger!";

    string kBio = "K Mac is a famous rockstar for the band \"BloodPakt\". He never gives up and will help keep the show going!";

    string agentBio = "Jon Rivers was a former detective for the Kirktown Police Department. " +
        "He is not afraid of selling out his closest friends to make a buck. Jon and Jackson have a bit of a rivalry.";

    string lieutenantBio = "Jackson Hart is a well respected officer who served for the Kirktown Police Department. " +
        "He always looks out for his teammates. He hates Jon with a passion, but for now they will have to get along to survive.";

    string jBio = "J Mac loves to cosplay as her favourite characters from movies and video games. " +
        "Her motivation for success is unstoppable! She loves her brothers music!";

    // Animated Portraits
    [Header("CHARACTER PIXELATED PORTAITS")]
    public GameObject sam;
    public GameObject k;
    public GameObject agent;
    public GameObject lieutenant;
    public GameObject j;

    // Drawn Portraits
    [Header("CHARACTER DRAWN PORRTRAITS")]
    public GameObject samP;
    public GameObject kP;
    public GameObject agentP;
    public GameObject lieutenantP;
    public GameObject jP;

    [Header("WEAPON STUFF")]
    public GameObject gunImage;
    public TMP_Text weaponName;
    public Sprite silverEdge;
    public Sprite fireAxe;
    public Sprite aa12;
    public Sprite pumpAction;
    public Sprite knife;

    [Header("CHARACTER BUTTONS")]
    public GameObject finchButton;
    public GameObject kButton;
    public GameObject lieutenantButton;
    public GameObject agentButton;
    public GameObject jButton;
    

    private void Update()
    {
        currentCharacter = GameObject.FindGameObjectWithTag("UI").GetComponent<MenuManager>().GetCharacter();
        CharacterSwap();
        ToggleCharacterButtons();
    }

    private void ToggleCharacterButtons()
    {
        if(saveDataManager.hasK == 1)
        {
            kButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            kButton.GetComponent<Button>().interactable = false;
        }

        if (saveDataManager.hasLieutenant == 1)
        {
            lieutenantButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            lieutenantButton.GetComponent<Button>().interactable = false;
        }

        if (saveDataManager.hasAgent == 1)
        {
            agentButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            agentButton.GetComponent<Button>().interactable = false;
        }

        if (saveDataManager.hasSlasher == 1)
        {
            jButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            jButton.GetComponent<Button>().interactable = false;
        }
    }

    private void Start()
    {
        if(currentCharacter != null)
        {
            characterName.text = currentCharacter.name;
        }
        bio.text = samBio;
    }

    public void CharacterSwap()
    {
        if (currentCharacter != null)
        {
            if (currentCharacter.characterName == "Finch")
            {
                // Portraits
                sam.SetActive(true);
                samP.SetActive(true);
                k.SetActive(false);
                kP.SetActive(false);
                agent.SetActive(false);
                agentP.SetActive(false);
                lieutenant.SetActive(false);
                lieutenantP.SetActive(false);
                j.SetActive(false);
                jP.SetActive(false);

                // Weapon
                gunImage.GetComponent<Image>().sprite = silverEdge;
                weaponName.text = "Silver Edge";

                characterName.text = currentCharacter.characterName;
                bio.text = samBio;
            }
            else if (currentCharacter.characterName == "K")
            {
                // Portraits
                sam.SetActive(false);
                samP.SetActive(false);
                k.SetActive(true);
                kP.SetActive(true);
                agent.SetActive(false);
                agentP.SetActive(false);
                lieutenant.SetActive(false);
                lieutenantP.SetActive(false);
                j.SetActive(false);
                jP.SetActive(false);

                // Weapon
                gunImage.GetComponent<Image>().sprite = fireAxe;
                weaponName.text = "Fireaxe";

                characterName.text = currentCharacter.characterName;
                bio.text = kBio;
            }
            else if (currentCharacter.characterName == "Agent")
            {
                // Portraits
                sam.SetActive(false);
                samP.SetActive(false);
                k.SetActive(false);
                kP.SetActive(false);
                agent.SetActive(true);
                agentP.SetActive(true);
                lieutenant.SetActive(false);
                lieutenantP.SetActive(false);
                j.SetActive(false);
                jP.SetActive(false);

                // Weapon
                gunImage.GetComponent<Image>().sprite = aa12;
                weaponName.text = "AA-12";

                characterName.text = currentCharacter.characterName;
                bio.text = agentBio;
            }
            else if (currentCharacter.characterName == "Lieutenant")
            {
                // Portraits
                sam.SetActive(false);
                samP.SetActive(false);
                k.SetActive(false);
                kP.SetActive(false);
                agent.SetActive(false);
                agentP.SetActive(false);
                lieutenant.SetActive(true);
                lieutenantP.SetActive(true);
                j.SetActive(false);
                jP.SetActive(false);

                // Weapon
                gunImage.GetComponent<Image>().sprite = pumpAction;
                weaponName.text = "The Trench";

                characterName.text = currentCharacter.characterName;
                bio.text = lieutenantBio;
            }
            else if (currentCharacter.characterName == "Slasher")
            {
                // Portraits
                sam.SetActive(false);
                samP.SetActive(false);
                k.SetActive(false);
                kP.SetActive(false);
                agent.SetActive(false);
                agentP.SetActive(false);
                lieutenant.SetActive(false);
                lieutenantP.SetActive(false);
                j.SetActive(true);
                jP.SetActive(true);

                // Weapon
                gunImage.GetComponent<Image>().sprite = knife;
                weaponName.text = "Hunting Knife";

                characterName.text = currentCharacter.characterName;
                bio.text = jBio;
            }
        }
    }
}
