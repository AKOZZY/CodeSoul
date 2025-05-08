using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCasing : MonoBehaviour
{
    Player player;
    Rigidbody2D rb;

    float timer;

    public float force;
    public float upForce;

    // Start is called before the first frame update
    void Start()
    {
        force = Random.Range(500, 1000);
        upForce = Random.Range(350, 1000);


        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        rb = gameObject.AddComponent<Rigidbody2D>();
        

        if (player.facingRight)
        {
            rb.AddForce(Vector2.up * upForce * Time.deltaTime, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * -force * Time.deltaTime, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.up * upForce * Time.deltaTime, ForceMode2D.Impulse);
            rb.AddForce(Vector2.right * force * Time.deltaTime, ForceMode2D.Impulse);
        }

        StartCoroutine(DestroyBullet(5));
    }

    private IEnumerator DestroyBullet(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
