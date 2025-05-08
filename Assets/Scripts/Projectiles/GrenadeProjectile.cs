using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    SpriteRenderer sr;
    WeaponSlot slot;

    public float mouseY;
    public float speed;
    float upForce;

    public float destroyTime;
    public int bulletDamage;

    public GameObject explosion;
    public GameObject damageCounter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        slot = player.GetComponentInChildren<WeaponSlot>();
        bulletDamage = slot.GetCurrentWeapon().damage;

        Destroy(gameObject, destroyTime);

        mouseY = Input.mousePosition.y;

        if(mouseY >= 300)
        {
            mouseY = 300;
        }
        else if(mouseY <= 100)
        {
            mouseY = 100;
        }

        upForce = mouseY;

        BulletDirection();
    }

    private void Update()
    {
        
    }

    

    private void BulletDirection()
    {
        if (player.facingRight)
        {
            rb.AddForce(Vector2.right * speed, ForceMode2D.Force);
            rb.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            sr.flipX = false;
        }
        else
        {
            rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            rb.AddForce(Vector2.up * upForce, ForceMode2D.Force);
            sr.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);

            int randomSplashDamage = Random.Range(bulletDamage, bulletDamage + 3);
            explosion.GetComponent<Explosion>().explosiveDamage = randomSplashDamage;

            Destroy(gameObject);


        }

        if(collision.gameObject.CompareTag("Ground"))
        {
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            int giveRandomDamage = Random.Range(bulletDamage, bulletDamage + 3);

            collision.gameObject.GetComponent<Enemy>().TakeDamage(giveRandomDamage);

            GameObject hitCounter = Instantiate(damageCounter, collision.gameObject.transform.position, Quaternion.identity);
            hitCounter.GetComponent<HitCounter>().damage = giveRandomDamage;
        }
    }
}
