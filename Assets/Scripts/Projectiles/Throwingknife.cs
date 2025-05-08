using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwingknife : MonoBehaviour
{
    Player player;
    WeaponSlot slot;
    Rigidbody2D rb;
    SpriteRenderer sr;

    public int damage = 999;
    public float speed;
    public float destroyTime;
    public float spinSpeed;

    public GameObject damageCounter;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr = gameObject.GetComponentInChildren<SpriteRenderer>();

        BulletDirection();
    }

    private void Update()
    {
        Destroy(gameObject, destroyTime);
        Spin(spinSpeed);
    }

    private void Spin(float spinSpeed)
    {
        gameObject.transform.Rotate(0, 0, -spinSpeed * Time.deltaTime);
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
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            GameObject hitCounter = Instantiate(damageCounter, collision.gameObject.transform.position, Quaternion.identity);
            hitCounter.GetComponent<HitCounter>().damage = damage;

            //Destroy(gameObject);
        }
    }
}
