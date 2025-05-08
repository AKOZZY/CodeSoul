using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Player Components;
    public PlayerData playerData;
    public GameManager gameManager;
    public SaveDataManager saveDataManager;
    public UIManager uiManager;
    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;
    DialogueManager dialogueManager;
    public GameObject head;

    // Movement Variables;
    public float speed; // default value 35.0f 
    public float quickSpeed;
    public float slowedSpeed;
    

    // Booleans;
    bool movingLeft;
    bool movingRight;
    public bool facingRight = true;
    public bool standingNearBuildable = false;
    public bool hasBeenHit = false;
    public bool isDead = false;
    public bool hitByPhantom = false;

    // Mouse Position;
    Vector3 mousePosition;

    // Perks
    [SerializeField] PerkManager perks;

    public int hp;

    public bool hasGreed = false;

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        saveDataManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveDataManager>();
    }

    private void Start()
    {
        playerData = gameManager.currentCharacter;
        hp = playerData.hp;
        gameManager.playerPoints = 500;
        speed = gameManager.currentCharacter.speed;
        quickSpeed = speed * 1.50f;
        slowedSpeed = speed / 2;

        dialogueManager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();

        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        
        if(!isDead)
        {
            Move();
        }
    }

    private void Update()
    {
        OnDeath();

        if (!isDead)
        {
            Inputs();
            Animate();
            Perks();

            if (!uiManager.isTabScreenOpen)
            {
                Flip();
            }
        }

        SlowPlayer();
        
    }

    public void SlowPlayer()
    {
        if(hitByPhantom)
        {
            StartCoroutine(SlowedByPhantom(3));
        }
    }

    private void OnDeath()
    {
        if(hp <= 0 && !perks.revival && !isDead)
        {
            isDead = true;
            saveDataManager.SaveData();
            gameManager.RankCalculator();
            StartCoroutine(DeathSequence(1));
        }
    }

    private void Hit()
    {
        if(!hasBeenHit && !isDead)
        {
            hasBeenHit = true;
            hp--;
            StartCoroutine(InvisibiltyFrames());
            dialogueManager.HurtDialogue();
            uiManager.HitFlash();
        }
    }

    private void Perks()
    {
        if (perks.quick)
        {
            speed = quickSpeed;
        }

        if (hp <= 0 && perks.revival)
        {
            perks.revival = false;
            hp = playerData.hp;
        }

        if(perks.greed)
        {
            hasGreed = true;
        }
    }

    private void Inputs()
    {
        movingLeft = Input.GetKey(KeyCode.A);
        movingRight = Input.GetKey(KeyCode.D);
    }

    private void Move()
    {
        if (movingLeft)
        {
            rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
            //facingRight = false;
        }
        else if (movingRight)
        {
            rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
            //facingRight = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    private void Flip()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        if(mousePosition.x > 0)
        {
            facingRight = true;
        }
        else
        {
            facingRight = false;
        }


        if (facingRight) transform.localScale = new Vector3(1, 1, 1);
        else transform.localScale = new Vector3(-1, 1, 1);
    }

    private void Animate()
    {
        animator.runtimeAnimatorController = gameManager.currentCharacter.controller;


        if (movingLeft || movingRight) animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);
    }

    private IEnumerator InvisibiltyFrames()
    {
        hasBeenHit = true;
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 100);
        yield return new WaitForSeconds(3f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        hasBeenHit = false;
    }

    private IEnumerator DeathSequence(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        SceneManager.LoadScene("RankingScreen");
    }

    private IEnumerator SlowedByPhantom(float slowDownTime)
    {
        speed = slowedSpeed;

        yield return new WaitForSeconds(slowDownTime);

        if(perks.quick)
        {
            speed = quickSpeed;
        }
        else
        {
            speed = playerData.speed;
        }

        hitByPhantom = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buildable"))
        {
            standingNearBuildable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Buildable"))
        {
            standingNearBuildable = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Hit();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Hit();
        }
    }
}
