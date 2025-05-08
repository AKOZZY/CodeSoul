using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBolt : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;
    SpriteRenderer sr;
    WeaponSlot slot;

    public GameObject shocker;
    public GameObject damageCounter;

    public float speed;
    public float destroyTime;

    public int bulletDamage;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponent<SpriteRenderer>();

        slot = player.GetComponentInChildren<WeaponSlot>();
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            int giveRandomDamage = Random.Range(bulletDamage, bulletDamage + 3);

            collision.gameObject.GetComponent<Enemy>().TakeDamage(giveRandomDamage);

            GameObject hitCounter = Instantiate(damageCounter, collision.gameObject.transform.position, Quaternion.identity);
            hitCounter.GetComponent<HitCounter>().damage = giveRandomDamage;
        }
    }

}
