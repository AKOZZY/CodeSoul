using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponSlot : MonoBehaviour
{
    UIManager ui;
    Player player;
    public WeaponData[] weapons;
    SpriteRenderer spriteRenderer;
    Animator animator;
    EventsManager eventsManager;
    float fireTimer;
    public CameraFollow playerCam;

    AudioSource audioSource;
    AudioClip gunshot;
    public AudioClip reload;

    public GameObject lowAmmoAlert;
    public GameObject reloadAlert;
    public GameObject reloadingAlert;
    public TMP_Text reloadTimer;
    public float reloadNumber;

    bool isReloding = false;

    public int currentWeapon = 0;
    public bool canSwap = true;

    [SerializeField] GameObject bulletCasing;
    SpriteRenderer bulletCasingSR;

    public GameObject bullet;
    public GameObject secondaryWeaponVisual;

    public Transform firepoint;

    public int bulletAmount;
    public float spread;

    float reloadTime;

    float currentPitch;

    //IEnumerator reloadCoroutine;

    private void Start()
    {
        fireTimer = 50;
        
        eventsManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventsManager>();
        eventsManager.weaponSlot = gameObject.GetComponent<WeaponSlot>();

        audioSource = gameObject.GetComponent<AudioSource>();
        currentPitch = audioSource.pitch;

        player = GetComponentInParent<Player>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>();

        
    }

    private void Update()
    {
        fireTimer += Time.deltaTime;

        //reloadCoroutine = ReloadCoroutine(GetCurrentWeapon().reload);

        if (!player.isDead)
        {
            if (weapons[1] != null)
            {
                SwapWeapons();
            }
            

            if (GetCurrentWeaponAmmo() <= 0)
            {
                SetCurrentWeaponAmmo(0);
            }
            else
            {
                if(!isReloding)
                {
                    Fire(GetCurrentWeaponFirerate());
                }
            }
        }

        bulletCasingSR = bulletCasing.GetComponent<SpriteRenderer>();

        CurrentWeapon();

        
        ReloadCurrentWeapon();
        //Reload();
        AmmoWarning();

        bullet = GetCurrentWeapon().projectile;
        bulletAmount = GetCurrentWeapon().shellsPerShot;

        SecondaryWeaponOnBack();
        
    }

    /*private void Aim()
    {
        Vector3 mousePosWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = mousePosWorld - transform.position;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);

        
    }*/

    private void SecondaryWeaponOnBack()
    {
        if (weapons[1] != null)
        {
            foreach (WeaponData weapon in weapons)
            {
                if(weapon != weapons[currentWeapon])
                {
                    secondaryWeaponVisual.GetComponent<SpriteRenderer>().sprite = weapon.sprite;
                }
            }
        }
    }

    private void AmmoWarning()
    {
        Vector3 offset = new Vector3(25, 30, 0);

        lowAmmoAlert.transform.position = Input.mousePosition + offset;
        reloadAlert.transform.position = Input.mousePosition + offset;
        reloadingAlert.transform.position = Input.mousePosition + offset;

        if(GetCurrentWeapon().ammo <= (GetCurrentWeapon().magazine / 3))
        {
            lowAmmoAlert.SetActive(true);
            reloadAlert.SetActive(false);
        }
        else
        {
            lowAmmoAlert.SetActive(false);
        }

        if(GetCurrentWeapon().ammo == 0)
        {
            lowAmmoAlert.SetActive(false);
            reloadAlert.SetActive(true);
        }
        else
        {
            reloadAlert.SetActive(false);
        }

        if(isReloding)
        {
            lowAmmoAlert.SetActive(false);
            reloadAlert.SetActive(false);
            reloadingAlert.SetActive(true);
            reloadNumber -= Time.deltaTime;
            reloadTimer.text = reloadNumber.ToString("F1");
        }
        else
        {
            reloadNumber = GetCurrentWeapon().reload;
            reloadingAlert.SetActive(false);
            reloadTimer.text = string.Empty;
        }
    }


    private void Fire(float firerate)
    {
        if(!ui.isTabScreenOpen)
        {
            if (Input.GetKey(KeyCode.Mouse0) && firerate < fireTimer && canSwap && !ui.IsMousePointerOverUI())
            {
                if (GetCurrentWeapon().type == WeaponType.melee)
                {
                    fireTimer = 0;

                    animator.SetTrigger("Fire");
                    audioSource.pitch = Random.Range(currentPitch, 2);
                    audioSource.PlayOneShot(GetCurrentWeapon().sound);

                    Instantiate(bulletCasing, player.GetComponentInParent<Transform>().position, Quaternion.identity);
                    Instantiate(bullet, firepoint.position, Quaternion.identity);
                }
                else if (GetCurrentWeapon().type == WeaponType.normal)
                {
                    fireTimer = 0;

                    //animator.SetBool("Fire", true);
                    animator.SetTrigger("Fire");

                    audioSource.pitch = Random.Range(currentPitch, 2);
                    audioSource.PlayOneShot(GetCurrentWeapon().sound);
                    DepleteAmmo();
                    Instantiate(bulletCasing, player.GetComponentInParent<Transform>().position, Quaternion.identity);
                    Instantiate(bullet, firepoint.position, Quaternion.identity);
                    
                }
                else if (GetCurrentWeapon().type == WeaponType.shotgun)
                {


                    fireTimer = 0;

                    //animator.SetBool("Fire", true);
                    animator.SetTrigger("Fire");
                    audioSource.pitch = Random.Range(currentPitch, 2);
                    audioSource.PlayOneShot(GetCurrentWeapon().sound);
                    DepleteAmmo();
                    Instantiate(bulletCasing, player.GetComponentInParent<Transform>().position, Quaternion.identity);
                    

                    for (int i = 0; i < bulletAmount; i++)
                    {
                        float randomY = Random.Range(-.5f, .5f);
                        Instantiate(bullet, firepoint.position + new Vector3(0, randomY, 0), Quaternion.identity);
                    }
                }
                else if (GetCurrentWeapon().type == WeaponType.special)
                {
                    fireTimer = 0;

                    //animator.SetBool("Fire", true);
                    animator.SetTrigger("Fire");
                    audioSource.pitch = Random.Range(currentPitch, 2);
                    audioSource.PlayOneShot(GetCurrentWeapon().sound);
                    DepleteAmmo();
                    

                    Instantiate(bullet, firepoint.position, Quaternion.identity);
                }
                else if (GetCurrentWeapon().type == WeaponType.explosive)
                {
                    fireTimer = 0;

                    //animator.SetBool("Fire", true);
                    animator.SetTrigger("Fire");
                    audioSource.pitch = Random.Range(currentPitch, 2);
                    audioSource.PlayOneShot(GetCurrentWeapon().sound);
                    DepleteAmmo();
                    
                    Instantiate(bullet, firepoint.position, Quaternion.identity);
                }
            }
        }
    }

    /*private void Reload()
    {
        if (GetCurrentWeaponAmmo() < GetCurrentWeapon().magazine)
        {
            if(Input.GetKeyDown(KeyCode.R) && !isReloding)
            {
                audioSource.PlayOneShot(reload);
                isReloding = true;
                //canSwap = false;
                StartCoroutine(reloadCoroutine);
            }
        }
    }*/

    private void ReloadCurrentWeapon()
    {
        if (GetCurrentWeaponAmmo() < GetCurrentWeapon().magazine)
        {
            if (Input.GetKeyDown(KeyCode.R) && !isReloding)
            {
                audioSource.PlayOneShot(reload);
                isReloding = true;
            }
        }

        if(isReloding)
        {
            reloadTime += Time.deltaTime;

            if(reloadTime >= weapons[currentWeapon].reload)
            {
                weapons[currentWeapon].ammo = weapons[currentWeapon].magazine;
                isReloding = false;
                reloadTime = 0;
            }
            else if(!canSwap)
            {
                reloadTime = 0;
                isReloding = false;
            }
        }
    }

    private void DepleteAmmo()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                weapon.ammo--;
            }
            i++;
        }
    }

    private void CurrentWeapon()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if(i == currentWeapon)
            {
                
                spriteRenderer.sprite = weapon.sprite;
                bulletCasingSR.sprite = weapon.bullet;
                animator.runtimeAnimatorController = weapon.controller;
                
            }
            i++;
        }
    }

    public void SetWeapon(WeaponData weapon)
    {
        if (weapons[1] == null)
        {
            weapons[1] = weapon;
        }
        else
        {
            weapons[currentWeapon] = weapon;
        }
    }

    public void ReplaceWeaponWithUpgradedVersion(WeaponData upgradedWeapon)
    {
        weapons[currentWeapon] = upgradedWeapon;
    }

    public int GetCurrentWeaponID()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon.id;
            }
            i++;
        }

        return 0;
    }

    public float GetCurrentWeaponFirerate()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon.firerate;
            }
            i++;
        }

        return 0;
    }

    public int GetCurrentWeaponAmmo()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon.ammo;
            }
            i++;
        }

        return 0;
    }

    public void SetCurrentWeaponAmmo(int ammo)
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                weapon.ammo = ammo;
            }
            i++;
        }
    }

    public bool CompareID(int ID)
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                if (weapon.id == ID)
                {
                    return true;
                }
            }
            i++;
        }

        return false;
    }

    public bool LookForWeaponInInventory(int ID)
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (weapon != null)
            {
                if(weapon.id == ID)
                {
                    return true;
                }
            }

            i++;
        }

        return false;
    }

    public Sprite GetCurrentWeaponImage()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon.sprite;
            }
            i++;
        }

        return null;
    }

    public string GetCurrentWeaponName()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon.title;
            }
            i++;
        }

        return null;
    }

    public WeaponData GetCurrentWeapon()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon;
            }
            i++;
        }

        return null;
    }

    public WeaponData GetPrimaryWeapon()
    {
        return weapons[0];
    }

    public WeaponData GetSecondaryWeapon()
    {
        return weapons[1];
    }

    public RuntimeAnimatorController GetCurrentWeaponAnimatorController()
    {
        int i = 0;

        foreach (WeaponData weapon in weapons)
        {
            if (i == currentWeapon)
            {
                return weapon.controller;
            }
            i++;
        }

        return null;
    }

    private void SwapWeapons()
    {
        // Swap to primary
        if (Input.GetKeyDown(KeyCode.Alpha1) && canSwap && currentWeapon != 0)
        {
            currentWeapon = 0;
            StartCoroutine(ResetTimer());
            StartCoroutine(SwapDelay());
            //StopCoroutine(reloadCoroutine);
        }
        // Swap to secondary
        if (Input.GetKeyDown(KeyCode.Alpha2) && canSwap && currentWeapon != 1)
        {
            currentWeapon = 1;
            StartCoroutine(ResetTimer());
            StartCoroutine(SwapDelay());
            //StopCoroutine(reloadCoroutine);
        }
    }

    public void OnWeaponPickup()
    {
        GetCurrentWeapon().ammo = GetCurrentWeapon().magazine;
        StartCoroutine(SwapDelay());
    }

    private IEnumerator ResetTimer()
    {
        yield return new WaitForSeconds(0.1f);
        fireTimer = 90;
    }

    private IEnumerator SwapDelay()
    {
        canSwap = false;
        yield return new WaitForSeconds(.1f);
        canSwap = true;
    }

    private IEnumerator ReloadCoroutine(float reloadSpeed)
    {
        reloadSpeed = GetCurrentWeapon().reload;
        yield return new WaitForSeconds(reloadSpeed);
        SetCurrentWeaponAmmo(GetCurrentWeapon().magazine);
        //canSwap = true;
        isReloding = false;
    }
}
