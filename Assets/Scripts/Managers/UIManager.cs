using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Scripts
    [SerializeField] PlayerData playerData;
    [SerializeField] Player player;
    [SerializeField] WeaponSlot weapon;
    [SerializeField] EndlessWaveGenerater waves;
    [SerializeField] PerkManager perkManager;
    GameManager gameManager;
    [SerializeField] BuildManager buildManager;
    EventsManager eventsManager;
    [SerializeField] GlobalSoundManager soundManager;
    

    // UI Elements
    public GameObject fadeIn;
    public GameObject fadeOut;

    public Image weaponImage;
    public Image weaponImage2;

    public TMP_Text weaponName;
    public TMP_Text weaponName2;
    public TMP_Text waveCount;
    public TMP_Text pointsCount;
    public TMP_Text ammoCount;
    public TMP_Text magazineCount;
    public TMP_Text ammoCount2;
    public TMP_Text magazineCount2;
    public TMP_Text interactText;
    public TMP_Text countdownText;

    public GameObject turretButton;
    public TMP_Text turretAmountText;

    public GameObject barricadeButton;
    public TMP_Text barricadeAmountText;

    public GameObject interactPrompt;
    public GameObject countdownPrompt;
    public Animator buildInventoryAnimator;

    public GameObject shopMenu;

    public GameObject weaponsSection;
    public GameObject buildablesSection;

    public GameObject greedIcon;
    public GameObject immunityIcon;
    public GameObject revivalIcon;
    public GameObject speedIcon;

    public TMP_Text playerHPStatus;

    public GameObject tabScreen;
    public bool isTabScreenOpen = false;

    public GameObject specialFillMeter;
    public GameObject specialButton;

    public GameObject hitflash;

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
        weaponImage.sprite = weapon.GetPrimaryWeapon().sprite;
        weaponName.text = weapon.GetPrimaryWeapon().title;
        waveCount.text = waves.currentWave.ToString();
        pointsCount.text = gameManager.playerPoints.ToString();

        if(weapon.GetSecondaryWeapon() != null )
        {
            weaponImage2.sprite = weapon.GetSecondaryWeapon().sprite;
            weaponName2.text = weapon.GetSecondaryWeapon().title;

            weaponImage2.enabled = true;

            if (weapon.GetSecondaryWeapon().type == WeaponType.melee)
            {
                ammoCount2.text = string.Empty;
                magazineCount2.text = string.Empty;
            }
            else
            {
                ammoCount2.text = weapon.GetSecondaryWeapon().ammo.ToString();
                magazineCount2.text = weapon.GetSecondaryWeapon().magazine.ToString();
            }
        }
        else
        {
            weaponImage2.enabled = false;
            weaponName2.text = "None";

            ammoCount2.text = string.Empty;
            magazineCount2.text = string.Empty;
        }

        if (weapon.GetPrimaryWeapon().type == WeaponType.melee)
        {
            ammoCount.text = string.Empty;
            magazineCount.text = string.Empty;
        }
        else
        {
            ammoCount.text = weapon.GetPrimaryWeapon().ammo.ToString();
            magazineCount.text = weapon.GetPrimaryWeapon().magazine.ToString();
        }

        if(weapon.GetSecondaryWeapon() != null)
        {
            if (weapon.GetSecondaryWeapon().type == WeaponType.melee)
            {
                ammoCount2.text = string.Empty;
                magazineCount2.text = string.Empty;
            }
            else
            {
                ammoCount2.text = weapon.GetSecondaryWeapon().ammo.ToString();
                magazineCount2.text = weapon.GetSecondaryWeapon().magazine.ToString();
            }
        }



        turretAmountText.text = buildManager.turretAmount.ToString();
        barricadeAmountText.text = buildManager.barricadeAmount.ToString();

        /*if (!waves.readyToSpawn && GameObject.FindGameObjectWithTag("Enemy") == false)
        {
            ShowCountdown();
        }
        else
        {
            HideCountdown();
        }*/

        buildableButtons();



        PerkHandlerUI();

        PlayerHPStatus();

        OnDeathUI();
        TabMenu();
        SpecialMeter();
    }

    private void TabMenu()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && !isTabScreenOpen)
        {
            Time.timeScale = 0;
            tabScreen.SetActive(true);
            isTabScreenOpen = true;
        }
        else if(Input.GetKeyDown(KeyCode.Tab) && isTabScreenOpen)
        {
            Time.timeScale = 1;
            tabScreen.SetActive(false);
            isTabScreenOpen = false;
        }

    }

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        gameManager.GetComponent<SaveDataManager>().SaveData();
        SceneManager.LoadScene("MainMenu");
    }

    private void SpecialMeter()
    {
        PlayerSpecialAbility psa = player.GetComponent<PlayerSpecialAbility>();
        specialFillMeter.GetComponent<Image>().fillAmount = psa.specialMeterCurrent / 100;

        if(psa.specialMeterCurrent == psa.specialMeterMax)
        {
            specialButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            specialButton.GetComponent<Button>().interactable = false;
        }
    }

    public void HitFlash()
    {
        hitflash.GetComponent<Animator>().SetTrigger("Hit");
    }

    private void OnDeathUI()
    {
        if(player.isDead)
        {
            fadeOut.SetActive(true);
        }
    }

    private void PlayerHPStatus()
    {
        if(player.hp == 3)
        {
            playerHPStatus.text = "FINE";
            playerHPStatus.color = Color.green;
        }
        else if(player.hp == 2)
        {
            playerHPStatus.text = "CAUTION";
            playerHPStatus.color = Color.yellow;
        }
        else if(player.hp == 1)
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

    public void PerkHandlerUI()
    {
        if(perkManager.greed) greedIcon.SetActive(true);
        if(perkManager.quick) speedIcon.SetActive(true);
        if(perkManager.immunity) immunityIcon.SetActive(true);

        if (perkManager.revival) revivalIcon.SetActive(true);
        else revivalIcon.SetActive(false);
    }

    public bool IsMousePointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    public void onInteract()
    {
        interactPrompt.SetActive(true);
    }

    public void ShowCountdown()
    {
        countdownPrompt.SetActive(true);
        countdownText.text = waves.countdownUntilNextWave.ToString("F0");
    }

    public void HideCountdown()
    {
        countdownPrompt.SetActive(false);
    }

    public void ShowShopMenu()
    {
        shopMenu.SetActive(true);
    }

    public void HideShopMenu()
    {
        shopMenu.SetActive(false);
    }

    public void ShowBuildablesSection()
    {
        buildablesSection.SetActive(true);
        weaponsSection.SetActive(false);
    }

    public void ShowWeaponsSection()
    {
        buildablesSection.SetActive(false);
        weaponsSection.SetActive(true);
    }

    public void offInteract()
    {
        interactPrompt.SetActive(false);
    }

    public void OpenBuildInventory()
    {
        buildInventoryAnimator.SetTrigger("OpenInventory");
    }

    public void ClickToBuyWeapon(WeaponData weapon)
    {
        if(gameManager.playerPoints >= weapon.price)
        {
            eventsManager.GiveWeapon(weapon, weapon.price);
            soundManager.Purchase();
        }
    }

    

    public void buildableButtons()
    {
        if(buildManager.turretAmount <= 0)
        {
            turretButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            turretButton.GetComponent<Button>().enabled = true;
        }

        if (buildManager.barricadeAmount <= 0)
        {
            barricadeButton.GetComponent<Button>().enabled = false;
        }
        else
        {
            barricadeButton.GetComponent<Button>().enabled = true;
        }
    }
}
