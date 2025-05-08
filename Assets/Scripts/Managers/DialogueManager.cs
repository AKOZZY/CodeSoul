using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    GameManager gameManager;

    public bool isDialoguePresent = false;
    public bool hasKilledEnemy = false;

    public GameObject dialogueBox;
    public GameObject playerPicture;

    [Header("CHARACTER SOUNDS")]
    AudioSource audioSource;
    public AudioClip finchSound;
    public AudioClip kSound;
    public AudioClip agentSound;
    public AudioClip lieutenantSound;

    [Header("PORTRAITS")]
    public Sprite finch;
    public Sprite k;
    public Sprite agent;
    public Sprite lieutenant;

    [Header("DIALOGUE")]
    public TMP_Text dialogueTextBox;

    // Starting wave lines
    string[] onStartLinesFinch = { "What happened?", "Did I? Where am I?", "Hello? Is anyone there?" };
    string[] onStartLinesK = { "Ugh, my head... it's pounding.", "Where am I? I'm not in the city anymore.", "Is this it?" };
    string[] onStartLinesAgent = { "Great... I'm in the middle of nowhere.", "Wheres my squad?!", "Looks like I'm all alone." };
    string[] onStartLinesLieutenant = { "Captain?! Where did you go?", "What the hell is this place!", "I don't have a good feeling about this." };

    // Drinking Perk Lines
    string[] onDrinkLinesFinch = { "Ugh... this tastes horrible!", "Oh my god... I think I'm gonna puke.", "What the.. what the hell did they put in this thing?" };
    string[] onDrinkLinesK = { "Hmm... weird taste.", "The taste is uhh... not very great.", "I feel... refreshed?" };
    string[] onDrinkLinesAgent = { "I've had worse tasting drinks before.", "This drink tastes like battery acid.", "My body ain't gonna like this." };
    string[] onDrinkLinesLieutenant = { "Ugh, this tastes like off-brand cola mixed with dirt.", "This drink is making my mouth burn!", "I'm gonna regret drinking this." };

    // First Kill Lines
    string[] onFirstKillLinesFinch = { "Oh my god... this is a living nightmare.", "What the hell are these things!", "Stay back!" };
    string[] onFirstKillLinesK = { "Stay back you freaks!", "What the hell are you?", "Stay back, I'm not afraid to use this!" };
    string[] onFirstKillLinesAgent = { "Hands in the air!", "Good thing I did my research on these freaks.", "Where the hell is my backup?" };
    string[] onFirstKillLieutenant = { "Those creatures! The creatures from my dream! They are real!", "Die you ugly freak!", "Get the hell away from me!" };

    // Random Lines After First Kill
    string[] onRandomKillLinesFinch = { "I'm getting good at this.", "This is kinda fun!", "Die demon spawn!" };
    string[] onRandomKillLinesK = { "You disgust me.", "This is getting ugly!", "I'm sending you all back to hell!" };
    string[] onRandomKillLinesAgent = { "Target neutralised!", "Target down!", "Confirmed kill!" };
    string[] onRandomKillLieutenant = { "You want some more?!", "I have enough lead for all of you!", "You don't want to do this! I'm a cop!" };

    // When Injured Lines
    string[] onHurtLinesFinch = { "That hurt!", "Get those claws away from me!", "You scratched me! You monster!" };
    string[] onHurtLinesK = { "Oh man that hurt.", "Damn, I'm bleeding.", "Stop it! Stop hurting me!" };
    string[] onHurtLinesAgent = { "I'm hit!", "It's gonna take more than that to bring me down!", "You just signed your death warrant!" };
    string[] onHurtLinesLieutenant = { "Don't scratch me!", "Get those claws away from me", "Thats gonna leave a mark." };

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = .2f;
        StartingDialogue();
    }

    private IEnumerator TypeWriterEffect(TMP_Text text, string message, float textSpeed, AudioClip characterSound)
    {
        isDialoguePresent = true;
        foreach (char c in message)
        {
            text.text += c;
            audioSource.PlayOneShot(characterSound);
            yield return new WaitForSeconds(textSpeed);
        }
        yield return new WaitForSeconds(3);
        dialogueBox.SetActive(false);
        text.text = string.Empty;
        isDialoguePresent = false;
    }

    private void EnableDialogueBox()
    {
        if (gameManager.currentCharacter.characterName == "Finch")
        {
            dialogueBox.SetActive(true);
            playerPicture.GetComponent<Image>().sprite = finch;
        }
        else if (gameManager.currentCharacter.characterName == "K")
        {
            dialogueBox.SetActive(true);
            playerPicture.GetComponent<Image>().sprite = k;
        }
        else if (gameManager.currentCharacter.characterName == "Agent")
        {
            dialogueBox.SetActive(true);
            playerPicture.GetComponent<Image>().sprite = agent;
        }
        else if(gameManager.currentCharacter.characterName == "Lieutenant")
        {
            dialogueBox.SetActive(true);
            playerPicture.GetComponent<Image>().sprite = lieutenant;
        }
    }

    private void StartingDialogue()
    {
        if (gameManager.currentCharacter.characterName == "Finch")
        {
            EnableDialogueBox();
            string randomLine = onStartLinesFinch[Random.Range(0, onStartLinesFinch.Length)];
            StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, finchSound));
        }
        else if (gameManager.currentCharacter.characterName == "K")
        {
            EnableDialogueBox();
            string randomLine = onStartLinesK[Random.Range(0, onStartLinesK.Length)];
            StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, kSound));
        }
        else if (gameManager.currentCharacter.characterName == "Agent")
        {
            EnableDialogueBox();
            string randomLine = onStartLinesAgent[Random.Range(0, onStartLinesAgent.Length)];
            StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, agentSound));
        }
        else if (gameManager.currentCharacter.characterName == "Lieutenant")
        {
            EnableDialogueBox();
            string randomLine = onStartLinesLieutenant[Random.Range(0, onStartLinesLieutenant.Length)];
            StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, lieutenantSound));
        }
    }

    public void DrinkPerkDialogue()
    {
        if (gameManager.currentCharacter.characterName == "Finch")
        {
            if(!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onDrinkLinesFinch[Random.Range(0, onDrinkLinesFinch.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, finchSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "K")
        {
            if(!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onDrinkLinesK[Random.Range(0, onDrinkLinesK.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, kSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Agent")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onDrinkLinesAgent[Random.Range(0, onDrinkLinesAgent.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, agentSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Lieutenant")
        {
            if(!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onDrinkLinesLieutenant[Random.Range(0, onDrinkLinesLieutenant.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, lieutenantSound));
            }
        }
    }

    public void KillFirstEnemyDialogue()
    {
        if (gameManager.currentCharacter.characterName == "Finch")
        {
            if (!isDialoguePresent && !hasKilledEnemy)
            {
                EnableDialogueBox();
                string randomLine = onFirstKillLinesFinch[Random.Range(0, onFirstKillLinesFinch.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, finchSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "K")
        {
            if (!isDialoguePresent && !hasKilledEnemy)
            {
                EnableDialogueBox();
                string randomLine = onFirstKillLinesK[Random.Range(0, onRandomKillLinesK.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, kSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Agent")
        {
            if (!isDialoguePresent && !hasKilledEnemy)
            {
                EnableDialogueBox();
                string randomLine = onFirstKillLinesAgent[Random.Range(0, onFirstKillLinesAgent.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, agentSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Lieutenant")
        {
            if (!isDialoguePresent && !hasKilledEnemy)
            {
                EnableDialogueBox();
                string randomLine = onFirstKillLieutenant[Random.Range(0, onFirstKillLieutenant.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, lieutenantSound));
            }
        }
    }

    public void HurtDialogue()
    {
        if (gameManager.currentCharacter.characterName == "Finch")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onHurtLinesFinch[Random.Range(0, onHurtLinesFinch.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, finchSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "K")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onHurtLinesK[Random.Range(0, onHurtLinesK.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, kSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Agent")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onHurtLinesAgent[Random.Range(0, onHurtLinesAgent.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, agentSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Lieutenant")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onHurtLinesLieutenant[Random.Range(0, onHurtLinesLieutenant.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, lieutenantSound));
            }
        }
    }

    public void RandomKillDialogue()
    {
        if (gameManager.currentCharacter.characterName == "Finch")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onRandomKillLinesFinch[Random.Range(0, onRandomKillLinesFinch.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, finchSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "K")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onRandomKillLinesK[Random.Range(0, onRandomKillLinesK.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, kSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Agent")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onRandomKillLinesAgent[Random.Range(0, onRandomKillLinesAgent.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, agentSound));
            }
        }
        else if (gameManager.currentCharacter.characterName == "Lieutenant")
        {
            if (!isDialoguePresent)
            {
                EnableDialogueBox();
                string randomLine = onRandomKillLieutenant[Random.Range(0, onRandomKillLieutenant.Length)];
                StartCoroutine(TypeWriterEffect(dialogueTextBox, randomLine, .05f, lieutenantSound));
            }
        }
    }
}
