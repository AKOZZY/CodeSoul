using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip exposionSFX;

    public int explosiveDamage;

    public GameObject damageCounter;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;

        audioSource.PlayOneShot(exposionSFX);

        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            int randomDamage = Random.Range(explosiveDamage, explosiveDamage + 3);

            collision.gameObject.GetComponent<Enemy>().TakeDamage(randomDamage);

            GameObject hitCounter = Instantiate(damageCounter, collision.gameObject.transform.position, Quaternion.identity);
            hitCounter.GetComponent<HitCounter>().damage = randomDamage;
        }
    }
}
