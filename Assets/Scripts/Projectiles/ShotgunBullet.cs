using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    Player player;
    WeaponSlot slot;
    Rigidbody2D rb;
    SpriteRenderer sr;

    Vector3 direction;

    public float speed = 500;
    public float upForce = 1000;

    public float destroyTime;
    public int enemiesHit = 0;

    int damage;

    public GameObject damageCounter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        slot = player.GetComponentInChildren<WeaponSlot>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        damage = slot.GetCurrentWeapon().damage;

        BulletDirection();
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
        
    }

    private void BulletDirection()
    {
        if (player.facingRight)
        {
            rb.AddForce(Vector3.right * speed);
   

            sr.flipX = false;
        }
        else
        {
            rb.AddForce(Vector3.right * -speed);


            sr.flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int giveRandomDamage = Random.Range(damage, damage + 3);

            collision.gameObject.GetComponent<Enemy>().TakeDamage(giveRandomDamage);

            GameObject hitCounter = Instantiate(damageCounter, collision.gameObject.transform.position, Quaternion.identity);
            hitCounter.GetComponent<HitCounter>().damage = giveRandomDamage;

            if (enemiesHit < 3)
            {
                enemiesHit++;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
