using System.Collections;
using System.Collections.Generic;
using System.Security;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    EventsManager eventsManager;
    WeaponSlot weaponSlot;
    EndlessWaveGenerater ewg;

    DialogueManager dialogueManager;

    public AudioSource enemyHitSource;
    public AudioSource enemyVoiceSource;

    public AudioClip enemyHitSound;

    public EnemyData enemyData;
    Rigidbody2D rb;
    bool facingRight = true;
    [SerializeField] GameObject healthbar;

    Material currentMaterial;
    [SerializeField] Material hitMaterial;

    public int hp;
    public int speed;
    public double pointReward;

    public GameObject bloodsplatter;

    float enemySoundTimer = 0;

    
    

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        ewg = GameObject.FindGameObjectWithTag("WaveManager").GetComponent<EndlessWaveGenerater>();
        hp = enemyData.maxhp;
        hp += ewg.enemyHpModifier;
        pointReward = enemyData.pointReward;
        pointReward += ewg.enemyPointRewardModifier;
        speed = enemyData.speed;
        weaponSlot = GameObject.FindGameObjectWithTag("PlayerWeapon").GetComponent<WeaponSlot>();
        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
        eventsManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<EventsManager>();
        UpdateHealth(hp);
        currentMaterial = gameObject.GetComponent<SpriteRenderer>().material;
        healthbar.GetComponent<Slider>().maxValue = hp;
        enemyHitSource = gameObject.GetComponentInChildren<AudioSource>();
    }

    private void FixedUpdate()
    {
        FollowPlayer(GameObject.FindGameObjectWithTag("Player"));
    }

    private void Update()
    {
        
        
        
        FlipSelf(GameObject.FindGameObjectWithTag("Player"));
        //OnDeath();
        RandomChanceEnemyNoise();
    }

    

    public void UpdateHealth(int health)
    {
        hp = health;
        healthbar.GetComponent<Slider>().value = health;
    }

    private void FollowPlayer(GameObject target)
    {
        if(transform.position.x > target.transform.position.x)
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
        }
    }

    private void FlipSelf(GameObject target)
    {
        if (transform.position.x > target.transform.position.x)
        {
            //transform.localScale = new Vector3(-1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }
        else
        {
            //transform.localScale = new Vector3(1, 1, 1);
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }
    }

    public void Hit()
    {
        hp--;
        healthbar.SetActive(true);
        UpdateHealth(hp);
        StartCoroutine(hitAnimation());
    }

    public void TakeDamage(int damage)
    {
        hp = hp - damage;
        healthbar.SetActive(true);
        UpdateHealth(hp);
        StartCoroutine(hitAnimation());
    }

    private void RandomChanceDialogue()
    {
        int randomNumer = Random.Range(0, 21);

        if(randomNumer == 20)
        {
            dialogueManager.RandomKillDialogue();
        }
    }

    private void RandomChanceEnemyNoise()
    {
        
        float enemySoundInterval = 1;

        enemySoundTimer += Time.deltaTime;

        if (enemySoundTimer > enemySoundInterval)
        {
            int randomNumber = Random.Range(0, 35);
            if (randomNumber == 10)
            {
                AudioClip randomAudio = enemyData.sounds[Random.Range(0, enemyData.sounds.Length)];
                enemyVoiceSource.PlayOneShot(randomAudio);
            }
            enemySoundTimer = 0;
            
        }
    }

    private void OnDeath()
    {
        if(hp <= 0)
        {
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;

            if(GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().hasGreed)
            {
                eventsManager.GivePoints(pointReward * 1.30);
            }
            else
            {
                eventsManager.GivePoints(pointReward);
            }
            dialogueManager.KillFirstEnemyDialogue();
            RandomChanceDialogue();
            dialogueManager.hasKilledEnemy = true;
            Destroy(gameObject);
            gameManager.GetComponent<SaveDataManager>().kills++;
            gameManager.kills++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            //TakeDamage(weaponSlot.GetCurrentWeapon().damage);

            
        }
    }

    private IEnumerator hitAnimation()
    {
        if (!enemyHitSource.isPlaying)
        {
            enemyHitSource.PlayOneShot(enemyHitSound);
        }
        gameObject.GetComponent<SpriteRenderer>().material = hitMaterial;

        
        //yield return new WaitForSeconds(.3f);

        

        if (facingRight)
        {
            rb.AddForce(Vector2.left * 25);
            rb.AddForce(Vector2.up * 10);
        }
        else if(!facingRight)
        {
            rb.AddForce(Vector2.right * 25);
            rb.AddForce(Vector2.up * 10);
        }
        
        GameObject BloodSplatter = Instantiate(bloodsplatter, transform.position, Quaternion.identity);
        //BloodSplatter.transform.parent = transform;
        
        yield return new WaitForSeconds(.2f);
        gameObject.GetComponent<SpriteRenderer>().material = currentMaterial;
        OnDeath();
        
        
        
    }
}
