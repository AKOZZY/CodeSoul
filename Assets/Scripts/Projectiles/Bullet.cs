using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;
    WeaponSlot slot;
    Rigidbody2D rb;
    SpriteRenderer sr;

    int bulletDamage;
    public float speed;
    public float destroyTime;

    public GameObject damageCounter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        slot = player.GetComponentInChildren<WeaponSlot>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        bulletDamage = slot.GetCurrentWeapon().damage;

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
            rb.AddForce(Vector2.right * speed);
            sr.flipX = false;
        }
        else
        {
            rb.AddForce(Vector2.left * speed);
            sr.flipX = true;
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
            
            Destroy(gameObject);
        }
    }
}
